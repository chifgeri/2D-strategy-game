using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

public enum ArmorType {
    BasicArmor
}
public class Armor : Item
    {
        private int armor;

        private ArmorType type;

        public Armor(int _price, int _armor, ArmorType _type): base(_price, 1, false) {
            armor = _armor;
            type = _type;
        }

          public override Item Clone(int amount){
            return new Armor(this.Price, this.armor, this.type);
        }
        
        public override Enum GetItemType()
        {
        throw new NotImplementedException();
        }
        public override Sprite GetSprite()
        {
        throw new NotImplementedException();
        }
        public override void Use()
        {
        throw new NotImplementedException();
        }
    }
}
