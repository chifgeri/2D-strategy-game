using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public enum EnemyTypes
    {
        Knight,
        Orc,
        Skeleton,
        Zombie,
        Golem
    }

    [Serializable]
    public class EnemyData
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private EnemyTypes enemyType;
        [SerializeField]
        private int level;
        [SerializeField]
        private int health;

        public EnemyData(string id, EnemyTypes enemyType, int level, int health)
        {
            this.id = id;
            this.enemyType = enemyType;
            this.level = level;
            this.health = health;
        }

        public EnemyTypes EnemyType { get => enemyType; set => enemyType = value; }
        public int Level { get => level; set => level = value; }
        public int Health { get => health; set => health = value; }
        public string Id { get => id; set => id = value; }
    }
}
