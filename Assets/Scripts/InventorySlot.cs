using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    private Image image;

    public void SetItem(Item i){
        item = i;
    }

    private void Awake(){
        image = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite){
        if(sprite != null){
            image.sprite = sprite;
        }else {
            image.sprite = null;
        }
    }

    public void SetAmount(int amount){

    }
}