using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public delegate void EquipWeaponHandler(Weapon weapon);

public class WeaponSlot : Singleton<WeaponSlot>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipWeapon(Weapon item) {
        Debug.Log(item.Name + "equipped");
    }

    public bool HandleMouseDrag(Vector2 mousePosition, Item item){
        Vector2 localMousePosition = this.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
        if (this.GetComponent<RectTransform>().rect.Contains(localMousePosition)){
            item.Equip();
            return true;
        }
        return false;
    }


}
