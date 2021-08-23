using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public delegate void WeaponEquippedHandler(Weapon weapon);

public delegate void WeaponUnequippedHandler();

public class InventoryController : Singleton<InventoryController>
{
    public event WeaponEquippedHandler WeaponEquipped;

    public event WeaponUnequippedHandler WeaponUnequipped;

    private PlayerCharacter selectedCharacter;
    
    public WeaponSlot weaponSlot;

    // public ArmorSlot armorSlot;

    public InventoryPanel inventoryPanel;

    private Inventory inventory = new Inventory(15);

    public Inventory Inventory {
            get {
                return inventory;
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        WeaponEquipped += weaponSlot.EquipWeapon;
        WeaponUnequipped += weaponSlot.UnequipWeapon;

        inventoryPanel.ItemDraggedOutside += weaponSlot.HandleMouseDrag;

        // For testing, this section should be deleted in production
        inventory.AddItem(new Weapon("Éles hosszú kard", WeaponType.LongSword, 1000));
        var saber = new Weapon("Szablya", WeaponType.Saber, 14000);
        inventory.AddItem(saber);
        inventory.AddItem(new Consumable("Kis életerő ital", ConsumableType.HealthPotion, 2, 30));
        inventory.AddItem(new Consumable("Kis életerő ital", ConsumableType.HealthPotion, 2, 30));        
    }

    public void CharacterChanged(Character c){
        if(c is PlayerCharacter){
            if(selectedCharacter != null){
                WeaponEquipped -= selectedCharacter.EquipWeapon;
            }
            selectedCharacter = (PlayerCharacter)c;
            WeaponEquipped += selectedCharacter.EquipWeapon;
        }
    }

    public bool UnequipWeapon(){
        if(selectedCharacter.weapon != null && !inventory.isFull()){
            Weapon temp = selectedCharacter.weapon;
            selectedCharacter.UnequipWeapon();
            inventory.AddItem(temp);
        } else {
            return false;
        }
        WeaponUnequipped();
        return true;
    }

     public void EquipWeapon(Weapon weapon){
         if(selectedCharacter == null){
             Debug.Log("Nincs karakter kijelölve.");
         } else {
            if(selectedCharacter.weapon != null){
                bool success = UnequipWeapon();
                if(success){
                    inventory.RemoveItem(weapon, false);
                    WeaponEquipped(weapon);
                }
            } else {
                inventory.RemoveItem(weapon, false);
                WeaponEquipped(weapon);
            }
         }
        UIOverlayManager.Instance.RefreshInventory(inventory);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
