using Assets.Scripts.Utils;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    public class AttackRandom : MonoBehaviour, IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0)
            {
                var target = players[UnityEngine.Random.Range(0, players.Length)];
                if (!Calculations.CalculateMiss(caster) && !Calculations.CalculateDodge(caster, target))
                {
                    var dmg = Calculations.CalculateDamage(caster, target);
                    target.Hit(dmg);
                }
            }
        }
    }
}

