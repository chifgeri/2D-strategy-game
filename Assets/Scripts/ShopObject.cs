using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopObject : MonoBehaviour
{
    [SerializeField]
    private ShopType shopType;
    [SerializeField]
    private string shopName;

    public ShopType ShopType { get => shopType; set => shopType = value; }
    public string ShopName { get => shopName; set => shopName = value; }
}
