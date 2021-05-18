using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    private SkillBase skill;

    private Image skillIcon;

    private void Awake() {
        // There can be 2 Images, the Background and the Icon
        Image[] images = this.gameObject.GetComponentsInChildren<Image>();
        Image slotImage = this.gameObject.GetComponent<Image>();
        Image iconSlot = null;

        foreach(Image i in images){
            if(!i.Equals(slotImage)){
                iconSlot = i;
            }
        }
        if(iconSlot != null){
            skillIcon = iconSlot;
        }
        
    }

    private void OnClick(){
        // TODO: implement click handler
    }

    public void SetSkill(SkillBase sk){
        skill = sk;
        if(sk != null){
            SetIcon(sk.GetIcon());
        } else {
            skillIcon.sprite = null;
            skillIcon.color = new Color(255,255,255, 0);
        }
    }

    public void SetIcon(Sprite sprite){
        skillIcon.sprite = sprite;
        skillIcon.color = new Color(255,255,255, 1);
    }

}
