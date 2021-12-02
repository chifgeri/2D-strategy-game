using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Model {
    [Serializable]
    public class Inventory
    {
        [SerializeField]
        private int size;
        [SerializeReference]
        private List<Item> items;

        public Inventory(int _size){
            size = _size;
            items = new List<Item>(_size);
            // Prefill with nulls
            for(int i = 0; i<=size; i++){
                items.Add(null);
            }
        }

        public List<Item> GetItems(){
            return items;
        }
        public void AddItem(Item i){
                if(i.Stackable){
                    int index = items.FindIndex(0, items.Count, delegate (Item item) {
                        if( item != null){
                            return item.GetItemType().Equals(i.GetItemType());
                        } else {
                            return false;
                        }
                    });
                    if(index >= 0){
                        items[index].Amount+=i.Amount;
                        return;
                    }
                }
                int idx = items.FindIndex(delegate (Item it){
                    return it == null;
                });

                if(idx > size-1 || idx < 0){
                    Debug.Log("[Inventory]: Size limit reached!");
                } else {
                    items[idx] = i;
                }
        }

        public void MoveItem(Item item, int from, int to, bool preventUnstack){
            if(to >= size || from < 0){
                return;
            }
            if(items.ElementAtOrDefault(to) != null){
                if(item.GetItemType().Equals(items[to].GetItemType()) && item.Stackable){
                    if(preventUnstack){
                        items[to].Amount+= item.Amount;
                        items[from] = null;
                    } else {
                        items[to].Amount++;
                        if(items[from].Amount > 1){
                            items[from].Amount--;
                        } else {
                            items[from] = null;
                        }
                    }
                } else {
                    items[from] = items[to];
                    items[to] = item;
                    
                }
            } else {
                // Destination is free
                if(item.Stackable && item.Amount > 1 && !preventUnstack){
                    items[from].Amount--;
                    items[to] = item.Clone(1);
                } else {
                    items[to] = item;
                    items[from] =  null;
                }

            }
        }

        public void RemoveItem(Item item, bool preventUnstack){
            int indx = items.FindIndex(delegate (Item i) {
                if(i != null){
                    return i.Equals(item);
                }
                return false;
            });
            if(indx < 0 || indx > size ){
                return;
            }
            if(item.Amount > 1 && !preventUnstack){
                items[indx].Amount--;
            } else {
                items[indx] = null;
            }
        }

        public bool isFull(){
            foreach(Item i in items){
                if(i == null){
                    return false;
                }
            }
            return true;
        }
    }

}
