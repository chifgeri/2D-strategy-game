using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Model {
    [Serializable]
    public class Round
    {
        [SerializeField]
        private Group playerGroup;
        [SerializeField]
        private Group enemyGroup;
        public Group PlayerGroup {
            get;
        }

        public Group EnemyGroup {
            get;
        }
        [SerializeField]
        private int roundNumber;

        public int RoundNumber {
            get => roundNumber;
        }

        [SerializeField]
        private Queue<Character> characterOrder;
        private Dictionary<int, Character> characters;
        private Dictionary<int, Character> enemyCharacters;
        private Queue<Character> queue;

        public Round(Group players, Group enemies) {
            playerGroup = players;
            enemyGroup = enemies;

            characterOrder = new Queue<Character>();
        }

        public Round(Dictionary<int, Character> characters, Dictionary<int, Character> enemyCharacters, Queue<Character> queue, int roundNumber)
        {
            this.characters = characters;
            this.enemyCharacters = enemyCharacters;
            this.queue = queue;
            this.roundNumber = roundNumber;
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

                c.CharacterDieEvent += OnCharacterDied; 
            }
            SetNext();
        }

        private void SetNext()
        {
            Character current = characterOrder.Dequeue();
            current.IsNext = true;
            current.EnableSkills();
        }

        private void OnCharacterDied(Character c)
        {
            if (playerGroup.Characters.Contains(c))
            {
                playerGroup.RemoveCharacter(c);
            }
            if (enemyGroup.Characters.Contains(c))
            {
                enemyGroup.RemoveCharacter(c);
            }
            if (characterOrder.Contains(c))
            {
                var queue = new Queue<Character>();
                while (characterOrder.Count < 1)
                {
                    var temp = characterOrder.Dequeue();
                    if (!temp.Equals(c))
                    {
                        queue.Enqueue(temp);
                    }
                }
                characterOrder = queue;
            }
        }

        public void ResetRound()
        {
            calculateOrder();
            SetNext();
        }

        public void CharacterActionDone(Character c)
        {
            c.IsNext = false;

            if(playerGroup.Characters.Count <= 0)
            {
                // Players died its a lose
                // TODO: Scene váltás a main képernyóre
                return;
            }
            if(enemyGroup.Characters.Count <= 0)
            {
                // Enemies died its a win
                Debug.Log("Win");
                // TODO: scene váltás, reward valalami
                return;
            }

            if (characterOrder.Count > 0)
            {
                SetNext();
            }
            else
            {
                roundNumber++;
                ResetRound();
            }
        }
    }
}
