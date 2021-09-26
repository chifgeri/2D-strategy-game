using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Model {

    public class Round
    {
        private Group playerGroup;
        private Group enemyGroup;

        public Group PlayerGroup {
            get;
        }

        public Group EnemyGroup {
            get;
        }

        private int roundNumber;

        public int RoundNumber {
            get;
        }

        private Queue<Character> characterOrder;


        Round(Group players, Group enemies) {
            playerGroup = players;
            enemyGroup = enemies;
        }

        private void calculateOrder() {
            // First comes the playable characters then the enemies
            List<Character> characters = new List<Character>();
            characters.AddRange(playerGroup.Characters);
            characters.Sort(delegate (Character a, Character b) {
                return a.Speed <= b.Speed ? -1 : 1;
            });

            foreach (var item in characters) {
                characterOrder.Enqueue(item);
            }

            characters = new List<Character>();
            characters.AddRange(enemyGroup.Characters);
            characters.Sort(delegate (Character a, Character b) {
                return a.Speed <= b.Speed ? -1 : 1;
            });

            foreach (var item in characters) {
                characterOrder.Enqueue(item);
            }


        }

        public void InitRound()
        {
            calculateOrder();
            Character current = characterOrder.Dequeue();
            current.IsNext = true;
        }

        public void CharacterActionDone(Character c)
        {
            c.IsNext = false;
            if (characterOrder.Count > 0)
            {
                Character current = characterOrder.Dequeue();
                current.IsNext = true;
            } else
            {
                roundNumber++;
                InitRound();
            }
        }
    }
}
