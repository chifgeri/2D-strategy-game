using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class PlayerCharacter : Character
    {
        private int experience;

        private Armor armor;

        private Weapon weapon;

        public void EquipWeapon(Item i){
            if(i is Weapon){
                weapon = (Weapon)i;
            }
        }

        public void EquipArmor(Item i){
            if(i is Armor){
                armor = (Armor)i;
            }
        }

        public void UnequipWeapon(){
            weapon = null;
        }

         public void UnequipArmor(){
            armor = null;
        }

        public int Experience {
            get { return experience; }
            set {
                if(value >= 0 && value <= 1000){
                    experience = value;
                }
            }
        }

        public Armor Armor { get => armor; }
        public Weapon Weapon { get => weapon; }
    }
}
