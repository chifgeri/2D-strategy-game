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
        item = i;
        image.sprite = item.GetSprite();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FightTextManager.Instance.ShowItemDetails(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FightTextManager.Instance.DisableItemDetails();
    }
}
