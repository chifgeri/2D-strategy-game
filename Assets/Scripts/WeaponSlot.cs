using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{

    private Image weaponImage;

    private void Awake() {
        weaponImage = GetComponent<Image>();
    }
    public void EquipWeapon(Weapon item) {
        switch(item.GetItemType()){
            case WeaponType.CurvedSword:
                weaponImage.sprite = WeaponSlotSprites.Instance.curvedSword;
                break;
             case WeaponType.KnightSword:
                weaponImage.sprite = WeaponSlotSprites.Instance.KnightSword;
                break;
             case WeaponType.LongSword:
                weaponImage.sprite = WeaponSlotSprites.Instance.longSword;
                break;
             case WeaponType.Saber:
                weaponImage.sprite = WeaponSlotSprites.Instance.Saber;
                break;
        }
    }

    public void UnequipWeapon() {
        weaponImage.sprite = null;
    }

    public void RefreshSlot(PlayerCharacter c){
        if(c.weapon != null){
            EquipWeapon(c.weapon);
        } else {
            UnequipWeapon();
        }
    }

    public bool HandleMouseDrag(Vector2 mousePosition, Item item){
        Vector2 localMousePosition = this.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
        if (this.GetComponent<RectTransform>().rect.Contains(localMousePosition)){
           if(item is Weapon){
               Weapon w = (Weapon)item;
              InventoryController.Instance.EquipWeapon(w);
           } else {
                // TODO: Show message to user
                Debug.Log("Cannot equip: not a weapon");
           }
           return true;
        }
        return false;
    }


}
