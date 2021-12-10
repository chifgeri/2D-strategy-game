using Assets.Scripts.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class EnemyCharacter : Character
    {
        private EnemyTypes type;

        public EnemyTypes Type { get => type; set => type = value; }

        [SerializeField]
        private int ExperienceValue;

        private bool InAttack = false;
        private bool IsDieing = false;

        public override void AttackAction(Character [] targets)
        {
            if (MainStateManager.Instance.CurrentRound != null) {
                var playerCharacters = MainStateManager.Instance.CurrentRound.PlayerGroup.Characters;
                var enemyCharacters = MainStateManager.Instance.CurrentRound.EnemyGroup.Characters;

                var behaviours = this.GetComponents<IBaseBehaviour>();
                if (behaviours != null && behaviours.Length != 0) {
                    InAttack = true;
                    StartCoroutine(PlayAnimationWithCallback("Attack", () => {
                        IsInAction = true;
                        var index = UnityEngine.Random.Range(0, behaviours.Length);
                        var behaviour = behaviours[index];
                        behaviour.Action(this, playerCharacters.ToArray(), enemyCharacters.ToArray());
                        InAttack = false;
                        CharacterActionDoneInvoke();
                    }));
                   
                } else
                {
                    Debug.LogError("No behaviour on Enemy");
                    CharacterActionDoneInvoke();
                }
            } else
            {
                Debug.LogError("Not in a fight (current round is null)");
            }

            
        }

        protected override void Update()
        {
            base.Update();
            if (base.IsNext && !InAttack && !IsDieing)
            {
                this.AttackAction(null);
            }
        }

        public override void Die(Character caster)
        {
            IsDieing = true;
            if(caster is PlayerCharacter)
            {
                var player = (PlayerCharacter)caster;
                player.Experience += ExperienceValue;
            }
            base.Die(caster);
        }
    }
}
