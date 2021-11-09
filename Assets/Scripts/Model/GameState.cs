using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameState
    {
        [SerializeField]
        private Map currentLevel;
        [SerializeField]
        private List<PlayableData> playableCharacters;
        [SerializeField]
        private RoomState fightData;
        [SerializeField]
        private int? currentRoomId;
        [SerializeField]
        private bool isInFight;
        [SerializeField]
        private bool isInMap;
        [SerializeField]
        private Vector3 lastPosition;

        public GameState(Map currentLevel, List<PlayableData> playableCharacters, RoomState fightData, bool isInFight, bool isInMap, Vector3 lastPosition, int? currentRoomId = null)
        {
            this.currentLevel = currentLevel;
            this.playableCharacters = playableCharacters;
            this.fightData = fightData;
            this.isInFight = isInFight;
            this.isInMap = isInMap;
            this.lastPosition = lastPosition;
            this.currentRoomId = currentRoomId;
        }

        public List<PlayableData> PlayableCharacters { get => playableCharacters; set => playableCharacters = value; }
        public Map CurrentLevel { get => currentLevel; set => currentLevel = value; }
        public RoomState FightData { get => fightData; set => fightData = value; }
        public bool IsInFight { get => isInFight; set => isInFight = value; }
        public bool IsInMap { get => isInMap; set => isInMap = value; }
        public Vector3 LastPosition { get => lastPosition; set => lastPosition = value; }
        public int? CurrentRoomId { get => currentRoomId; set => currentRoomId = value; }
    }
}
