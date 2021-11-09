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

    private Character selected;
    private Group playableHeroes = new Group();
    private Group enemyGroup = new Group();
    private Round round;
    private List<TargetMarker> targetMarkers = new List<TargetMarker>();
    private List<Character> targets = new List<Character>();

    event CharacterChangedHandler CharacterChanged;

    private void Awake()
    {
        if(MainStateManager.Instance.CurrentRound != null)
        {
            var room = MainStateManager.Instance.CurrentRound;
            var state = MainStateManager.Instance.GameState;

            Dictionary<int, Character> characters = new Dictionary<int, Character>();
            Dictionary<int, Character> enemyCharacters = new Dictionary<int, Character>();

            if(state.FightData == null)
            {
                foreach (var playerChar in state.PlayableCharacters)
                {
                    var hero = CreatePlayerHero(playerChar);
                    characters[playerChar.Id] = hero;
                }
                playableHeroes.Characters.AddRange(characters.Values);

                var roomData = state.CurrentLevel.Rooms.FirstOrDefault(r => r.RoomId == state.CurrentRoomId);
                foreach (var enemy in roomData.Enemies)
                {
                    var hero = CreateEnemyHero(enemy);
                    enemyCharacters[enemy.Id] = hero;
                }
                enemyGroup.Characters.AddRange(enemyCharacters.Values);
                room = new Round(playableHeroes, enemyGroup);
                round = room;
                round.InitRound();
            } else
            {

                foreach (var playerChar in state.FightData.PlayerCharacters)
                {
                    var hero = CreatePlayerHero(playerChar);
                    characters[playerChar.Id] = hero;
                }
                playableHeroes.Characters.AddRange(characters.Values);

                var roomData = state.CurrentLevel.Rooms.FirstOrDefault(r => r.RoomId == state.CurrentRoomId);
                foreach (var enemy in state.FightData.EnemyCharacters)
                {
                    var hero = CreateEnemyHero(enemy);
                    enemyCharacters[enemy.Id] = hero;
                }
                enemyGroup.Characters.AddRange(enemyCharacters.Values);

                Queue<Character> queue = new Queue<Character>();
                while (state.FightData.OrderId.Count > 0) {
                    int id = state.FightData.OrderId.Dequeue();

                    characters.TryGetValue(id, out var hero);
                    if (hero != null) {
                        queue.Enqueue(hero);
                    }
                    enemyCharacters.TryGetValue(id, out var enemy);
                    if (enemy != null)
                    {
                        queue.Enqueue(enemy);
                    }
                }
                int current = state.FightData.CurrentId;
                characters.TryGetValue(current, out var currentHero);
                enemyCharacters.TryGetValue(current, out var currentEnemy);
                if (currentHero != null)
                {
                    currentHero.IsNext = true;
                }
                if (currentEnemy != null)
                {
                    currentEnemy.IsNext = true;
                }
                room = new Round(characters, enemyCharacters, queue, state.FightData.RoundNumber);
                round = room;
            }
           
        }
    }

    private Character CreatePlayerHero(PlayableData data)
    {
        var prefab = PlayerCharacters.Instance.PlayerTypeToPrefab(data.PlayableType);
        var hero = Instantiate<HeroController>(prefab, new Vector3(-5, 0, 0), Quaternion.identity);
        hero.Health = data.Health;
        hero.Level = data.Level;
        hero.Experience = data.Experience;
        hero.EquipWeapon(data.Weapon);
        hero.EquipArmor(data.Armor);

        return hero;
    }

    private Character CreateEnemyHero(EnemyData data)
    {
        var prefab = EnemyCharacters.Instance.EnemyTypeToPrefab(data.EnemyType);
        var hero = Instantiate<HeroController>(prefab, new Vector3(-5, 0, 0), Quaternion.identity);
        hero.Health = data.Health;
        hero.Level = data.Level;

        return hero;
    }

    // Start is called before the first frame update

    void Start()
    {
         GameObject hudObject = GameObject.Find("CharacterHUD");
         if(hudObject != null){
            CharHUD = hudObject;
            CharHUD.SetActive(false);
         }

        CharacterChanged += UIOverlayManager.Instance.RefreshSkills;
        CharacterChanged += UIOverlayManager.Instance.ShowCharacterInfo;
        CharacterChanged += InventoryController.Instance.CharacterChanged;

        initScene();

    }

    void initScene()
    {

        foreach(var hero in playableHeroes.Characters)
        {
            hero.CharacterNewSpellEvent += this.characterChangedSpell;
        }
        
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
                        selected.CastSkill(chars);
                    } 
                    else
                    {
                        SelectCharacter(character);
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
          foreach (int target in selected.SelectedSkill.validTargetsInTeam)
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
    
          foreach (int target in selected.SelectedSkill.validTargetsInEnemy)
          {
              if (enemyGroup.Characters.Count >= target+1)
              {
                  var heroTransform = enemyGroup.Characters[target].gameObject.transform;
    
                  targets.Add(enemyGroup.Characters[target]);
    
    
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

    public void SelectCharacter(Character target)
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
