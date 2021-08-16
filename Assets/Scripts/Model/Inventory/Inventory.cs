using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class Inventory
    {
        private int size;
        private List<Item> items = new List<Item>();

        public Inventory(int _size){
            size = _size;
        }

        public List<Item> GetItems(){
            return items;
        }
        public void AddItem(Item i){
            if(items.Count < size-1){
                if(i.Stackable){
                    int index = items.FindIndex(0, items.Count, delegate (Item item) {
                        return item.GetItemType().Equals(i.GetItemType());
                    });
                    if(index >= 0){
                        items[index].Amount+=i.Amount;
                        return;
                    }
                }
                items.Add(i);
                
            } else {
                Debug.Log("[Inventory]: Size limit reached!");
            }
        }

    }

}
