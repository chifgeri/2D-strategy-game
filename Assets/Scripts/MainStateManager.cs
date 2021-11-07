using Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStateManager : Singleton<MainStateManager>
{
    private GameState gameState;

    private List<Map> levels = new List<Map>();

    public GameState GameState { get => gameState; set => gameState = value; }

    // Start is called before the first frame update
    protected override void Awake()
    {
        if(MainStateManager.Instance != null)
        {
            return;
        }
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }

    public async void StartNewGame()
    {
        if(levels != null && levels.Count == 0)
        {
            await LoadLevels();
        }
        gameState = new GameState(levels[0], new List<Character>(), null, false, true, new Vector3(0, 0, 0));
    
        SceneManager.LoadSceneAsync("MapScene");
    }

    public async Task LoadLevels()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath);
        foreach(var fileInfo in dir.EnumerateFiles().Where(file => file.Name.Contains("level") && file.Extension.Equals(".json"))) {
            var map = await MapLoader.LoadData(fileInfo.FullName);
            levels.Add(map);
        }
        levels.OrderBy(l => l.LevelOrder);
    }

    public async Task  SaveGameState(string fileName)
    {
        var jsonData = JsonUtility.ToJson(gameState);
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+fileName, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);

        await sw.WriteAsync(jsonData);
        sw.Flush();
        sw.Close();
        fs.Close();
    }
    public async Task LoadGameState(string fileName)
    {
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+ fileName, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);

        string json = await sr.ReadToEndAsync();

        gameState = JsonUtility.FromJson<GameState>(json);

        sr.Close();
        fs.Close();

        return;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(gameState != null && (gameState.IsInFight || gameState.IsInMap))
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }



}
