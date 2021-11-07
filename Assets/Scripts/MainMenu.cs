using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button loadSaveButton;
    public LoadSavedGamesMenu loadSaveMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        var buttonText = loadSaveButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (MainStateManager.IsInitialized && MainStateManager.Instance.GameState != null)
        {
            if(buttonText != null)
            {
                buttonText.text = "Save Game";
            }
            loadSaveButton.onClick.RemoveAllListeners();
            loadSaveButton.onClick.AddListener(() =>
            {
                loadSaveMenu.gameObject.SetActive(true);
                loadSaveMenu.SetInteractionType(LoadSaveType.Save);
            });
        } 
        else
        {
            if (buttonText != null)
            {
                buttonText.text = "Load Game";
            }
            loadSaveButton.onClick.RemoveAllListeners();
            loadSaveButton.onClick.AddListener(() =>
            {
                loadSaveMenu.gameObject.SetActive(true);
                loadSaveMenu.SetInteractionType(LoadSaveType.Load);
            });
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(SceneManager.sceneCount > 1)
            {
                if(MainStateManager.Instance.GameState != null && MainStateManager.Instance.GameState.IsInMap)
                {
                    SceneManager.LoadScene("MapScene");
                }

                if (MainStateManager.Instance.GameState != null && MainStateManager.Instance.GameState.IsInFight)
                {
                    SceneManager.LoadScene("RoomScene");
                }
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
