using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Model {

    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Item item;
    
        public int Index {
            get;
            set;
        }
        [SerializeField]
        private Image image;

        public void SetItem(Item i){
            item = i;
            if(i != null){
                SetImage(i.GetSprite());
                SetAmount(i.Amount);
            } else {
                SetImage(null);
                SetAmount(0);
            }
        }

        public Item GetItem(){
            return item;
        }

        public void SetImage(Sprite sprite){
            if(sprite != null){
                image.sprite = sprite;
                image.color = new Color(1, 1, 1, 1);
            }else {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
            }
        }

        public void SetAmount(int amount){
            var textfield = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();

            if(textfield != null){
                if(amount > 1){
                    textfield.text = amount.ToString();
                } else {
                    textfield.text = "";
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (item != null && ItemTextManager.Instance != null)
            {
                ItemTextManager.Instance.ShowItemDetails(item);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (ItemTextManager.Instance != null)
            {
                ItemTextManager.Instance.DisableItemDetails();
            }
        }
    }
}