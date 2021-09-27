using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

    
    public delegate void CharacterChangedHandler(Character c);
    public delegate void CharacterUsedSpellDelegate(Character c);

    public abstract class Character : MonoBehaviour
    {
        public event CharacterUsedSpellDelegate CharacterUsedSpellEvent;

        public Animator animator;
        public SkillBase[] skillPrefabs = new SkillBase[4];
        private List<SkillBase> skills;

        private bool isSelected = false;
        private int health = 100;
        private int speed;
        private int level;
        private int baseDamage;
        private float baseCrit;
        private float baseStunResist;
        private float dodgeChance;

        public int Speed { get; set; }
        public int BaseDamage { get; set; }
        public float BaseCrit { get; }
        public float BaseStunResist { get; }
        public float DodgeChance { get;  }
        public int ArmorValue { get; }
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
                if(value >= 0 && value <= 100){
                    health = value;
                }
            }
        }

        public int Level{
            get { return level; }
            set {
                if(value >= 0 && value <= 100){
                    level = value;
                }
            }
        }

        protected virtual void Awake() {
            skills = new List<SkillBase>(4);
        }

        protected virtual void Start() {
             foreach(SkillBase skillPref in skillPrefabs){
                 if(skillPref != null){
                    SkillBase skill = Instantiate(skillPref);
                    skills.Add(skill);
                    // Notify the character when the skill selected
                    skill.SkillSelected += this.SelectSkill;
                 } else {
                     skills.Add(null);
                 }
            }
        }

        public void CastSkill(Character[] targets)
        {
            if (SelectedSkill)
            {
                Debug.Log("Character used spell");
                SelectedSkill.CastSkill(this, targets);
                SelectedSkill = null;
                CharacterUsedSpellEvent(this);
            }
            else
            {
                Debug.LogError("Selected skill is Null");
            }
        }

        public void SelectSkill(SkillBase skill)
        {
            if (IsNext)
            {
               SelectedSkill = skill;
            }
        }

        public void setSkill(SkillBase skill, int position){
            if(position < 0 && position > 3){
                throw new System.Exception("Wrong position given!");
            }
            skills[position] = skill;
        }

        public abstract void Die();

        public void Select(){
            isSelected = true;
        }

         public void UnSelect(){
            isSelected = false;
        }

        public List<SkillBase> GetSkills(){
            return skills;
        }
    }
}
