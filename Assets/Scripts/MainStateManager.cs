using Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStateManager : Singleton<MainStateManager>
{
    private GameState gameState;

    private List<Map> levels = new List<Map>();

    private bool isLoading = false;

    private Round currentRound;

    public GameState GameState { get => gameState; set => gameState = value; }
    public Round CurrentRound { get => currentRound; set => currentRound = value; }
    public List<Map> Levels { get => levels; set => levels = value; }
    public bool IsLoading { get => isLoading; set => isLoading = value; }   

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
        IsLoading = true;
        if(levels != null && levels.Count == 0)
        {
            await LoadLevels();
        }
        gameState = new GameState(null, new List<PlayableData>()
        { new PlayableData("Geralt", System.Guid.NewGuid().ToString(), PlayableTypes.Axeman, 1, 100, 0, null, null, 1500),
          new PlayableData("Lehnard", System.Guid.NewGuid().ToString(), PlayableTypes.Paladin, 1, 100, 0, null, null, 2000),
        }, null, false, true, new Vector3(0, 0, 0), money: 10000, inventory: new Inventory(15));

        gameState.Inventory.AddItem(new Artifact("Egyedi", 2, 2000, ArtifactType.Necklace));
        gameState.Inventory.AddItem(new Consumable("Little heal potion", 3, 150, ConsumableType.HealthPotion));

        LoadScene("TownScene");
    }

    public void LoadCurrentLevel()
    {
        IsLoading = true;
        gameState.LastPosition = gameState.CurrentLevel.GroupPosition;
        LoadScene("MapScene");
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

    public void SaveGameState(string filename)
    {
        StartCoroutine(SaveGameStateRoutine(filename));
    }

    public IEnumerator SaveGameStateRoutine(string fileName)
    {
        IsLoading = true;
        if (gameState.IsInFight)
        {
            Dictionary<string, Character> dict = new Dictionary<string, Character>();
            List<PlayableData> playables = new List<PlayableData>();
            List<EnemyData> enemies = new List<EnemyData>();
            string current = null;

            // Get playable data
            foreach (var player in CurrentRound.PlayerGroup.Characters)
            {
                var guid = System.Guid.NewGuid().ToString();
                playables.Add(new PlayableData(player.Name, guid, player.Type, player.Level, player.Health, player.Experience, player.Weapon, player.Armor, player.Price));
                dict[guid] = player;
                if (player.IsNext)
                {
                    current = guid;
                }
            }
            // Get enemies data
            foreach (var enemy in CurrentRound.EnemyGroup.Characters)
            {
                var guid = System.Guid.NewGuid().ToString();
                enemies.Add(new EnemyData(enemy.Name, guid, enemy.Type, enemy.Level, enemy.Health));
                dict[guid] = enemy;
                if (enemy.IsNext)
                {
                    current = guid;
                }
            }
            // Get current order
            List<string> order = new List<string>();
            while(CurrentRound.CharacterOrder.Count > 0)
            {
                var character = CurrentRound.CharacterOrder.Dequeue();
                var value = dict.FirstOrDefault(x => x.Value.Equals(character)).Key ?? null;
                if (value != null)
                {
                    order.Add(value);
                }

            }
            gameState.FightData = new RoomState(playables, enemies, CurrentRound.RoundNumber, current, order);
            gameState.PlayableCharacters = playables;
        }

        var jsonData = JsonUtility.ToJson(gameState);
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+fileName, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);



        Task task =  sw.WriteAsync(jsonData);
        yield return new WaitUntil(() => task.IsCompleted);

        sw.Flush();
        sw.Close();
        fs.Close();
        IsLoading = false;
    }

    public void LoadGameState(string filename)
    {
        StartCoroutine(LoadGameStateRoutine(filename));
    }
    public IEnumerator LoadGameStateRoutine(string fileName)
    {
        IsLoading = true;
        
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+ fileName, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);

        Task<string> json = sr.ReadToEndAsync();

        yield return new WaitUntil(() => json.IsCompleted);

        gameState = JsonUtility.FromJson<GameState>(json.Result);

        if (gameState.IsInFight)
        {
            LoadScene("RoomScene");
        }

        if (gameState.IsInMap)
        {
            LoadScene("MapScene");
        }
        else
        {
            LoadScene("TownScene");
        }
        sr.Close();
        fs.Close();
    }

    public void LoadScene(string sceneName, float delay = 0.0f)
    {
        StartCoroutine(LoadSceneAfterDelaySec(sceneName, delay));
    }

    public void OnRoundWin()
    {
        var room = GameState.CurrentLevel.Rooms.Find(room => room.RoomId == GameState.CurrentRoomId);
        room.Cleared = true;

        if (room.LootItems.Count > 0)
        {
            foreach (Item item in room.LootItems)
            {
                GameState.Inventory.AddItem(item);
            }
        }

        UIOverlayManager.Instance.ShowLootItemsAndMoney(room.LootItems, room.LootMoney);
        GameState.Money += room.LootMoney;
        GameState.CurrentRoomId = 0;

        if (GameState.CurrentLevel.Rooms.Any(room => !room.Cleared))
        {
            GameState.IsInFight = false;
            GameState.IsInMap = true;
            GameState.CurrentRoomId = -1;
            StartCoroutine(LoadSceneAfterDelaySec("MapScene", 5.0f));
        }
        else
        {
            GameState.CurrentRoomId = -1;
            GameState.IsInFight = false;
            GameState.IsInMap = false;
            if (MessagePanel.Instance != null)
            {
                MessagePanel.Instance.ShowMessage("Congratulations! Level Cleared!");
            }
            GameState.CurrentLevel.Cleared = true;
            var level = levels.Find(level => level.LevelName == GameState.CurrentLevel.LevelName);
            level.Cleared = true;
            GameState.CurrentLevel = null;
            StartCoroutine(LoadSceneAfterDelaySec("TownScene", 5.0f));
        }
       
    }

    public void OnRoundLose()
    {
        GameState.PlayableCharacters = new List<PlayableData>();
        GameState.CurrentLevel = null;
        GameState.CurrentRoomId = -1;
        GameState.FightData = null;
        GameState.IsInFight = false;
        GameState.IsInMap = false;
        StartCoroutine(LoadSceneAfterDelaySec("TownScene", 5.0f));
    }

    public IEnumerator LoadSceneAfterDelaySec(string sceneName, float delay)
    {
        IsLoading = true;
        yield return new WaitForSeconds(delay);
        var task = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => task.isDone);
        IsLoading = false;
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
