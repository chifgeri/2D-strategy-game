using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

    
    public delegate void CharacterChangedHandler(Character c);




    public abstract class Character : MonoBehaviour
    {
        public Animator animator;
        public bool isSelected = false;
        private int health = 100;
        public int level;

        private int speed;

        public int Speed;

        private int baseDamage;

        private int baseCrit;

        private int baseStunResist;

        private int dodgeChance;

        private int armorValue;

/*         public bool IsSelected {
            get;
        } */


        public SkillBase[] skillPrefabs = new SkillBase[4];

        private List<SkillBase> skills;

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
                 } else {
                     skills.Add(null);
                 }
            }
        }

        public void setSkill(SkillBase skill, int position){
            if(position < 0 && position > 3){
                throw new System.Exception("Wrong position given!");
            }
            skills[position] = skill;
        }

        public void useSkill(int position, Character[] targets){
            if(position < 0 && position > 3){
                throw new System.Exception("Wrong position given!");
            }

            skills[position].CastSkill(this, targets);
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
