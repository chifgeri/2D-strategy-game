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
    public class HealLowestHealth : MonoBehaviour, IBaseBehaviour
    {
        public void Action(EnemyCharacter caster, PlayerCharacter[] players, EnemyCharacter[] enemyCharacters)
        {
            if (enemyCharacters.Length > 0)
            {
                var list = enemyCharacters.ToList();
                list.Sort(delegate (EnemyCharacter p1, EnemyCharacter p2) { return p1.Health.CompareTo(p2.Health); });
                var target = list.First();
                if (!Calculations.CalculateMiss(caster))
                {
                    var heal = Calculations.CalculateHealing(caster);
                    target.Heal(heal);
                }
            }
        }
    }
}
