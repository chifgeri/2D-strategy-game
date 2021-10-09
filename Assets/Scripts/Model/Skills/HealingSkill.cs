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
            if (this.CalculateMiss(caster))
            {
                // Display miss animation, effect
                // caster.Miss()
                return;
            }

            int heal = CalculateHealing(caster);
            // Hit enemy
            targets[0].Heal(heal);
            return;
        }
    }
}
