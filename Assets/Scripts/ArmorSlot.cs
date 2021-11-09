using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;

public class ArmorSlot : MonoBehaviour
{

    private Image armorImage;

    private void Awake() {
        armorImage = GetComponent<Image>();
    }
    public void EquipArmor(Armor item) {
        switch(item.GetItemType()){
            case ArmorType.KnightArmor:
                armorImage.sprite = ArmorSprites.Instance.knightArmor;
                break;
             case ArmorType.KnightArmor2:
                armorImage.sprite = ArmorSprites.Instance.knightArmor2;
                break;
             default:
                armorImage.sprite = ArmorSprites.Instance.knightArmor;
                break;
        } 
    }

    public void UnequipArmor() {
        armorImage.sprite = null;
    }

    public void RefreshSlot(PlayerCharacter c){
        if(c.Armor != null){
            EquipArmor(c.Armor);
        } else {
            UnequipArmor();
        }
    }

    public bool HandleMouseDrag(Vector2 mousePosition, Item item){
        Vector2 localMousePosition = this.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
        if (this.GetComponent<RectTransform>().rect.Contains(localMousePosition)){
           if(item is Armor)
           {
              Armor arm = (Armor)item;
              InventoryController.Instance.EquipArmor(arm);
           } else {
            // TODO: Show message to user
             Debug.Log("Cannot equip: not a armor");
           }
           return true;
        }
        return false;
    }


}
