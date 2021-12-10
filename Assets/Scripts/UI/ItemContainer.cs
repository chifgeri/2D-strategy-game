using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Item item;

    public Image image;
    // Start is called before the first frame update
    public void SetItem(Item i)
    {
        if(i == null)
        {
            item = i;
            image.color = new Color(1, 1, 1, 0);
            return;
        }
        item = i;
        image.sprite = item.GetSprite();
        image.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemTextManager.Instance.ShowItemDetails(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTextManager.Instance.DisableItemDetails();
    }
}
