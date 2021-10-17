using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

namespace Model {
    public delegate void SkillSelectedDelegate(SkillBase skill);

    public abstract class SkillBase : MonoBehaviour {

    public Sprite icon;

    public int damage = 0;
    public float damageModifier = 1.0f;
    public float healModifier = 1.0f;
    public float dodgeModifier = 1.0f;
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
                Debug.Log("Missed");
                return true;
            }
            return false;
    }

    public bool CalculateDodge(Character caster, Character target)
    {
          float dodgeChance = target.baseDodgeChance * 0.5f *target.Level / 10.0f;
          float casterAccuracy = caster.baseAccuracy + 0.5f * caster.Level / 10.0f * dodgeModifier;

            var possibility = dodgeChance * (1 - casterAccuracy);
            float rand = Random.value;
            Debug.Log($"Random: {rand}");
            Debug.Log($"Possibility: {possibility}");
            if ( rand <= possibility)
            {
                return true;
            }

          return false;
    }

        public int CalculateDamage(Character caster, Character target)
        {
            var dmg = (caster.baseDamage * caster.Level + this.damage - target.baseArmor) * damageModifier;
            if(dmg < 0)
            {
                dmg = 0;
            }
            Debug.Log($"Damage: {dmg}");
            return Mathf.FloorToInt(dmg);
        }

        public int CalculateHealing(Character caster)
        {
            return (caster.baseDamage * caster.Level) + this.damage;
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