using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class PlayerCharacter : Character
    {
        private int experience;

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
