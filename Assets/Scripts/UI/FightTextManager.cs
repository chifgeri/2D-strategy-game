using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public void ShowText(string value, Vector3 position, TextType textType)
    {
        var textObject = Instantiate(this.textObject, position, Quaternion.identity);
        var text = textObject.GetComponentInChildren<TextMeshProUGUI>();
        var animator = textObject.GetComponent<Animator>();

        text.SetText(value);

        switch (textType)
        {
            case TextType.Damage:
                text.color = new Color(190, 33, 33, 255);
                break;
            case TextType.Heal:
                text.color = new Color(56, 204, 16, 255);
                break;
            case TextType.Miss:
                text.color = new Color(181, 181, 181, 255);
                break;
            case TextType.Dodge:
                text.color = new Color(121, 121, 121, 255);
                break;
        }
        var animations = new[] {
         "TextAnimationLeft", "TextAnimationRight"
        };
        animator.Play(animations[UnityEngine.Random.Range(0, 2)]);
    }

}
