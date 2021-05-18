using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        public List<Item> GetItems(){
            return items;
        }
        public void AddItem(Item i){
            items.Add(i);
        }

    }

}
