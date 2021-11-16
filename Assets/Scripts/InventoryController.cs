﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public delegate void WeaponEquippedHandler(Weapon weapon);

public delegate void WeaponUnequippedHandler();

public delegate void ArmorEquippedHandler(Armor armor);

public delegate void ArmorUnequippedHandler();

public class InventoryController : Singleton<InventoryController>
{
    public event WeaponEquippedHandler WeaponEquipped;

    public event WeaponUnequippedHandler WeaponUnequipped;

    public event ArmorEquippedHandler ArmorEquipped;

    public event ArmorUnequippedHandler ArmorUnequipped;

    private PlayerCharacter selectedCharacter;
    
    public WeaponSlot weaponSlot;

    public ArmorSlot armorSlot;

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

        ArmorEquipped += armorSlot.EquipArmor;
        ArmorUnequipped += armorSlot.UnequipArmor;

        inventoryPanel.ItemDraggedOutside += armorSlot.HandleMouseDrag;

        // For testing, this section should be deleted in production
        inventory.AddItem(new Weapon("Éles hosszú kard", WeaponType.LongSword, 1000));
        inventory.AddItem(new Weapon("Szablya", WeaponType.Saber, 14000));
        inventory.AddItem(new Weapon("Lovagi kard", WeaponType.KnightSword, 14000));
        inventory.AddItem(new Weapon("Görbe kard", WeaponType.CurvedSword, 14000));
        inventory.AddItem(new Armor("Kis páncél", 12000, 20, ArmorType.KnightArmor));
        inventory.AddItem(new Armor("Isteni páncél", 12000, 20, ArmorType.KnightArmor2));
        inventory.AddItem(new Consumable("Kis életerő ital", ConsumableType.HealthPotion, 2, 30));
        inventory.AddItem(new Consumable("Kis életerő ital", ConsumableType.HealthPotion, 2, 30));        
    }

    public void CharacterChanged(Character c){
        if(c is PlayerCharacter){
            if(selectedCharacter != null){
                WeaponEquipped -= selectedCharacter.EquipWeapon;
                ArmorEquipped -= selectedCharacter.EquipArmor;
            }
            selectedCharacter = (PlayerCharacter)c;
            WeaponEquipped += selectedCharacter.EquipWeapon;
            ArmorEquipped += selectedCharacter.EquipArmor;
            weaponSlot.RefreshSlot(selectedCharacter);
            armorSlot.RefreshSlot(selectedCharacter);
        }
    }

    public bool UnequipWeapon(){
        if(selectedCharacter.Weapon != null && !inventory.isFull()){
            Weapon temp = selectedCharacter.Weapon;
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
            if(selectedCharacter.Weapon != null){
                bool success = UnequipWeapon();
                if(success){
                    inventory.RemoveItem(weapon, false);
                    WeaponEquipped(weapon);
                } else {
                    inventory.RemoveItem(weapon, false);
                    inventory.AddItem(selectedCharacter.Weapon);
                    WeaponEquipped(weapon);
                }
            } else {
                inventory.RemoveItem(weapon, false);
                WeaponEquipped(weapon);
            }
         }
    }

     public bool UnequipArmor(){
        if(selectedCharacter.Armor != null && !inventory.isFull()){
            Armor temp = selectedCharacter.Armor;
            selectedCharacter.UnequipArmor();
            inventory.AddItem(temp);
        } else {
            return false;
        }
        ArmorUnequipped();
        return true;
    }

     public void EquipArmor(Armor armor){
         if(selectedCharacter == null){
             Debug.Log("Nincs karakter kijelölve.");
         } else {
            if(selectedCharacter.Armor != null){
                bool success = UnequipArmor();
                if(success){
                    inventory.RemoveItem(armor, false);
                    ArmorEquipped(armor);
                } else {
                    inventory.RemoveItem(armor, false);
                    inventory.AddItem(selectedCharacter.Armor);
                    ArmorEquipped(armor);
                }
            } else {
                inventory.RemoveItem(armor, false);
                ArmorEquipped(armor);
            }
         }
    }
}
