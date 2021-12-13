using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingObject;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(MainStateManager.Instance != null && MainStateManager.Instance.IsLoading)
        {
            if (!loadingObject.activeInHierarchy)
            {
                loadingObject.SetActive(true);
            }
        } else  if (loadingObject.activeInHierarchy)
        {
            loadingObject.SetActive(false);
        }
    }
}
