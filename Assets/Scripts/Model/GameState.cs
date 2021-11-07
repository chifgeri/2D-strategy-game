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
        private List<Character> playableCharacters;
        [SerializeField]
        private Round currentRound;
        [SerializeField]
        private bool isInFight;
        [SerializeField]
        private bool isInMap;
        [SerializeField]
        private Vector3 lastPosition;

        public GameState(Map currentLevel, List<Character> playableCharacters, Round round, bool isInFight, bool isInMap, Vector3 lastPosition)
        {
            this.currentLevel = currentLevel;
            this.playableCharacters = playableCharacters;
            currentRound = round;
            this.isInFight = isInFight;
            this.isInMap = isInMap;
            this.lastPosition = lastPosition;
        }

        public List<Character> PlayableCharacters { get => playableCharacters; set => playableCharacters = value; }
        public Map CurrentLevel { get => currentLevel; set => currentLevel = value; }
        public Round CurrentRound { get => currentRound; set => currentRound = value; }
        public bool IsInFight { get => isInFight; set => isInFight = value; }
        public bool IsInMap { get => isInMap; set => isInMap = value; }
        public Vector3 LastPosition { get => lastPosition; set => lastPosition = value; }
    }
}
