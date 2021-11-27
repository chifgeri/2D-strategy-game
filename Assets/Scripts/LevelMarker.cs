using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMarker : MonoBehaviour
{
    // Start is called before the first frame update
    public int order = 0;
    [SerializeField]
    private TMP_Text tmpro;
    private int currentLevel;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    public void SetLevelNumber(int levelNumber, bool cleared)
    {
        currentLevel = levelNumber;
        tmpro.text = $"Level {currentLevel}";
        if (cleared)
        {
            GetComponent<SpriteRenderer>().color = new Color(142 / 256, 142 / 256, 142 / 256);
        } else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
    }

    public void Disable()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void OnClick()
    {
        if(MainStateManager.Instance != null)
        {
            MainStateManager.Instance.GameState.CurrentLevel = MainStateManager.Instance.Levels[currentLevel];
            MainStateManager.Instance.LoadCurrentLevel();
        }
    }
}
