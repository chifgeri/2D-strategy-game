using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Map
    {
        [SerializeField]
        private string levelName;
        [SerializeField]
        private int levelOrder;
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;
        [SerializeField]
        private int levelRequirement;
        // The group position at the map instance
        [SerializeField]
        private Vector2 groupPosition;
        [SerializeField]
        private List<string> mapModel;
        [SerializeField]
        private List<Room> rooms;

        public Map(string name, List<string> model, Vector2 position, int width, int height, int levelRequirement, List<Room> rooms = null, int levelOrder = 0)
        {
            levelName = name;
            mapModel = model;
            groupPosition = position;
            this.width = width;
            this.height = height;
            this.rooms = rooms;
            this.levelRequirement = levelRequirement;
            this.levelOrder = levelOrder;
        }

        public string LevelName { get => levelName; set => levelName = value; }
        public Vector2 GroupPosition { get => groupPosition; set => groupPosition = value; }
        public List<string> MapModel { get => mapModel; set => mapModel = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public List<Room> Rooms { get => rooms; set => rooms = value; }
        public int LevelRequirement { get => levelRequirement; set => levelRequirement = value; }
        public int LevelOrder { get => levelOrder; set => levelOrder = value; }
    }
}
