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
        private List<string> enemies;
        [SerializeField]
        private List<Item> lootItems;
        [SerializeField]
        private int lootMoney;
        // Door pisition in 2D coordinates which is the tilemap cell
        [SerializeField]
        private Vector2 doorPosition;
        [SerializeField]
        private bool cleared;

        public int RoomId { get => roomId; set => roomId = value; }
        public List<string> Enemies { get => enemies; set => enemies = value; }
        public List<Item> LootItems { get => lootItems; set => lootItems = value; }
        public int LootMoney { get => lootMoney; set => lootMoney = value; }
        public Vector2 DoorPosition { get => doorPosition; set => doorPosition = value; }
        public bool Cleared { get => cleared; set => cleared = value; }
    }
}
