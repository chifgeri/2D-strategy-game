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
            UI.Name.text = save1.Name.Split('.')[0]; ;
            UI.LastModDate.text = save1.LastWriteTime.ToString("yyyy.MM.dd HH:mm");
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
            UI.Name.text = save2.Name.Split('.')[0];
            UI.LastModDate.text = save2.LastWriteTime.ToString("yyyy.MM.dd HH:mm");
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
            UI.Name.text = save3.Name.Split('.')[0]; ;
            UI.LastModDate.text = save3.LastWriteTime.ToString("yyyy.MM.dd HH:mm");
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
            savedGame1.onClick.RemoveAllListeners();
            savedGame1.onClick.AddListener(() => { SaveOrLoadGameHandler(savedGame1.GetComponent<SavedGameUI>().Name.text); });
            savedGame2.onClick.RemoveAllListeners();
            savedGame2.onClick.AddListener(() => { SaveOrLoadGameHandler(savedGame2.GetComponent<SavedGameUI>().Name.text); });
            savedGame3.onClick.RemoveAllListeners();
            savedGame3.onClick.AddListener(() => { SaveOrLoadGameHandler(savedGame3.GetComponent<SavedGameUI>().Name.text); });
        }
        if (loadSaveType.Equals(LoadSaveType.Save))
        {
            title.text = "Save Game";
            savedGame1.onClick.RemoveAllListeners();
            savedGame1.onClick.AddListener(() => { SaveOrLoadGameHandler("save1"); });
            savedGame2.onClick.RemoveAllListeners();
            savedGame2.onClick.AddListener(() => { SaveOrLoadGameHandler("save2"); });
            savedGame3.onClick.RemoveAllListeners();
            savedGame3.onClick.AddListener(() => { SaveOrLoadGameHandler("save3"); });
        }
    }


    public void SaveOrLoadGameHandler(string name)
    {
        if (interactionType.Equals(LoadSaveType.Save))
        {
            MainStateManager.Instance.SaveGameState(name + ".json");
            ListSavedGames();
        }
        if (interactionType.Equals(LoadSaveType.Load) && !name.Equals("Empty"))
        {
           MainStateManager.Instance.LoadGameState(name + ".json");
        }
     }
}
