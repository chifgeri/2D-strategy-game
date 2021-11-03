using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public static class EnemyTypes
    {
        public static readonly string Zombie = "Zombie";
        public static readonly string Knight = "Knight";
        public static readonly string Orc = "Orc";
        public static readonly string Skeleton = "Zombie";
        public static readonly string Golem = "Golem";

    }
    [Serializable]
    public class EnemyData
    {
        [SerializeField]
        private string enemyType;
        [SerializeField]
        private int level;
        [SerializeField]
        private int health;

        public EnemyData(string enemyType, int level, int health)
        {
            this.enemyType = enemyType;
            this.level = level;
            this.health = health;
        }

        public string EnemyType { get => enemyType; set => enemyType = value; }
        public int Level { get => level; set => level = value; }
        public int Health { get => health; set => health = value; }
    }
}
