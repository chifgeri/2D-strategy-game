using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {

    [Serializable]
    public abstract class Item
        {
            [SerializeField]
            private int amount;
            [SerializeField]
            private int price;
            [SerializeField]
            private bool stackable;
            [SerializeField]
            private string name;


            public int Amount{
                get {
                    return amount;
                }
                set {
                    amount = value;
                }
            }

            public bool Stackable {
                get {
                    return stackable;
                }
            }

            public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }

        protected Item(string _name, int _price, int _amount, bool _stackable){
                Name = _name;
                price = _price;
                amount = _amount;
                stackable = _stackable;
            }
        
            public abstract void Use();
            public abstract void Equip();

            public abstract Sprite GetSprite();

            // Attributes to display in item hint
            public abstract ItemAttribute GetItemAttributes();

            public abstract System.Enum GetItemType();

            public abstract Item Clone(int amount);
    }
}
