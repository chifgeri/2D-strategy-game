using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        var canvas = GetComponent<Canvas>();
    }

    private void AddLine(ItemAttribute attribute, Vector2 position)
    {
        TMP_Text text;
    }
}
