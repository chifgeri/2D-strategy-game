using Assets.Scripts.Utils;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviours
{
    public class HealWholeTeam
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (enemyCharacters.Length > 0)
            {
                foreach (var target in enemyCharacters)
                {
                    if (!Calculations.CalculateMiss(caster))
                    {
                        var heal = Calculations.CalculateHealing(caster);
                        target.Heal(heal);
                    }
                }
            }
        }
    }
}
