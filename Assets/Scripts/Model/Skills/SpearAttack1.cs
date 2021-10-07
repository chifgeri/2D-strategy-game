using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model {

    public class SpearAttack1 : SkillBase
    {
        // Should modify for multiple targets
        public override void CastSkill(Character caster, Character[]  targets)
        {
            if (this.CalculateMiss(caster))
            {
                // Display miss animation, effect
                // caster.Miss()
                return;
            }

            if (this.CalculateDodge(caster, targets[0], 1.05f))
            {
                // Display dodge animation, effect
                // target[0].Dodge()
                return;
            }

            int dmg = CalculateDamage(caster, targets[0], 1.1f);
            // Call attack animation
            // caster.animator.SpearAttack1(targets[0]);
            
            // Hit enemy
            targets[0].Hit(dmg);
            return;
        }
    }
}
