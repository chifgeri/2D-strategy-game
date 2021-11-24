using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

public enum ArmorType {
    BasicArmor,
    KnightArmor,
    KnightArmor2
}
[Serializable]
public class Armor : Item
    {
        [SerializeField]
        private int armor;
        [SerializeField]
        private ArmorType type;

        public int ArmorValue { get => armor; }

        public Armor(string _name, int _armor, ArmorType _type, int _price) : base(_name, _price, 1, false) {
            armor = _armor;
            type = _type;
        }

          public override Item Clone(int amount){
            return new Armor(this.Name, this.armor, this.type, this.Price);
        }
        
        public override Enum GetItemType()
        {
            return type;
        }
        public override Sprite GetSprite()
        {
            switch(type){
                case ArmorType.KnightArmor:
                    return ArmorSprites.Instance.knightArmor;
                case ArmorType.KnightArmor2:
                    return ArmorSprites.Instance.knightArmor2;
                default:
                    return ArmorSprites.Instance.knightArmor;
            }
        }
        public override void Use()
        {
        throw new NotImplementedException();
        }

        public override void Equip()
        {
        throw new NotImplementedException();
        }

        public override ItemAttribute GetItemAttributes()
        {
            return new ItemAttribute()
                {
                    AttributeName = "Armor",
                    ValueString = armor.ToString()
                };
        }
    }
}
