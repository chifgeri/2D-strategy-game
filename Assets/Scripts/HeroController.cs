using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class HeroController : Model.PlayerCharacter
{
    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();

    }
     protected override void Update() {
        base.Update();
        if(this.IsSelected){
            if (Input.GetKeyDown(KeyCode.E))
                {
                if(animator != null)
                {         
                    animator.SetTrigger("Attack");
                }
            }
        }  
     }
}
