using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviours
{
    class AttackRandom : IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0)
            {
                players[UnityEngine.Random.Range(0, players.Length)].Hit(caster.BaseDamage * caster.Level);
            }
        }
    }
}

