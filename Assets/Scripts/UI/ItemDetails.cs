using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemName;
    [SerializeField]
    private TMP_Text itemPrice;
    [SerializeField]
    private TMP_Text attributeName;
    [SerializeField]
    private TMP_Text attributeValue;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        { 
            gameObject.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5), Quaternion.identity);
        }
    }

    public void SetValues(Item item)
    {
        itemName.text = item.Name;
        itemPrice.text = item.Price.ToString();

        var attributes = item.GetItemAttributes();
        if(attributes != null)
        {
            Debug.Log(attributes.AttributeName);
            attributeName.text = attributes.AttributeName;
            attributeValue.text = attributes.ValueString;
        }
    }
}
