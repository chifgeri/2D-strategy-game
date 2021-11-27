using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class TownUiController : MonoBehaviour
{
    [SerializeField]
    private ShopController shopController;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Shop"));
            if (hit.collider != null)
            {
                var shop = hit.collider.gameObject.GetComponent<ShopObject>();
                if (shop != null)
                {
                    shopController.ShowItems(shop.ShopName, shop.ShopType);
                }
            }
        }

    }
}