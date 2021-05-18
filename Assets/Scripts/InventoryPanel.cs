using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class InventoryPanel : MonoBehaviour
{
  public InventorySlot slotPrefab;

  private List<InventorySlot> slots;

  static int SLOT_HEIGHT = 80;

  static int SLOT_WIDTH = 80;

  private void Awake() {
    slots = new List<InventorySlot>();

    var rectTrans = this.gameObject.GetComponent<RectTransform>();
    for(int i=0; i<15; i++){
        var slot = Instantiate(slotPrefab,new Vector3(0,0,0), Quaternion.identity);

        var slotRectTrans = slot.GetComponent<RectTransform>();

        slotRectTrans.SetParent(rectTrans.transform, false);

        slotRectTrans.anchoredPosition = new Vector2(
                                        (i%5 * (float) SLOT_WIDTH) + SLOT_WIDTH/2,
                                        -(i/5 * (float) SLOT_HEIGHT) - SLOT_HEIGHT/2
                                        );

        slots.Add(slot);
    }
  }

  public void RefreshItems(Inventory inventory){
    // TODO: Refresh the sprites and text (amount) in the inventory slots
    var items = inventory.GetItems();
    int i = 0;
    foreach(var item in items){
        if(i>=15){
          break;
        }
        slots[i].SetImage(item.GetSprite());
        slots[i].SetAmount(item.Amount);
        i++;
    }

  }
}
