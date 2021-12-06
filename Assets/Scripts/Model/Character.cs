using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Model {

    public delegate void CharacterActionDoneDelegate(Character c);
    public delegate void AnimationCallback();


    public abstract class Character : MonoBehaviour
    {
        public event CharacterNewSpellDelegate CharacterDieEvent;
        public event CharacterActionDoneDelegate CharacterActionDone;

        public HealthBarController HBPrefab;
        public NextMarker IsNextPrefab;

        protected HealthBarController healthBar;
        protected NextMarker isNextMarker;
        protected Animator animator;

        [SerializeField]
        protected ParticleSystem healEffect;

        private bool isSelected = false;
        private int health = 100;
        [SerializeField]
        private int speed;
        [SerializeField]
        private int level;
        [SerializeField]
        private int baseDamage;
        [SerializeField]
        private int baseArmor;
        [SerializeField]
        private float baseCrit;
        [SerializeField]
        private float baseStunResist;
        [SerializeField]
        private float baseDodgeChance;
        [SerializeField]
        private float baseAccuracy;


        const string animBaseLayer = "Base Layer";
        int attackAnimHash = Animator.StringToHash(animBaseLayer + ".Attack");
        int dieAnimHash = Animator.StringToHash(animBaseLayer + ".Die");

        public bool IsSelected {
            get { return isSelected; }
        }
        public string Name { get; set; }

        public bool IsNext
        {
            get;
            set;
        }

        public int Health {
            get { return health; }
            set {
                if(value <= 0)
                {
                    health = 0;
                }
                if(value >= 100)
                {
                    health = 100;
                }
                 if( value > 0 && value < 100){
                    health = value;
                }
            }
        }

        public int Level{
            get { return level; }
            set {
                if(value >= 0 && value <= 10){
                    level = value;
                }
            }
        }

        public int Speed { get => speed; set => speed = value; }
        public int BaseDamage { get => baseDamage; set => baseDamage = value; }
        public int BaseArmor { get => baseArmor; set => baseArmor = value; }
        public float BaseCrit { get => baseCrit; set => baseCrit = value; }
        public float BaseStunResist { get => baseStunResist; set => baseStunResist = value; }
        public float BaseDodgeChance { get => baseDodgeChance; set => baseDodgeChance = value; }
        public float BaseAccuracy { get => baseAccuracy; set => baseAccuracy = value; }


        public virtual int GetCurrentDamage()
        {
            return Level * BaseDamage;
        }

        public virtual int GetCurrentArmor()
        {
            return Level * BaseArmor;
        }

        protected virtual void Awake() { 
            var transform = this.GetComponent<Transform>();
            animator = this.GetComponent<Animator>();
            if (HBPrefab != null)
            {
                healthBar = Instantiate<HealthBarController>(
                        HBPrefab,
                        new Vector3(
                            transform.position.x,
                            -0.15f,
                            3),
                         Quaternion.identity);
            } else
            {
                Debug.Log($"HBPrefab is NULL on {gameObject.name}");
            }
        }

        protected virtual void Update()
        {
            if (this.IsNext)
            {
                if (isNextMarker == null)
                {
                    isNextMarker = Instantiate(
                    IsNextPrefab,
                    new Vector3(
                        transform.position.x,
                        transform.position.y + HBPrefab.GetComponent<RectTransform>().rect.height * 2 + 0.5f,
                        0.5f),
                     Quaternion.identity);
                }
                isNextMarker.gameObject.SetActive(true);
            }
            if (!this.IsNext && isNextMarker != null)
            {
                isNextMarker.gameObject.SetActive(false);
            }

            healthBar.SetValue(Health / 100.0f);
        }

        public void Hit(int damage, Character caster)
        {
            animator.SetTrigger("Hit");
            FightTextManager.Instance.ShowText(damage.ToString(), gameObject.transform.position, TextType.Damage);
            Health -= damage;
            if(Health <= 0)
            {
                Die(caster);
            }
        }

        public void Heal(int amount)
        {
            healEffect.transform.position = gameObject.transform.position;
            healEffect.Play();
            FightTextManager.Instance.ShowText(amount.ToString(), gameObject.transform.position, TextType.Heal);
            Health += amount;
        }
        
        protected void CharacterActionDoneInvoke()
        {
            this.CharacterActionDone(this);
        }

        public virtual void Die(Character caster)
        {
            StartCoroutine(PlayAnimationWithCallback("Die", () =>
            {
                if (MessagePanel.Instance != null)
                {
                    MessagePanel.Instance.ShowMessage($"{caster.Name} killed {this.Name}");
                }

                CharacterDieEvent(this);

                if (isNextMarker != null)
                {
                    Destroy(isNextMarker.gameObject);
                }
                if (healthBar != null)
                {
                    Destroy(healthBar.gameObject);
                }
                Destroy(this.gameObject);
            }));
        }

        public IEnumerator PlayAnimationWithCallback(string stateName, AnimationCallback callback)
        {
            //Get hash of animation
            int animHash = 0;
            if (stateName == "Attack")
                animHash = attackAnimHash;
            else if (stateName == "Die")
                animHash = dieAnimHash;

            //targetAnim.Play(stateName);
            animator.CrossFadeInFixedTime(stateName, 0.6f);

            //Wait until we enter the current state
            while (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != animHash)
            {
                yield return null;
            }

            float counter = 0;
            float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;

            //Now, Wait until the current state is done playing
            while (counter < (waitTime))
            {
                counter += Time.deltaTime;
                yield return null;
            }

            //Done playing. Do something below!
            Debug.Log("Done Playing");
            callback();
        }

        public virtual void Select(){
            isSelected = true;
        }

         public virtual void UnSelect(){
            isSelected = false;
        }

        public virtual void SetNext()
        {
            IsNext = true;
        }

        public virtual void UnsetNext()
        {
            IsNext = false;
        }

        public abstract void AttackAction(Character[] targets);


        private void OnMouseOver()
        {
            FightTextManager.Instance.ShowDetailToCharacter(this);
        }

        private void OnMouseExit() {
            FightTextManager.Instance.DisableDetail();
        }
    }
}
