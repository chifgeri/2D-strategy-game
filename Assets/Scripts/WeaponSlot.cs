using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class WeaponSlot : MonoBehaviour
{
    public void EquipWeapon(Weapon item) {
        Debug.Log(item.Name + "equipped");
    }

    public void UnequipWeapon() {
        Debug.Log("Unequipped");
    }

    public bool HandleMouseDrag(Vector2 mousePosition, Item item){
        Vector2 localMousePosition = this.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
        if (this.GetComponent<RectTransform>().rect.Contains(localMousePosition)){
           try
           {
               Weapon w = (Weapon)item;
              InventoryController.Instance.EquipWeapon(w);
           }
           catch (System.InvalidCastException)
           {
                // TODO: Show message to user
                Debug.Log("Cannot equip: not a weapon");
           }
           return true;
        }
        return false;
    }


}
