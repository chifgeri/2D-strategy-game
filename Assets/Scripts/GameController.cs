using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    Character selected;
    GameObject CharHUD;


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
    }
}
