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
        private string levelId;
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;
        // The group position at the map instance
        [SerializeField]
        private Vector2 groupPosition;
        [SerializeField]
        private List<string> mapModel;
        [SerializeField]
        private List<Room> rooms;

        public Map(string name, string id, List<string> model, Vector2 position, int width, int height, List<Room> rooms = null)
        {
            levelName = name;
            levelId = id;
            mapModel = model;
            groupPosition = position;
            this.width = width;
            this.height = height;
            this.rooms = rooms;
        }

        public string LevelName { get => levelName; set => levelName = value; }
        public string LevelId { get => levelId; set => levelId = value; }
        public Vector2 GroupPosition { get => groupPosition; set => groupPosition = value; }
        public List<string> MapModel { get => mapModel; set => mapModel = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public List<Room> Rooms { get => rooms; set => rooms = value; }
    }
}
