using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
public abstract class Item 
    {
        int amount;

        int price;

        public int Amount;
        public int Price;
        
        public abstract void Use();

        public abstract Sprite GetSprite();

    }
}
