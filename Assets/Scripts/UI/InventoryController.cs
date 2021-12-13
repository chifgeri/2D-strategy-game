using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public delegate void WeaponEquippedHandler(Weapon weapon);

public delegate void WeaponUnequippedHandler();

public delegate void ArmorEquippedHandler(Armor armor);

public delegate void ArmorUnequippedHandler();

public class InventoryController : Singleton<InventoryController>
{


    public event WeaponUnequippedHandler WeaponUnequipped;

    public event ArmorUnequippedHandler ArmorUnequipped;

    public event WeaponEquippedHandler WeaponEquipped;

    public event ArmorEquippedHandler ArmorEquipped;

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

    protected override void Awake()
    {
        base.Awake();
        var rectTrans = GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(1452, 612);

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
        inventoryPanel.ItemDoubleClick += this.HandleItemClick;

        inventory = MainStateManager.Instance.GameState.Inventory;
    }

    public void HandleItemClick(Item item)
    {
        if(item != null && selectedCharacter != null)
        {
            item.Use(selectedCharacter);
        }
    }

    public void CharacterChanged(Character c){
        if(c is PlayerCharacter){
            selectedCharacter = (PlayerCharacter)c;
            weaponSlot.RefreshSlot(selectedCharacter);
            armorSlot.RefreshSlot(selectedCharacter);
        }
    }

    public bool UnequipWeapon()
    {
        if (selectedCharacter != null)
        {
            selectedCharacter.UnequipWeapon();
        }
        else
        {
            return false;
        }
        WeaponUnequipped();
        return true;
    }

        public void EquipWeapon(Weapon weapon){
         if(selectedCharacter == null){
             Debug.Log("Nincs karakter kijelölve.");
         } else {
            bool success = selectedCharacter.EquipWeapon(weapon);
            if (success)
            {
                WeaponEquipped(weapon);
                MainStateManager.Instance.GameState.Inventory.RemoveItem(weapon, false);
            }
         }
    }

     public bool UnequipArmor(){
        if(selectedCharacter != null ){
            selectedCharacter.UnequipArmor();
        } else {
            return false;
        }
        ArmorUnequipped();
        return true;
    }

     public void EquipArmor(Armor armor){
        if (selectedCharacter == null)
        {
            Debug.Log("Nincs karakter kijelölve.");
        }
        else
        {
            bool success = selectedCharacter.EquipArmor(armor);
            if (success)
            {
                ArmorEquipped(armor);
                MainStateManager.Instance.GameState.Inventory.RemoveItem(armor, false);
            }
        }
    }
}
