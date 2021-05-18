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

        
        public Consumable(ConsumableType _type, int _amount, int _price): base(_price, _amount, true) {
            type = _type;
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
        public override Sprite GetSprite()
        {
            switch(type){
                case ConsumableType.HealthPotion: 
                    return ConsumableSprites.Instance.healthPotion;
                case ConsumableType.DodgeBuff:
                    return ConsumableSprites.Instance.buffPotion;
                default:
                     return ConsumableSprites.Instance.healthPotion;
            }
        }

         public override System.Enum GetItemType()
        {
            return type;
        }

    }

}