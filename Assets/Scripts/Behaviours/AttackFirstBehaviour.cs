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
    public class AttackFirstBehaviour : MonoBehaviour, IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0) {
                if (!Calculations.CalculateMiss(caster) && !Calculations.CalculateDodge(caster, players[0]))
                {
                    var dmg = Calculations.CalculateDamage(caster, players[0]);
                    players[0].Hit(dmg);
                }
            }
        }
    }
}
