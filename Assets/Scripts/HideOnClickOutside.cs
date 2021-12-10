using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HideOnClickOutside : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        HideIfClickedOutside();
    }
    private void HideIfClickedOutside()
    {
        var panel = gameObject;
        if (Input.GetMouseButtonDown(0) && panel.gameObject.activeInHierarchy)
        {
            var rectTranses = gameObject.GetComponentsInChildren<RectTransform>();
            var contains =  rectTranses.Any( rt => RectTransformUtility.RectangleContainsScreenPoint(
                rt,
                Input.mousePosition,
                Camera.main));
            if (!contains)
            {
                panel.SetActive(false);
            }
        }
    }
}
