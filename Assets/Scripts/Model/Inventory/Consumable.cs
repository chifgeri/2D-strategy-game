using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

    public enum ConsumableType {
        HealthPotion,
        DodgeBuff,

    }
    public class Consumable : Item
    {
        private ConsumableType type;

        
        public Consumable(ConsumableType _type){
            type = _type;
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
        public override Sprite GetSprite()
        {
             throw new System.NotImplementedException();
        }

    }

}