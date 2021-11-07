using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public delegate void CharacterChangedHandler(Character c);
    public delegate void CharacterUsedSpellDelegate(Character c);
    public delegate void CharacterNewSpellDelegate(Character c);

    public abstract class Character : MonoBehaviour
    {
        public event CharacterUsedSpellDelegate CharacterUsedSpellEvent;
        public event CharacterNewSpellDelegate CharacterNewSpellEvent;
        public event CharacterNewSpellDelegate CharacterDieEvent;

        public HealthBar HBPrefab;
        public NextMarker IsNextPrefab;
        protected HealthBar healthBar;
        protected NextMarker isNextMarker;
        public Animator animator;
        public SkillBase[] skillPrefabs = new SkillBase[4];
        private List<SkillBase> skills;

        private bool isSelected = false;
        private int health = 100;
        [SerializeField]
        private int speed;
        [SerializeField]
        private int defaultLevel;
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

        public SkillBase SelectedSkill { get; set; }


        public bool IsSelected {
            get { return isSelected; }
        } 
        
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
            get { return defaultLevel; }
            set {
                if(value >= 0 && value <= 10){
                    defaultLevel = value;
                }
            }
        }

        public int Speed { get => speed; set => speed = value; }
        public int DefaultLevel { get => defaultLevel; set => defaultLevel = value; }
        public int BaseDamage { get => baseDamage; set => baseDamage = value; }
        public int BaseArmor { get => baseArmor; set => baseArmor = value; }
        public float BaseCrit { get => baseCrit; set => baseCrit = value; }
        public float BaseStunResist { get => baseStunResist; set => baseStunResist = value; }
        public float BaseDodgeChance { get => baseDodgeChance; set => baseDodgeChance = value; }
        public float BaseAccuracy { get => baseAccuracy; set => baseAccuracy = value; }

        protected virtual void Awake() {
            skills = new List<SkillBase>(4);

            var transform = this.GetComponent<Transform>();

            healthBar = Instantiate<HealthBar>(
                    HBPrefab,
                    new Vector3(
                        transform.position.x,
                        transform.position.y + HBPrefab.GetComponent<RectTransform>().rect.height * 2 + 0.15f,
                        0),
                     Quaternion.identity);

            foreach (SkillBase skillPref in skillPrefabs)
            {
                if (skillPref != null)
                {
                    SkillBase skill = Instantiate(skillPref);
                    skills.Add(skill);
                    // Notify the character when the skill selected
                    skill.SkillSelected += this.SelectSkill;
                }
                else
                {
                    skills.Add(null);
                }
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

        public void Hit(int damage)
        {
            // TODO: Show information to user
            Health -= damage;
            if(Health <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            // TODO: Show information to user
            Health += amount;
        }

        public void CastSkill(Character[] targets)
        {
            if (SelectedSkill)
            {
                SelectedSkill.CastSkill(this, targets);
                SelectedSkill = null;
                CharacterUsedSpellEvent(this);
                DisableSkills();
            }
            else
            {
                Debug.LogError("Selected skill is null");
            }
        }

        public void SelectSkill(SkillBase skill)
        {
            if (IsNext)
            {
               SelectedSkill = skill;
               CharacterNewSpellEvent(this);
            }
        }

        public void setSkill(SkillBase skill, int position){
            if(position < 0 && position > 3){
                throw new System.Exception("Wrong position given!");
            }
            skills[position] = skill;
        }

        public virtual void Die()
        {

            CharacterDieEvent(this);

            if (isNextMarker != null)
            {
                Destroy(isNextMarker.gameObject);
            }
            if (healthBar != null) {
                Destroy(healthBar.gameObject);
            }
            Destroy(this.gameObject);
        }

        public void Select(){
            isSelected = true;
        }

         public void UnSelect(){
            isSelected = false;
        }

        public List<SkillBase> GetSkills(){
            return skills;
        }

        public void EnableSkills()
        {
            foreach (var skill in skills)
            {
                skill.disabled = false;
            }
        }

        public void DisableSkills()
        {
            foreach (var skill in skills)
            {
                skill.disabled = true;
            }
        }
    }
}
