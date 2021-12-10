using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

namespace Model {
    public delegate void SkillSelectedDelegate(SkillBase skill);

    public abstract class SkillBase : MonoBehaviour {
        [SerializeField]
        private Animation aniamtionClip;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private string name;
        [SerializeField]
        private int damage = 0;
        [SerializeField]
        private int heal = 0;
        [SerializeField]
        private float damageModifier = 1.0f;
        [SerializeField]
        private string description = null;
        [SerializeField]
        private bool disabled = true;
        [SerializeField]
        private List<int> validTargetsInTeam = new List<int>(4);
        [SerializeField]
        private List<int> validTargetsInEnemy = new List<int>(4);

        public int Damage { get => damage; }
        public int Heal { get => heal; }
        public float DamageModifier { get => damageModifier; }
        public string Description { get => description; }
        public bool Disabled { get => disabled; set => disabled = value; }
        public List<int> ValidTargetsInTeam { get => validTargetsInTeam; }
        public List<int> ValidTargetsInEnemy { get => validTargetsInEnemy; }
        public string Name { get => name; set => name = value; }
        public Animation AniamtionClip { get => aniamtionClip; set => aniamtionClip = value; }

        // This event notifies the Character Which Skill is casted currently
        public event SkillSelectedDelegate SkillSelected;

        // Effekt, ha van
        // public Effect effect;

   

        public void SelectSkill() {
            SkillSelected(this);
         }
    
        public abstract void CastSkill(Character caster, Character[] target);

        public Sprite GetIcon(){
          return icon;
        }
  }

}