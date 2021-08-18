using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public delegate void EquipWeaponHandler(Weapon weapon);

public class WeaponSlot : Singleton<WeaponSlot>
{
    public void EquipWeapon(Weapon item) {
        Debug.Log(item.Name + "equipped");
    }

    public bool HandleMouseDrag(Vector2 mousePosition, Item item){
        Vector2 localMousePosition = this.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
        if (this.GetComponent<RectTransform>().rect.Contains(localMousePosition)){
           try
           {
               Weapon w = (Weapon)item;
               EquipWeapon(w);
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
