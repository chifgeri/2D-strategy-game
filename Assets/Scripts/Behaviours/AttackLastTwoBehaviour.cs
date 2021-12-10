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
    public class AttackLastTwoBehaviour : MonoBehaviour, IBaseBehaviour
    {
        
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0)
            {
                if (players.Length >= 2)
                {
                    if (!Calculations.CalculateMiss(caster) && !Calculations.CalculateDodge(caster, players[players.Length - 1]))
                    {
                        var dmg = Calculations.CalculateDamage(caster, players[players.Length - 1]);
                        players[players.Length - 1].Hit(dmg, caster);
                    }
                    if (!Calculations.CalculateMiss(caster) && !Calculations.CalculateDodge(caster, players[players.Length - 2]))
                    {
                        var dmg = Calculations.CalculateDamage(caster, players[players.Length - 2]);
                        players[players.Length - 2].Hit(dmg, caster);
                    }

                    // Call effects, hit animation etc.
                }
                else
                {
                    players[0].Hit(caster.BaseDamage * caster.Level, caster);
                }
            }
        }
    }
}
