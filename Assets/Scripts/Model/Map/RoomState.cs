using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class RoomState
    {
        [SerializeField]
        private List<PlayableData> playerCharacters;
        [SerializeField]
        private List<EnemyData> enemyCharacters;
        [SerializeField]
        private int roundNumber;
        [SerializeField]
        private string currentId;
        [SerializeField]
        private List<string> order;

        public RoomState(List<PlayableData> playerCharacters, List<EnemyData> enemyCharacters, int roundNumber, string currentId, List<string> order)
        {
            this.playerCharacters = playerCharacters;
            this.enemyCharacters = enemyCharacters;
            this.roundNumber = roundNumber;
            this.currentId = currentId;
            this.order = order;
        }

        public List<PlayableData> PlayerCharacters { get => playerCharacters; set => playerCharacters = value; }
        public List<EnemyData> EnemyCharacters { get => enemyCharacters; set => enemyCharacters = value; }
        public int RoundNumber { get => roundNumber; set => roundNumber = value; }
        public string CurrentId { get => currentId; set => currentId = value; }
        public List<string> Order { get => order; set => order = value; }
    }
}
