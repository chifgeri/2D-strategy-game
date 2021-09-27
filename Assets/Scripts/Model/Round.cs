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

        public Round(Group players, Group enemies) {
            playerGroup = players;
            enemyGroup = enemies;

            characterOrder = new Queue<Character>();
        }

        private void calculateOrder() {
            // First comes the playable characters then the enemies
            List<Character> characters = new List<Character>();
            characters.AddRange(playerGroup.Characters);
            characters.AddRange(enemyGroup.Characters);
            
            characters.Sort(delegate (Character a, Character b) {
                if(a == null)
                {
                    return -1;
                }
                if (b == null)
                {
                    return 1;
                }
                return -a.Speed.CompareTo(b.Speed);
            });

            Debug.Log(characters[0]);

            foreach (var item in characters) {
                characterOrder.Enqueue(item);
            }
        }

        public void InitRound()
        {
            calculateOrder();
            foreach(Character c in characterOrder)
            {
                c.CharacterUsedSpellEvent += CharacterActionDone;
            }
            Character current = characterOrder.Dequeue();
            Debug.Log(current.Speed);
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
