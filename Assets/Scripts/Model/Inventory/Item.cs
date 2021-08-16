using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
public abstract class Item
    {
        private int amount;

        private int price;

        private bool stackable;

        private string name;

        public string Name {
            get;
            set;
        }

        public int Amount{
            get {
                return amount;
            }
            set {
                amount = value;
            }
        }
        public int Price {
            get;
            set;
        }

        public bool Stackable {
            get {
                return stackable;
            }
        }

        protected Item(string _name, int _price, int _amount, bool _stackable){
            name = _name;
            price = _price;
            amount = _amount;
            stackable = _stackable;
        }
        
        public abstract void Use();
        public abstract void Equip();

        public abstract Sprite GetSprite();

        public abstract System.Enum GetItemType();

        public abstract Item Clone(int amount);
    }
}
