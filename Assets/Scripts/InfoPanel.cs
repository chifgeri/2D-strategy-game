using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI characterName;
    [SerializeField]
    TextMeshProUGUI level;
    [SerializeField]
    TextMeshProUGUI speed;
    [SerializeField]
    TextMeshProUGUI damage;
    [SerializeField]
    TextMeshProUGUI armor;
    [SerializeField]
    TextMeshProUGUI stunResist;
    [SerializeField]
    TextMeshProUGUI dodgeChance;
    [SerializeField]
    TextMeshProUGUI accruacy;
    [SerializeField]
    TextMeshProUGUI critChance;
    
    public void SetValues(Character c)
    {
        characterName.text = c.name;
        level.text = c.Level.ToString();
        speed.text = c.Speed.ToString();
        damage.text = c.BaseDamage.ToString();
        armor.text = c.BaseArmor.ToString();
        stunResist.text = $"{c.BaseStunResist*100} %";
        dodgeChance.text = $"{c.BaseDodgeChance * 100} %";;
        accruacy.text = $"{c.BaseAccuracy * 100} %";
        critChance.text = $"{c.BaseCrit * 100} %";
    }

}
