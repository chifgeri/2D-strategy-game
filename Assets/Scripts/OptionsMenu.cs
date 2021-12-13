using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button previous;
    [SerializeField]
    private Button next;
    [SerializeField]
    private TMP_Text resolutionText;

    private int currentIndex = 2;


    private List<Resolution> resolutions = new List<Resolution>()
    {
        new Resolution()
        {
            width = 800,
            height = 600,
        },
        new Resolution()
        {
            width = 1280,
            height = 720,
        },
        new Resolution()
        {
            width = 1440,
            height = 900,
        },
         new Resolution()
        {
            width = 1600,
            height = 900,
        },

        new Resolution()
        {
            width = 1920,
            height = 1080,
        }
    };

    private void Awake()
    {
        resolutionText.text = $"{resolutions[currentIndex].width}x{resolutions[currentIndex].height}";
    }

    public void SetNextResolution()
    {
        if(currentIndex >= resolutions.Count - 1)
        {
            return;
        }

        currentIndex++;
        if(currentIndex == resolutions.Count - 1)
        {
            next.interactable = false;
        }
        if(previous.IsInteractable() == false)
        {
            next.interactable = false;
        }

        Screen.SetResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, 0);
        resolutionText.text = $"{resolutions[currentIndex].width}x{resolutions[currentIndex].height}";
    }

    public void SetPreviousResolution()
    {
        if (currentIndex <= 0)
        {
            return;
        }

        currentIndex--;
        if (next.IsInteractable() == false)
        {
            next.interactable = true;
        }
        if(currentIndex == 0)
        {
            previous.interactable = false;
        }

        Screen.SetResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, 0);
        resolutionText.text = $"{resolutions[currentIndex].width}x{resolutions[currentIndex].height}";
    }

}
