using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

    public enum WeaponType {
        LongSword,
        CurvedSword,
        Saber,
        KnightSword
    }

    [Serializable]
    public class Weapon : Item
        {
            // Weapon type
            [SerializeField]
            private WeaponType type;
            [SerializeField]
            private int damage;

            public int Damage {
                get;
            }
            // Buffs

            public Weapon(string _name, WeaponType _type, int _price): base(_name, _price, 1, false){
                this.type = _type;
                this.damage = (int)Mathf.Round(UnityEngine.Random.Range(5.0f, 15.0f));
            }

            override public void Use(){
                return;
            }

            override public void Equip(){
                return;
            }

            public override Sprite GetSprite()
            {
                switch(type){
                    case WeaponType.LongSword:
                         return WeaponSprites.Instance.longSword;
                    case WeaponType.Saber:
                        return WeaponSprites.Instance.Saber;
                    case WeaponType.CurvedSword:
                        return WeaponSprites.Instance.curvedSword;
                    case WeaponType.KnightSword:
                        return WeaponSprites.Instance.KnightSword;
                    default: 
                        return null;
                }
            }

        public override Enum GetItemType()
        {
          return type;
        }

        public override Item Clone(int amount = 1){
                return new Weapon(this.Name, this.type, this.Price);
            }

        public override List<ItemAttribute> GetItemAttributes()
        {
            return new List<ItemAttribute>()
            {
                new ItemAttribute()
                {
                    AttributeName = "Damage",
                    ValueString = Damage.ToString()
                },
                new ItemAttribute()
                {
                    AttributeName = "Name",
                    ValueString = Name.ToString()
                },
                new ItemAttribute()
                {
                    AttributeName = "Price",
                    ValueString = Price.ToString()
                },
                new ItemAttribute()
                {
                    AttributeName = "Stackable",
                    ValueString = Stackable ? "Yes" : "No"
                }

            };
        }
    }


}
