using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviours
{
    class AttackLowestHealth : IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0)
            {
                var list = players.ToList();
                list.Sort(delegate(PlayerCharacter p1, PlayerCharacter p2) { return p1.Health.CompareTo(p2.Health); });
                list.First().Hit(caster.BaseDamage * caster.Level);
            }
        }
    }
}
