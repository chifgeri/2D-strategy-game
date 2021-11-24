using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Room
    {
        [SerializeField]
        private int roomId;
        [SerializeField]
        private List<EnemyData> enemies;
        [SerializeReference]
        private List<Item> lootItems;
        [SerializeField]
        private int lootMoney;
        // Door pisition in 2D coordinates which is the tilemap cell
        [SerializeField]
        private Vector2Int doorPosition;
        [SerializeField]
        private bool cleared;

        public Room(int roomId, List<EnemyData> enemies, List<Item> lootItems, int lootMoney, Vector2Int doorPosition, bool cleared)
        {
            this.roomId = roomId;
            this.enemies = enemies;
            this.lootItems = lootItems;
            this.lootMoney = lootMoney;
            this.doorPosition = doorPosition;
            this.cleared = cleared;
        }

        public int RoomId { get => roomId; set => roomId = value; }
        public List<EnemyData> Enemies { get => enemies; set => enemies = value; }
        public List<Item> LootItems { get => lootItems; set => lootItems = value; }
        public int LootMoney { get => lootMoney; set => lootMoney = value; }
        public Vector2Int DoorPosition { get => doorPosition; set => doorPosition = value; }
        public bool Cleared { get => cleared; set => cleared = value; }
    }
}
