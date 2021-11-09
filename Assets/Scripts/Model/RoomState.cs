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
        private int currentId;
        [SerializeField]
        private Queue<int> orderId;

        public RoomState(List<PlayableData> playerCharacters, List<EnemyData> enemyCharacters, int roundNumber, int currentId, Queue<int> orderId)
        {
            this.playerCharacters = playerCharacters;
            this.enemyCharacters = enemyCharacters;
            this.roundNumber = roundNumber;
            this.currentId = currentId;
            this.orderId = orderId;
        }

        public List<PlayableData> PlayerCharacters { get => playerCharacters; set => playerCharacters = value; }
        public List<EnemyData> EnemyCharacters { get => enemyCharacters; set => enemyCharacters = value; }
        public int RoundNumber { get => roundNumber; set => roundNumber = value; }
        public int CurrentId { get => currentId; set => currentId = value; }
        public Queue<int> OrderId { get => orderId; set => orderId = value; }
    }
}
