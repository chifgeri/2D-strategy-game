using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate bool ItemDraggedOutsideHandler(Vector2 mousePosition, Item item);

public class InventoryPanel : MonoBehaviour
{
  public InventorySlot slotPrefab;

  private List<InventorySlot> slots;

  private InventorySlot hoveredSlot;

  private GameObject mouseObject;

  static int SLOT_HEIGHT = 80;

  static int SLOT_WIDTH = 80;

  public event ItemDraggedOutsideHandler ItemDraggedOutside;

  private void Awake() {
    slots = new List<InventorySlot>();

    ItemDraggedOutside += WeaponSlot.Instance.HandleMouseDrag;

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

        AddEvent(slot, EventTriggerType.PointerEnter, delegate { OnEnter(slot); });
        AddEvent(slot, EventTriggerType.PointerExit, delegate { OnExit(slot); });
        AddEvent(slot, EventTriggerType.BeginDrag, delegate { OnDragStart(slot); });
        AddEvent(slot, EventTriggerType.EndDrag, delegate { OnDragEnd(slot); });
        AddEvent(slot, EventTriggerType.Drag, delegate { OnDrag(slot); });
    }
  }

  public void RefreshItems(Inventory inventory){
    var items = inventory.GetItems();
    int i = 0;
    foreach(var item in items){
        if(i>=15){
          break;
        }
        slots[i].SetItem(item);
        slots[i].SetImage(item.GetSprite());
        slots[i].SetAmount(item.Amount);
        i++;
    }
  }


  private void AddEvent(InventorySlot slot, EventTriggerType type, UnityAction<BaseEventData> action){
    EventTrigger trigger = slot.GetComponent<EventTrigger>();
    var eventTrigger = new EventTrigger.Entry();
    eventTrigger.eventID = type;
    eventTrigger.callback.AddListener(action);
    trigger.triggers.Add(eventTrigger);

  }

  private void OnEnter(InventorySlot slot){
    hoveredSlot = slot;
  }

  private void OnExit(InventorySlot slot){
    hoveredSlot = null;
  }
  private void OnDragStart(InventorySlot slot){
    mouseObject = new GameObject();
    var rt = mouseObject.AddComponent<RectTransform>();
    rt.sizeDelta = new Vector2(64, 64);
    mouseObject.transform.SetParent(transform.parent);

    if(slot.GetItem() != null){
      var img = mouseObject.AddComponent<Image>();
      img.sprite = slot.GetItem().GetSprite();
      img.raycastTarget = false;
    }    
  }

  private void OnDragEnd(InventorySlot slot){
    var item = slot.GetItem();
    if(item == null){
      Debug.Log("[Error]: Dragged item is null");
      return;
    }

    if( hoveredSlot != null && !hoveredSlot.Equals(slot)){
      var hoveredItem = hoveredSlot.GetItem();
      // With Left Ctrl the stackable items behave like not stackable
      if(item.Stackable && !Input.GetKey(KeyCode.LeftControl)){
        // On an empty slot the stacked items only deposit one item not the whole stack
        if( hoveredItem == null && item != null){
             if(item.Amount > 1){
            slot.SetItem(item.Clone(item.Amount-1));
          } else {
            slot.SetItem(null);
          }
          hoveredSlot.SetItem(item.Clone(1));
        } else {
          // Same type items can be stacked into each other
          if(hoveredItem.GetItemType().Equals(item.GetItemType()) && item != null){
            if(item.Amount > 1){
              slot.SetItem(item.Clone(item.Amount-1));
            } else {
              slot.SetItem(null);
            }
            hoveredSlot.SetItem(hoveredItem.Clone(hoveredItem.Amount+1));
          }
          else {
            slot.SetItem(hoveredSlot.GetItem());
            hoveredSlot.SetItem(item);
          }
        }
      } else {
        slot.SetItem(hoveredSlot.GetItem());
        hoveredSlot.SetItem(item);
      }
    } else {
        // Outside the panel we can eqip the items or something, else they are dropped (destroyed)

        bool actionSuccessful = ItemDraggedOutside(Input.mousePosition, item);
        if(!actionSuccessful){
          Vector2 localMousePosition = this.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
            if (!this.GetComponent<RectTransform>().rect.Contains(localMousePosition))
            {
              slot.SetItem(null);
          }
        }

    }

    Destroy(mouseObject);
    mouseObject = null;
  }

  private void OnDrag(InventorySlot slot){
    if(mouseObject != null){
      mouseObject.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
    }
  }

}
