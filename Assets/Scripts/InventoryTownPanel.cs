using Model;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryTownPanel : MonoBehaviour
{
    [SerializeField]
    private InventorySlot inventorySlotPrefab;
    [SerializeField]
    private ShopController shopController;
    private List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField]
    float xOffset = 36.0f;
    [SerializeField]
    float yOffset = 24.0f;
    [SerializeField]
    float size = 56.0f;
    [SerializeField]
    float xSpacing = 20.0f;
    [SerializeField]
    float ySpacing = 8.0f;


    // Start is called before the first frame update
    void Awake()
    {
        // y offset for the text row
        for (int i = 0; i < 15; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, this.transform);
            var rectTrans = slot.GetComponent<RectTransform>();
            rectTrans.sizeDelta = new Vector2(size, size);
            var xPos = xOffset + ((i % 5) * size + size / 2.0f) + (i % 5) * xSpacing;
            var yPos = -(yOffset + ((i / 5) * size) + size / 2.0f) - (i / 5) * ySpacing;
            rectTrans.anchoredPosition = new Vector2(
                xPos, yPos);
            slots.Add(slot);
        }

        foreach (var slot in slots)
        {
            AddEvent(slot, EventTriggerType.PointerClick, delegate { OnClick(slot); });
        }
    }

    private void OnClick(InventorySlot slot)
    {
        if (slot.GetItem() != null) {
            shopController.SelectItemToSell(slot.GetItem());
        }
    }

    private void AddEvent(InventorySlot slot, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = slot.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    // Update is called once per frame
    private void Update()
    {
        if (MainStateManager.Instance?.GameState?.Inventory != null)
        {
            RefreshItems(MainStateManager.Instance.GameState.Inventory);
        }
    }

    public void RefreshItems(Inventory inventory)
    {
        var items = inventory.GetItems();
        int i = 0;
        foreach (var item in items)
        {
            if (i >= 15)
            {
                break;
            }
            slots[i].SetItem(item);
            if (item != null)
            {
                slots[i].SetImage(item.GetSprite());
                slots[i].SetAmount(item.Amount);
            }
            i++;
        }
    }
}
