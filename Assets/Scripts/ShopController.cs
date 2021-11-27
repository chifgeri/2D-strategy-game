using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public enum ShopType
    {
        WeaponShop,
        ArmorShop,
    }

    public class ShopController : MonoBehaviour
    {
        [SerializeField]
        private InventorySlot inventorySlotPrefab;
        [SerializeField]
        private TMP_Text text;
        [SerializeField]
        private InventorySlot selectedItemSlot;
        [SerializeField]
        private Button buyButton;
        [SerializeField]
        private Button sellButton;

        private List<InventorySlot> slots = new List<InventorySlot>();

        private void Awake()
        {
            // y offset for the text row
            float xOffset = 24.0f;
            float yOffset = 48.0f;
            float size = 64.0f;
            float spacing = 16.0f;
            for (int i = 0; i < 15; i++)
            {
                var slot = Instantiate(inventorySlotPrefab, this.transform);
                var xPos = xOffset + ((i % 5) * size + size / 2.0f) + (i%5)*spacing;
                var yPos = -(yOffset + ((i / 5) * size) + size / 2.0f) - (i/5)*spacing;
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    xPos, yPos);
                slots.Add(slot);
            }

            foreach(var slot in slots)
            {
                AddEvent(slot, EventTriggerType.PointerClick, delegate { OnClick(slot); });
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

        private void OnClick(InventorySlot slot)
        {
            if(slot.GetItem() != null)
            {
                SelectItemToBuy(slot.GetItem());
            }
        }

        public void ShowItems(string shopName, ShopType shopType)
        {
            if (gameObject.activeInHierarchy)
            {
                DeactivatePanel();
            }
            gameObject.SetActive(true);
            text.text = shopName;
            List<Item> items = new List<Item>();
            switch (shopType)
            {
                case ShopType.WeaponShop:
                    items = ItemDictionary.weapons.Values.ToList();
                    break;
                case ShopType.ArmorShop:
                    items = ItemDictionary.armors.Values.ToList();
                    break;
            }

            int i = 0;
            foreach(var item in items)
            {
                if(i >= slots.Count)
                {
                    break;
                }
                slots[i].SetItem(item);
                i++;
            }
        }

        public void DeactivatePanel()
        {
            text.text = "";
            foreach(var slot in slots)
            {
                slot.SetItem(null);
            }
            selectedItemSlot.SetItem(null);
            gameObject.SetActive(false);
        }

        public void SelectItemToSell(Item item)
        {
            selectedItemSlot.SetItem(item);
            sellButton.gameObject.SetActive(true);
            if (buyButton.gameObject.activeInHierarchy)
            {
                buyButton.gameObject.SetActive(false);
            }

        }

        public void SelectItemToBuy(Item item)
        {
            selectedItemSlot.SetItem(item);
            buyButton.gameObject.SetActive(true);
            if (sellButton.gameObject.activeInHierarchy)
            {
                sellButton.gameObject.SetActive(false);
            }
        }

        public void BuyCurrentItem()
        {
            if(MainStateManager.Instance != null)
            {
                var money = MainStateManager.Instance.GameState.Money;

                if(money >= selectedItemSlot.GetItem().Price)
                {
                    MainStateManager.Instance.GameState.Money -= selectedItemSlot.GetItem().Price;
                    MainStateManager.Instance.GameState.Inventory.AddItem(selectedItemSlot.GetItem().Clone(1));
                } else
                {
                    // TODO: Message not enough money
                }
            }
        }

        public void SellCurrentItem()
        {
            if (MainStateManager.Instance != null)
            {
                MainStateManager.Instance.GameState.Money += selectedItemSlot.GetItem().Price;
                MainStateManager.Instance.GameState.Inventory.RemoveItem(selectedItemSlot.GetItem(), false);
                selectedItemSlot.SetItem(null);
                sellButton.gameObject.SetActive(false);
            }
        }
    }
}
