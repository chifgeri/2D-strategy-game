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
    public class AttackLowestHealth : MonoBehaviour, IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (players.Length > 0)
            {
                var list = players.ToList();
                list.Sort(delegate(PlayerCharacter p1, PlayerCharacter p2) { return p1.Health.CompareTo(p2.Health); });
                var target = list.First();
                if (!Calculations.CalculateMiss(caster) && !Calculations.CalculateDodge(caster, target))
                {
                    var dmg = Calculations.CalculateDamage(caster, target);
                    target.Hit(dmg);
                }
            }
        }
    }
}
