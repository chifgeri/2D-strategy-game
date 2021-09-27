using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class HeroController : Model.PlayerCharacter
{
    public HealthBar HBPrefab;
    public NextMarker IsNextPrefab;
    HealthBar healthBar;
    NextMarker isNextMarker;


    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();

        var transform = this.GetComponent<Transform>();

        healthBar = Instantiate<HealthBar>(
                HBPrefab, 
                new Vector3(
                    transform.position.x,
                    transform.position.y + HBPrefab.GetComponent<RectTransform>().rect.height*2 + 0.15f,
                    0 ),
                 Quaternion.identity);
    }
     private void Update() {

        if(this.IsSelected){
            if (Input.GetKeyDown(KeyCode.E))
                {
                if(animator != null)
                {         
                    animator.SetTrigger("Attack");
 
                    Health -= 10;

                    Debug.Log(Health);

                    if(Health >= 10){
                        healthBar.SetValue(Health/100.0f);
                    }
                    else {
                        Health = 0;
                        healthBar.SetValue(0.0f);
                    } 
                }
            }
        }

        if (this.IsNext)
        {
            if(isNextMarker == null)
            {
                isNextMarker = Instantiate<NextMarker>(
                IsNextPrefab,
                new Vector3(
                    transform.position.x,
                    transform.position.y + HBPrefab.GetComponent<RectTransform>().rect.height * 2 + 0.5f,
                    0.5f),
                 Quaternion.identity);
            }
            isNextMarker.gameObject.SetActive(true);
        }
        if (!this.IsNext && isNextMarker != null)
        {
            isNextMarker.gameObject.SetActive(false);
        }
        
     }
}
