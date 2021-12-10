using Assets.Scripts.Utils;
using System;
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
            if (Calculations.CalculateMiss(caster))
            {
                return;
            }

            if (Calculations.CalculateDodge(caster, targets[0]))
            {
                return;
            }

            int dmg = Calculations.CalculateDamage(caster, targets[0], this.Damage, this.DamageModifier);

            targets[0].Hit(dmg, caster);
            return;
        }
    }
}
