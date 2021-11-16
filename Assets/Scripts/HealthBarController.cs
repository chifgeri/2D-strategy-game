using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image mask;
    float originalSize;

    private void Awake()
    {
        var images = this.GetComponentsInChildren<Image>();
        foreach (var image in images) {
            if (image.gameObject.name == "Mask")
            {
                mask = image;
            }
        }
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {				      
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
