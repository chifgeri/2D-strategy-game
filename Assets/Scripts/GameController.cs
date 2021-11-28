using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

using UnityEngine.UI;
using Utils;
using System.Linq;

public class GameController : Singleton<GameController>
{
    GameObject CharHUD;   
    public TargetMarker targetPrefab;

    private PlayerCharacter selected;
    private Group<PlayerCharacter> playableHeroes = new Group<PlayerCharacter>();
    private Group<EnemyCharacter> enemyHeroes = new Group<EnemyCharacter>();
    private Round round;
    private List<TargetMarker> targetMarkers = new List<TargetMarker>();
    private List<Character> targets = new List<Character>();

    event CharacterChangedHandler CharacterChanged;

    private void InitScene()
    {
        MainStateManager.Instance.GameState.IsInFight = true;
        MainStateManager.Instance.GameState.IsInMap = false;

        var room = MainStateManager.Instance.CurrentRound;
        var state = MainStateManager.Instance.GameState;

        Dictionary<string, PlayerCharacter> characters = new Dictionary<string, PlayerCharacter>();
        Dictionary<string, EnemyCharacter> enemyCharacters = new Dictionary<string, EnemyCharacter>();

        float heroPositionX = -2.5f;
        float enemyPositionX = 2.5f;

        if (state.FightData == null)
        {
            foreach (var playerChar in state.PlayableCharacters)
            {
                var hero = CreatePlayerHero(playerChar, heroPositionX);
                characters[playerChar.Id] = hero;
                heroPositionX += -2.0f;
            }
            playableHeroes.Characters.AddRange(characters.Values);

            var roomData = state.CurrentLevel.Rooms.FirstOrDefault(r => r.RoomId == state.CurrentRoomId);
            foreach (var enemy in roomData.Enemies)
            {
                var hero = CreateEnemyHero(enemy, enemyPositionX);
                enemyCharacters[enemy.Id] = hero;
                enemyPositionX += 2f;
            }
            enemyHeroes.Characters.AddRange(enemyCharacters.Values);
            round = new Round(playableHeroes, enemyHeroes);
            round.InitRound();
        }
        else
        {
            
            foreach (var playerChar in state.FightData.PlayerCharacters)
            {
                var hero = CreatePlayerHero(playerChar, heroPositionX);
                characters[playerChar.Id] = hero;
                heroPositionX += -2.0f;
            }
            playableHeroes.Characters.AddRange(characters.Values);
            
            var roomData = state.CurrentLevel.Rooms.FirstOrDefault(r => r.RoomId == state.CurrentRoomId);
            foreach (var enemy in state.FightData.EnemyCharacters)
            {
                var hero = CreateEnemyHero(enemy, enemyPositionX);
                enemyCharacters[enemy.Id] = hero;
                enemyPositionX += 2f;
            }
            enemyHeroes.Characters.AddRange(enemyCharacters.Values);

            Queue<Character> queue = new Queue<Character>();
            foreach(var id in state.FightData.Order)
            {
                characters.TryGetValue(id, out var hero);
                if (hero != null)
                {
                    queue.Enqueue(hero);
                }
                enemyCharacters.TryGetValue(id, out var enemy);
                if (enemy != null)
                {
                    queue.Enqueue(enemy);
                }
            }
            string current = state.FightData.CurrentId;
            characters.TryGetValue(current, out var currentHero);
            enemyCharacters.TryGetValue(current, out var currentEnemy);
            if (currentHero != null)
            {
                currentHero.SetNext();
            }
            if (currentEnemy != null)
            {
                currentEnemy.SetNext();
            }
            round = new Round(playableHeroes, enemyHeroes, queue, state.FightData.RoundNumber);
            round.InitEvents();
        }

        
        foreach (var hero in playableHeroes.Characters)
        {
            hero.CharacterNewSpellEvent += this.characterChangedSpell;
        }
        MainStateManager.Instance.CurrentRound = round;
    }

    private PlayerCharacter CreatePlayerHero(PlayableData data, float xPos)
    {
        var prefab = PlayerCharacters.Instance.PlayerTypeToPrefab(data.PlayableType);
        var hero = Instantiate<HeroController>(prefab, new Vector3(xPos, 0, 0), Quaternion.identity);
        hero.Type = data.PlayableType;
        hero.Health = data.Health;
        hero.Level = data.Level;
        hero.Experience = data.Experience;
        hero.EquipWeapon(data.Weapon);
        hero.EquipArmor(data.Armor);
        hero.Price = data.Price;

        return hero;
    }

