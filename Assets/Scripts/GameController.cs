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

    Character selected;
    Group playableHeroes = new Group();
    Group enemyGroup = new Group();
    Round round;

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

        hero1.Speed = 4;
        hero2.Speed = 7;
        hero3.Speed = 2;

        playableHeroes.AddCharacter(hero1);
        playableHeroes.AddCharacter(hero2);
        enemyGroup.AddCharacter(hero3);      

        round = new Round(playableHeroes, playableHeroes);
        round.InitRound();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown (0)){ 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Character"));
                if (hit.collider != null) {
                    if(selected != null){
                        selected.UnSelect();
                    }
                    selected = hit.collider.gameObject.GetComponent<Character>();
                    if( selected != null){
                        selected.Select();
                        CharacterChanged(selected);

                        if(CharHUD != null){
                            var transform = CharHUD.GetComponent<RectTransform>();

                            if(transform != null){
                                var selectedTrans = selected.GetComponent<Transform>();
                                transform.anchoredPosition = new Vector2(selectedTrans.position.x, selectedTrans.position.y + transform.rect.height / 2.0f  );
                                CharHUD.SetActive(true);
                            }   
                        }
                    }
                    Debug.Log("You selected the " + hit.collider.gameObject);
                }
            }

        if(selected != null && selected.SelectedSkill != null)
        {
            // Display marker under valid targets
        } else
        {
            // Remove markers and events
        }
    }
}
