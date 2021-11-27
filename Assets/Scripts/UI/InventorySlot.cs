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

        private void Awake(){
            image = GetComponent<Image>();
        }

        public Item GetItem(){
            return item;
        }

        public void SetImage(Sprite sprite){
            if(sprite != null){
                image.sprite = sprite;
            }else {
                image.sprite = null;
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
            if (item != null && SceneManager.GetActiveScene().name == "RoomScene")
            {
                FightTextManager.Instance.ShowItemDetails(item);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SceneManager.GetActiveScene().name == "RoomScene")
            {
                FightTextManager.Instance.DisableItemDetails();
            }
        }
    }
}