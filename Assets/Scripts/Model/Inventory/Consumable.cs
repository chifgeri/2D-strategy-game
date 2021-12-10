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

        
        public Consumable(string _name, int _amount, int _price, ConsumableType _type) : base(_name, _price, _amount, true) {
            type = _type;
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
            return new Consumable(this.Name, amount, this.Price, this.type);
        }

        public override ItemAttribute GetItemAttributes()
        {
            return null;
        }

        public override void Use(PlayerCharacter target)
        {
            switch (type)
            {
                case ConsumableType.HealthPotion:
                    target.Heal(20);
                    MainStateManager.Instance.GameState.Inventory.RemoveItem(this, false);
                    break;
                case ConsumableType.DodgeBuff:
                    break;
            }
        }
    }

}