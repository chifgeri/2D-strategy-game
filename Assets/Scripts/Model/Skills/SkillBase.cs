using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

namespace Model {
    public delegate void SkillSelectedDelegate(SkillBase skill);

    public abstract class SkillBase : MonoBehaviour {

    public Sprite icon;

    public int damage;

    public string description;

    public bool disabled = true;

    public List<int> validTargetsInTeam = new List<int>(4);

    public List<int> validTargetsInEnemy = new List<int>(4);

     // This event notifies the Character Which Skill is casted currently
     public event SkillSelectedDelegate SkillSelected;

        // Effekt, ha van
        // public Effect effect;

    public bool CalculateMiss(Character caster, Character target){
      // TODO:  Implement miss calculation
      return false;
    }

    public bool CalculateDodge(Character caster, Character target){
      // TODO:  Implement dodge calculation
      return false;
    }

    public void SelectSkill() {
         SkillSelected(this);
     }
    
    public abstract void CastSkill(Character caster, Character[] target);

    public Sprite GetIcon(){
      return icon;
    }
  }

}