using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System.Threading.Tasks;

public enum LoadSaveType
{
    Save,
    Load,
}


public class LoadSavedGamesMenu : MonoBehaviour
{
    public Button savedGame1;
    public Button savedGame2;
    public Button savedGame3;

    public TMPro.TextMeshProUGUI title;

    private LoadSaveType interactionType;

    private void Awake()
    {
        savedGame1.onClick.AddListener(async () => { await SaveOrLoadGameHandler(savedGame1.GetComponent<SavedGameUI>().Name.text); });
        savedGame2.onClick.AddListener(async () => { await SaveOrLoadGameHandler(savedGame2.GetComponent<SavedGameUI>().Name.text); });
        savedGame3.onClick.AddListener(async () => { await SaveOrLoadGameHandler(savedGame3.GetComponent<SavedGameUI>().Name.text); });
    }

    public void ListSavedGames()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        IEnumerable<FileInfo> fileInfos = dir.EnumerateFiles();
        var files = fileInfos.Where(file => file.Name.Contains("save") && file.Extension.Equals(".json")).ToList();
        var save1 = files.Where(f => f.Name.Contains("save1")).FirstOrDefault();

        if(save1 != null)
        {
            var UI = savedGame1.GetComponent<SavedGameUI>();
            UI.Name.text = save1.Name;
            UI.LastModDate.text = save1.CreationTime.ToString("yyyy.MM.dd HH:mm");
        } 
        else
        {
            var UI = savedGame1.GetComponent<SavedGameUI>();
            UI.Name.text = "Empty";
            UI.LastModText.text = "";
            UI.LastModDate.text = "";
        }

        var save2 = files.Where(f => f.Name.Contains("save2")).FirstOrDefault();
        if (save2 != null)
        {
            var UI = savedGame2.GetComponent<SavedGameUI>();
            UI.Name.text = save2.Name;
            UI.LastModDate.text = save2.CreationTime.ToString("yyyy.MM.dd HH:mm");
        }
        else
        {
            var UI = savedGame2.GetComponent<SavedGameUI>();
            UI.Name.text = "Empty";
            UI.LastModText.text = "";
            UI.LastModDate.text = "";
        }
        var save3 = files.Where(f => f.Name.Contains("save3")).FirstOrDefault();
        if (save3 != null)
        {
            var UI = savedGame3.GetComponent<SavedGameUI>();
            UI.Name.text = save3.Name;
            UI.LastModDate.text = save3.CreationTime.ToString("yyyy.MM.dd HH:mm");
        }
        else
        {
            var UI = savedGame3.GetComponent<SavedGameUI>();
            UI.Name.text = "Empty";
            UI.LastModText.text = "";
            UI.LastModDate.text = "";
        }

    }

    public void SetInteractionType(LoadSaveType loadSaveType)
    {
        interactionType = loadSaveType;
        if (loadSaveType.Equals(LoadSaveType.Load)){
            title.text = "Load Saved Game";
        }
        if (loadSaveType.Equals(LoadSaveType.Save))
        {
            title.text = "Save Game";
        }
    }


    public async Task SaveOrLoadGameHandler(string name)
    {
        if (interactionType.Equals(LoadSaveType.Save))
        {
            await MainStateManager.Instance.SaveGameState(name + ".json");
        }
        if (interactionType.Equals(LoadSaveType.Load) && !name.Equals("Empty"))
        {
           await MainStateManager.Instance.LoadGameState(name + ".json");
        }

    }
}
