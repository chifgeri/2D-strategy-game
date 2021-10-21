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
        private string LevelName;
        [SerializeField]
        private string LevelId;
        // The group position at the map instance
        [SerializeField]
        private Vector2 groupPosition;

        // 0 - ground, 1 - wall, 2 - door, 3 - chest variation, 4 - barrel variation
        [SerializeField]
        private List<int> mapModel;

        // private List<Rooms> rooms;
        // Room: Enemies, Loot, Cleared, Number - ID, 


        public Map(string name, string levelId, List<int> model, Vector2 position)
        {
           LevelName = name;
           LevelId = levelId;
           mapModel = model;
           groupPosition = position;
        }
       // public List<int> MapModel { get => mapModel; set => mapModel = value; }
    }
}
