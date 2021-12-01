using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTextManager : Singleton<ItemTextManager>
{
    [SerializeField]
    private ItemDetails itemDetails;
    // Start is called before the first frame update
    public void ShowItemDetails(Item i)
    {
        if (!itemDetails.gameObject.activeInHierarchy)
        {
            itemDetails.gameObject.SetActive(true);
            itemDetails.SetValues(i);
        }
    }


    public void DisableItemDetails()
    {
        if (itemDetails.gameObject.activeInHierarchy)
        {
            itemDetails.Clear();
            itemDetails.gameObject.SetActive(false);
        }
    }
}
