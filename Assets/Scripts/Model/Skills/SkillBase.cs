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

    public bool CalculateMiss(Character caster){
            if (Random.value <= 0.2 - (0.1 * caster.Level / 10.0f))
            {
                return true;
            }
            return false;
    }

    public bool CalculateDodge(Character caster, Character target, float skillModifier)
    {
          float dodgeChance = target.DodgeChance * 0.5f *target.Level / 10.0f;
          float casterAccuracy = caster.BaseAccuracy + 0.5f * caster.Level / 10.0f * skillModifier;

            var possibility = dodgeChance * 1 - casterAccuracy;
            if(Random.value <= possibility)
            {
                return true;
            }

          return false;
    }

        public int CalculateDamage(Character caster, Character target, float skillModifier)
        {
            var dmg = (caster.BaseDamage * caster.Level - target.ArmorValue)*skillModifier;
            return Mathf.FloorToInt(dmg);
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