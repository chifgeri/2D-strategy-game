using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TownUiController : MonoBehaviour
{
    [SerializeField]
    private ShopController shopController;
    [SerializeField]
    private InventoryTownPanel inv;
    [SerializeField]
    private GameObject heroShop;
    [SerializeField]
    private TMP_Text moneyText;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Shop"));
            if (hit.collider != null)
            {

                var shop = hit.collider.gameObject.GetComponent<HeroShop>();
                if (shop != null)
                {
                    heroShop.SetActive(true);
                    return;
                }
               
                var shop2 = hit.collider.gameObject.GetComponent<ShopObject>();
                if (shop2 != null)
                {
                    shopController.ShowItems(shop2.ShopName, shop2.ShopType);
                    inv.gameObject.SetActive(true);
                }
            }
        }

        if (MainStateManager.Instance?.GameState?.Money != null)
        {
            moneyText.text = MainStateManager.Instance?.GameState?.Money.ToString();
        }

    }
}
