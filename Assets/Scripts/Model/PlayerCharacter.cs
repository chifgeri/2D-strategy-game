using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class PlayerCharacter : Character
    {
        private int experience;

        public Armor armor;

        public Weapon weapon;

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


        public int Experience {
            get { return experience; }
            set {
                if(value >= 0 && value <= 1000){
                    experience = value;
                }
            }
        }

        override public void Die(){
            // TODO implement
        }
    }
}
