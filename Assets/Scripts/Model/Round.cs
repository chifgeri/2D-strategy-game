using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Model {
    [Serializable]
    public class Round
    {
        [SerializeField]
        private Group<PlayerCharacter> playerGroup;
        [SerializeField]
        private Group<EnemyCharacter> enemyGroup;
        Character current;
        [SerializeField]
        private int roundNumber;

        public int RoundNumber {
            get => roundNumber;
        }

        [SerializeField]
        private Queue<Character> characterOrder;

        public Group<PlayerCharacter> PlayerGroup { get => playerGroup; }
        public Group<EnemyCharacter> EnemyGroup { get => enemyGroup; }
        public Queue<Character> CharacterOrder { get => characterOrder; }

        public Round(Group<PlayerCharacter> players, Group<EnemyCharacter> enemies) {
            playerGroup = players;
            enemyGroup = enemies;

            characterOrder = new Queue<Character>();
        }

        public Round(Group<PlayerCharacter> players , Group<EnemyCharacter> enemies, Queue<Character> queue, int roundNumber)
        {
            playerGroup = players;
            enemyGroup = enemies;

            this.characterOrder = queue;
            this.roundNumber = roundNumber;
        }

        private void CalculateOrder() {
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
            roundNumber = 1;
            CalculateOrder();
            InitEvents();
            SetNext();
        }

        public void InitEvents()
        {
            foreach (PlayerCharacter c in playerGroup.Characters)
            {
                c.CharacterActionDone += CharacterActionDone;
                c.CharacterDieEvent += OnCharacterDied;
            }
            foreach (EnemyCharacter c in enemyGroup.Characters)
            {
                c.CharacterActionDone += CharacterActionDone;
                c.CharacterDieEvent += OnCharacterDied;
            }
        }

        private void SetNext()
        {
            if(current != null)
            {
                current.UnsetNext();
            }
            current = characterOrder.Dequeue();
            current.SetNext();
        }

        private void OnCharacterDied(Character c)
        {
            if (c is PlayerCharacter character && playerGroup.Characters.Contains(character))
            {
                playerGroup.RemoveCharacter(character);
            }
            if (c is EnemyCharacter character1 && enemyGroup.Characters.Contains(character1))
            {
                enemyGroup.RemoveCharacter(character1);
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
            CalculateOrder();
            SetNext();
        }

        public void CharacterActionDone(Character c)
        {
            c.IsNext = false;

            if(playerGroup.Characters.Count <= 0)
            {
                // Players died its a lose
                FightTextManager.Instance.ShowDefeatText();
                MainStateManager.Instance.OnRoundLose();
                GameController.Instance.SetPlaybeDataState();
                return;
            }
            if(enemyGroup.Characters.Count <= 0)
            {
                // Enemies died its a win
                FightTextManager.Instance.ShowWinText();
                MainStateManager.Instance.OnRoundWin();
                GameController.Instance.SetPlaybeDataState();
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
