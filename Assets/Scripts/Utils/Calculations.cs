﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Calculations
    {
        public static bool CalculateMiss(Character caster)
        {
            if (UnityEngine.Random.value <= 0.2 - (0.1 * caster.Level / 10.0f))
            {
                Debug.Log("Missed");
                return true;
            }
            return false;
        }

        public static bool CalculateDodge(Character caster, Character target, float dodgeModifier = 1.0f)
        {
            float dodgeChance = target.BaseDodgeChance * 0.5f * target.Level / 10.0f;
            float casterAccuracy = caster.BaseAccuracy + 0.5f * caster.Level / 10.0f * dodgeModifier;

            var possibility = dodgeChance * (1 - casterAccuracy);
            float rand = UnityEngine.Random.value;
            Debug.Log($"Random: {rand}");
            Debug.Log($"Possibility: {possibility}");
            if (rand <= possibility)
            {
                return true;
            }

            return false;
        }

        public static int CalculateDamage(Character caster, Character target, int plusDamage = 0, float damageModifier = 1.0f)
        {
            var dmg = (caster.BaseDamage * caster.Level + plusDamage - target.BaseArmor) * damageModifier;
            if (dmg < 0)
            {
                dmg = 0;
            }
            Debug.Log($"Damage: {dmg}");
            return Mathf.FloorToInt(dmg);
        }

        public static int CalculateHealing(Character caster, int plusHeal = 0)
        {
            return (caster.BaseDamage * caster.Level) + plusHeal;
        }
    }
}