    private EnemyCharacter CreateEnemyHero(EnemyData data, float xPos)
    {
        var prefab = EnemyCharacters.Instance.EnemyTypeToPrefab(data.EnemyType);
        var hero = Instantiate<EnemyController>(prefab, new Vector3(xPos, 0, 0), Quaternion.identity);
        hero.Health = data.Health;
        hero.Level = data.Level;
        hero.Type = data.EnemyType;

        return hero;
    }

    // Start is called before the first frame update

    void Start()
    {
         InitScene();
         GameObject hudObject = GameObject.Find("CharacterHUD");
         if(hudObject != null){
            CharHUD = hudObject;
            CharHUD.SetActive(false);
         }

        CharacterChanged += UIOverlayManager.Instance.RefreshSkills;
        CharacterChanged += InventoryController.Instance.CharacterChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0)){ 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Character"));
            if (hit.collider != null) {
                var character = hit.collider.gameObject.GetComponent<Character>();
                if(character != null)
                {
                    if (IsTarget(character))
                    {
                        var chars = new Character[1];
                        chars[0] = character;
                        selected.AttackAction(chars);
                    } 
                    else
                    {
                        if (character is PlayerCharacter)
                        {
                            SelectCharacter((PlayerCharacter)character);
                        }
                    }
                }        
            } 
            else
            {
                RemoveTargetMarkers();
            }
        }

        if (selected != null && selected.SelectedSkill != null)
        {
            if (targets.Count <= 0)
            {
                DisplayTargetMarkers();
            }
        }
        else
        {
            RemoveTargetMarkers();
        }

    }

    public void DisplayTargetMarkers()
    {
         // Display marker under valid targets   
          foreach (int target in selected.SelectedSkill.ValidTargetsInTeam)
          {
              if (playableHeroes.Characters.Count >= target+1)
              {
                  var heroTransform = playableHeroes.Characters[target].gameObject.transform;
    
                  targets.Add(playableHeroes.Characters[target]);
    
                  targetMarkers.Add(
                   Instantiate<TargetMarker>(
                   targetPrefab,
                   new Vector3(
                       heroTransform.position.x,
                       heroTransform.position.y + 0.5f,
                       0.5f),
                    Quaternion.identity)
                   );
              }
          }
    
          foreach (int target in selected.SelectedSkill.ValidTargetsInEnemy)
          {
              if (enemyHeroes.Characters.Count >= target+1)
              {
                  var heroTransform = enemyHeroes.Characters[target].gameObject.transform;
    
                  targets.Add(enemyHeroes.Characters[target]);
    
    
               targetMarkers.Add(
                   Instantiate<TargetMarker>(
                   targetPrefab,
                   new Vector3(
                       heroTransform.position.x,
                       heroTransform.position.y - 0f,
                       0),
                    Quaternion.identity)
                   );
              }
          }
    }

    public void RemoveTargetMarkers()
    {
        // Remove markers
        foreach (TargetMarker marker in targetMarkers)
        {
            Destroy(marker.gameObject);
        }
        targetMarkers = new List<TargetMarker>();

        targets = new List<Character>();
    }

    public void SelectCharacter(PlayerCharacter target)
    {
        if (selected != null)
        {
            selected.UnSelect();
        }
        selected = target;
        if (selected != null)
        {
            selected.Select();
            CharacterChanged(selected);

            if (CharHUD != null)
            {
                var transform = CharHUD.GetComponent<RectTransform>();

                if (transform != null)
                {
                    var selectedTrans = selected.GetComponent<Transform>();
                    transform.anchoredPosition = new Vector2(selectedTrans.position.x, selectedTrans.position.y + transform.rect.height / 2.0f);
                    CharHUD.SetActive(true);
                }
            }
        }
    }

    private bool IsTarget(Character character)
    {
        if(selected != null && selected.SelectedSkill != null)
        {
            return targets.Contains(character);
        }
        return false;
    }

    private void characterChangedSpell(Character c)
    {
        RemoveTargetMarkers();
    }
}
