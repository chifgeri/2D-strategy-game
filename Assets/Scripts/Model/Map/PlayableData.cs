﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{

    public enum PlayableTypes
    {
      Axeman,
      Paladin,
      Archer,
      Wizard,
      Medic
    }

    [Serializable]
    public class PlayableData
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private PlayableTypes playableType;
        [SerializeField]
        private int level;
        [SerializeField]
        private int health;
        [SerializeField]
        private int experience;
        [SerializeField]
        private Weapon weapon;
        [SerializeField]
        private Armor armor;

        public PlayableData(int id, PlayableTypes playableType, int level, int health, int experience, Weapon weapon, Armor armor)
        {
            this.id = id;
            this.playableType = playableType;
            this.level = level;
            this.health = health;
            this.experience = experience;
            this.weapon = weapon;
            this.armor = armor;
        }

        public PlayableTypes PlayableType { get => playableType; set => playableType = value; }
        public int Level { get => level; set => level = value; }
        public int Health { get => health; set => health = value; }
        public int Experience { get => experience; set => experience = value; }
        public Weapon Weapon { get => weapon; set => weapon = value; }
        public Armor Armor { get => armor; set => armor = value; }
        public int Id { get => id; set => id = value; }
    }
}