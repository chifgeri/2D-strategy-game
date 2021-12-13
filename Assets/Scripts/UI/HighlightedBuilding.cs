using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedBuilding : MonoBehaviour
{
    [SerializeField]
    private Sprite highlighted;
    [SerializeField]
    private Sprite nonHighlighted;

    // Start is called before the first frame update
    public void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = nonHighlighted;
    }

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().sprite = highlighted;
    }
}
