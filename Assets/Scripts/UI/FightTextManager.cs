using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Model;
using UnityEngine.UIElements;
using System;

public enum TextType
{
    Damage,
    Heal,
    Miss,
    Dodge
}

public class FightTextManager : Singleton<FightTextManager>
{
    [SerializeField]
    private GameObject textObject;
    [SerializeField]
    private InfoPanel detailPanel;
    [SerializeField]
    private GameObject winText;
    [SerializeField]
    private GameObject defeatText;
    [SerializeField]
    private ItemDetails itemDetails;
    [SerializeField]
    private SkillDetails skillDetails;

    public void ShowText(string value, Vector3 position, TextType textType)
    {
        var textObject = Instantiate(this.textObject, position, Quaternion.identity);
        var text = textObject.GetComponentInChildren<TMP_Text>();
        var animator = textObject.GetComponent<Animator>();

        text.SetText(value);

        switch (textType)
        {
            case TextType.Damage:
                text.color = new Color(190f/255f, 33f/255, 33f / 255, 255f / 255);
                break;
            case TextType.Heal:
                text.color = new Color(56f / 255, 204f / 255, 16f / 255, 255f / 255);
                break;
            case TextType.Miss:
                text.color = new Color(181f / 255, 181f / 255, 181f / 255, 255f / 255);
                break;
            case TextType.Dodge:
                text.color = new Color(121f / 255, 121f / 255, 121f / 255, 255f / 255);
                break;
        }
        var animations = new[] {
         "TextAnimationLeft", "TextAnimationRight"
        };
        animator.Play(animations[UnityEngine.Random.Range(0, 2)]);

        var color = text.color;
    }

    public void ShowDetailToCharacter(Character c)
    {
        if (!detailPanel.gameObject.activeInHierarchy)
        {
            detailPanel.gameObject.SetActive(true);
            detailPanel.SetValues(c);
        }
    }


    public void DisableDetail()
    {
        if (detailPanel.gameObject.activeInHierarchy)
        {
            detailPanel.gameObject.SetActive(false);
        }
    }

    public void ShowItemDetails(Item i)
    {
        if (!itemDetails.gameObject.activeInHierarchy)
        {
            itemDetails.gameObject.SetActive(true);
            itemDetails.SetValues(i);
        }
    }


    public void DisableItemDetails()
    {
        if (itemDetails.gameObject.activeInHierarchy)
        {
            itemDetails.gameObject.SetActive(false);
        }
    }

    public void ShowSkillDetails(SkillBase s)
    {
        if (!skillDetails.gameObject.activeInHierarchy)
        {
            skillDetails.gameObject.SetActive(true);
            skillDetails.SetValues(s);
        }
    }


    public void DisableSkillDetails()
    {
        if (skillDetails.gameObject.activeInHierarchy)
        {
            skillDetails.gameObject.SetActive(false);
        }
    }

    public void ShowWinText()
    {
        winText.SetActive(true);
    }

    public void ShowDefeatText()
    {
        defeatText.SetActive(true);
    }

}
