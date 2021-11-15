using Assets.Scripts.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class EnemyCharacter : Character
    {
        private EnemyTypes type;

        public EnemyTypes Type { get => type; set => type = value; }

        public override void AttackAction(Character [] targets)
        {
            if (MainStateManager.Instance.CurrentRound != null) {
                var playerCharacters = MainStateManager.Instance.CurrentRound.PlayerGroup.Characters;
                var enemyCharacters = MainStateManager.Instance.CurrentRound.EnemyGroup.Characters;

                var behaviours = this.GetComponents<IBaseBehaviour>();
                if (behaviours != null && behaviours.Length != 0) {
                    // Randomly selected behaviour in each round
                    var index = UnityEngine.Random.Range(0, behaviours.Length);
                    Debug.Log(behaviours.Length);
                    Debug.Log(index);
                    var behaviour = behaviours[index];
                    behaviour.Action(this, playerCharacters.ToArray(), enemyCharacters.ToArray()); 
                } else
                {
                    Debug.LogError("No behaviour on Enemy");
                }
            } else
            {
                Debug.LogError("Not in a fight (current round is null)");
            }

            CharacterActionDoneInvoke();
        }

        protected override void Update()
        {
            base.Update();
            if (base.IsNext)
            {
                this.AttackAction(null);
            }
        }
    }
}
