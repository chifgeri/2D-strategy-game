using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviours
{
    class AttackLastTwoBehaviour : IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0)
            {
                if (players.Length >= 2)
                {
                    players[players.Length-1].Hit(20 * caster.Level);
                    players[players.Length-2].Hit(20 * caster.Level);

                    // Call effects, hit animation etc.
                }
                else
                {
                    players[0].Hit(20 * caster.Level);
                }
            }
        }
    }
}
