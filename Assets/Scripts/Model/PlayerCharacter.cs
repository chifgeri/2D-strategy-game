using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{
    public delegate void CharacterChangedHandler(PlayerCharacter c);

    public delegate void CharacterNewSpellDelegate(Character c);
    public class PlayerCharacter : Character
    {
        public event CharacterNewSpellDelegate CharacterNewSpellEvent;

        private int experience;

        private Armor armor;

        private Weapon weapon;

        [SerializeField]
        private SkillBase[] skillPrefabs = new SkillBase[4];

        private List<SkillBase> skills;

        public SkillBase SelectedSkill { get; set; }

        public void EquipWeapon(Item i)
        {
            if (i is Weapon)
            {
                weapon = (Weapon)i;
            }
        }

        public void EquipArmor(Item i)
        {
            if (i is Armor)
            {
                armor = (Armor)i;
            }
        }

        public void UnequipWeapon()
        {
            weapon = null;
        }

        public void UnequipArmor()
        {
            armor = null;
        }

        public int Experience
        {
            get { return experience; }
            set
            {
                if (value >= 0 && value <= 1000)
                {
                    experience = value;
                }
            }
        }

        public Armor Armor { get => armor; }
        public Weapon Weapon { get => weapon; }

        protected override void Awake()
        {
            base.Awake();
            skills = new List<SkillBase>(4);
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

        public override void AttackAction(Character[] targets)
        {
            if (SelectedSkill)
            {
                SelectedSkill.CastSkill(this, targets);
                SelectedSkill = null;
                CharacterActionDoneInvoke();
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

        public void setSkill(SkillBase skill, int position)
        {
            if (position < 0 && position > 3)
            {
                throw new System.Exception("Wrong position given!");
            }
            skills[position] = skill;
        }

        public List<SkillBase> GetSkills()
        {
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

        public override void SetNext()
        {
            base.SetNext();
            this.EnableSkills();
        }
        public override void UnsetNext()
        {
            base.UnsetNext();
            this.DisableSkills();
        }
    }
}
