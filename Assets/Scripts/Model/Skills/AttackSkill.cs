﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AttackSkill : SkillBase
    {
        public override void CastSkill(Character caster, Character[] targets)
        {
            if (this.CalculateMiss(caster))
            {
                // Display miss animation, effect
                // caster.Miss()
                return;
            }

            if (this.CalculateDodge(caster, targets[0]))
            {
                // Display dodge animation, effect
                // target[0].Dodge()
                return;
            }

            int dmg = CalculateDamage(caster, targets[0]);
            // Call attack animation
            // caster.animator.SpearAttack1(targets[0]);

            // Hit enemy
            targets[0].Hit(dmg);
            return;
        }
    }
}