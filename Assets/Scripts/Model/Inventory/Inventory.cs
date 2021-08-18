using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    public class Inventory
    {
        private int size;
        private List<Item> items;

        public Inventory(int _size){
            size = _size;
            items = new List<Item>(_size);
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

            UIOverlayManager.Instance.RefreshInventory(this);
        }

        public void MoveItem(Item item, int from, int to){
            if(to >= size || from < 0){
                return;
            }
            if(items.ElementAtOrDefault(to) != null){
                if(item.GetItemType().Equals(items[to].GetItemType()) && item.Stackable){
                    items[to].Amount++;
                    if(items[from].Amount > 1){
                        items[from].Amount--;
                    } else {
                        items[from] = null;
                    }
                } else {
                    items[from] = items[to];
                    items[to] = item;
                    
                }
            } else {
                // Destination is free
                if(item.Stackable && item.Amount > 1){
                    items[from].Amount--;
                    items[to] = item.Clone(1);
                } else {
                    items[to] = item;
                    items[from] =  null;
                }

            }
            Debug.Log("hooray");
            UIOverlayManager.Instance.RefreshInventory(this);
        }

    }

}
