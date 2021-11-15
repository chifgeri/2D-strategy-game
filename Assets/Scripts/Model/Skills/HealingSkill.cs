using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class HealingSkill : SkillBase
    {
        public override void CastSkill(Character caster, Character[] targets)
        {
            if (Calculations.CalculateMiss(caster))
            {
                // Display miss animation, effect
                // caster.Miss()
                return;
            }

            int heal = Calculations.CalculateHealing(caster);
            // Hit enemy
            targets[0].Heal(heal);
            return;
        }
    }
}
