using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    GameObject CharHUD;   
    public HeroController knightPrefab1;
    public HeroController knightPrefab2;
    public HeroController knightPrefab3;
    public TargetMarker targetPrefab;

    private Character selected;
    private Group playableHeroes = new Group();
    private Group enemyGroup = new Group();
    private Round round;
    private List<TargetMarker> targetMarkers = new List<TargetMarker>();
    private List<Character> targets = new List<Character>();

    event CharacterChangedHandler CharacterChanged;

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
        var hero1 = Instantiate<HeroController>(knightPrefab1, new Vector3(-5, 0, 0), Quaternion.identity);
        var hero2 = Instantiate<HeroController>(knightPrefab2, new Vector3(-2.5f, 0, 0), Quaternion.identity);
        var hero3 = Instantiate<HeroController>(knightPrefab3, new Vector3(0, 0, 0), Quaternion.identity);

        hero1.speed = 4;
        hero2.speed = 7;
        hero3.speed = 2;


        playableHeroes.AddCharacter(hero1);
        playableHeroes.AddCharacter(hero2);
        enemyGroup.AddCharacter(hero3);      

        round = new Round(playableHeroes, playableHeroes);
        foreach(var hero in playableHeroes.Characters)
        {
            hero.CharacterNewSpellEvent += this.characterChangedSpell;
        }
        round.InitRound();
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
