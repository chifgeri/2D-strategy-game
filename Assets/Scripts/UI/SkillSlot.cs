using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void SkillCastedDelegate(SkillBase skill);

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillBase skill;

    private Image skillIcon;

    private Button button;

    private void Awake() {
        // There can be 2 Images, the Background and the Icon
        Image[] images = this.gameObject.GetComponentsInChildren<Image>();
        Image slotImage = this.gameObject.GetComponent<Image>();
        button = this.gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => clickHandler());
        }
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

    private void Update()
    {
        if (skill != null)
        {
            if (skill.Disabled)
            {
                skillIcon.color = new Color(255, 255, 255, 0.5f);
            }
            else
            {
                skillIcon.color = new Color(255, 255, 255, 1);
            }
        }
    }

    private void clickHandler(){
        if(this.skill != null)
        {
            this.skill.SelectSkill();
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skill != null)
        {
            FightTextManager.Instance.ShowSkillDetails(skill);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FightTextManager.Instance.DisableSkillDetails();
    }

}
