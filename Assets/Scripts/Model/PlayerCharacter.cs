using System;
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

        private int experience = 0;

        private Armor armor;

        private Weapon weapon;

        private PlayableTypes type;

        [SerializeField]
        private SkillBase[] skillPrefabs = new SkillBase[4];

        private List<SkillBase> skills;

        public SkillBase SelectedSkill { get; set; }

        private int price;

        [SerializeField]
        private ExperienceController XPPrefab;

        protected ExperienceController XPBar;

        public int Experience
        {
            get { return experience; }
            set
            {
                if (value >= 0 && value < 1000)
                {
                    experience = value;
                }
                if (value >= 1000)
                {
                    experience = 0;
                    LevelUp();
                }
            }
        }

        public Armor Armor { get => armor; }
        public Weapon Weapon { get => weapon; }
        public PlayableTypes Type { get => type; set => type = value; }
        public int Price { get => price; set => price = value; }


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

            if (XPPrefab != null)
            {
                XPBar = Instantiate(
                        XPPrefab,
                        new Vector3(
                            transform.position.x,
                            -(0.15f+Math.Abs(XPPrefab.GetComponent<RectTransform>().rect.height/2)
                            +Math.Abs(HBPrefab.GetComponent<RectTransform>().rect.height/2)),
                            3),
                         Quaternion.identity);
            }
            else
            {
                Debug.Log($"XPBar is NULL on {gameObject.name}");
            }
        }

        protected override void Update()
        {
            base.Update();

            XPBar.SetValue(Experience / 1000.0f);
        }

        public void EquipWeapon(Item i)
        {
         
            if (i is Weapon)
            {
                if (weapon != null)
                {
                    MainStateManager.Instance.GameState.Inventory.RemoveItem(i, true);
                    MainStateManager.Instance.GameState.Inventory.AddItem(weapon);
                }
                weapon = (Weapon)i;
            }
        }

        public void EquipArmor(Item i)
        {
            if (i is Armor)
            {
                if (armor != null)
                {
                    MainStateManager.Instance.GameState.Inventory.RemoveItem(i, true);
                    MainStateManager.Instance.GameState.Inventory.AddItem(armor);
                }
                armor = (Armor)i;
            }
        }

        public void UnequipWeapon()
        {
            if (weapon != null)
            {
                if (!MainStateManager.Instance.GameState.Inventory.isFull())
                { 
                    MainStateManager.Instance.GameState.Inventory.AddItem(weapon);
                    weapon = null;
                }
            }
        }

        public void UnequipArmor()
        {
            if (armor != null)
            {
                if (!MainStateManager.Instance.GameState.Inventory.isFull())
                {
                    MainStateManager.Instance.GameState.Inventory.AddItem(armor);
                    armor = null;
                }
            }
        }

   
        private void LevelUp()
        {

            // TODO: Animation, effect, text
            throw new NotImplementedException();
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
                skill.Disabled = false;
            }
        }

        public void DisableSkills()
        {
            foreach (var skill in skills)
            {
                skill.Disabled = true;
            }

        }

        public override int GetCurrentDamage()
        {
            return base.GetCurrentDamage() + (weapon?.Damage ?? 0);
        }

        public override int GetCurrentArmor()
        {
            return base.GetCurrentDamage() + (armor?.ArmorValue ?? 0);
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
