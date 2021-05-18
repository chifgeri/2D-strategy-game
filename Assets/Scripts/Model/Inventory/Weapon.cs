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
public class Weapon : Item
    {
        // Weapon type
        private WeaponType type;

        private int damage;

        public int Damage {
            get;
        }

        // Buffs

        public Weapon(WeaponType _type){
            this.type = _type;
            this.damage = (int)Mathf.Round(UnityEngine.Random.Range(5.0f, 15.0f));
        }

        override public void Use(){
            // Equip weapon
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
    }

}
