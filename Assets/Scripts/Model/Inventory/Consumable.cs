using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

    public enum ConsumableType {
        HealthPotion,
        DodgeBuff,

    }
    [Serializable]
    public class Consumable : Item
    {
        [SerializeField]
        private ConsumableType type;

        
        public Consumable(string _name, ConsumableType _type, int _amount, int _price): base(_name, _price, _amount, true) {
            type = _type;
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }

         public override void Equip()
        {
            return;
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

        public override Item Clone(int amount){
            return new Consumable(this.Name, this.type, amount, this.Price);
        }

    }

}