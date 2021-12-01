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

    private Round currentRound;

    public GameState GameState { get => gameState; set => gameState = value; }
    public Round CurrentRound { get => currentRound; set => currentRound = value; }
    public List<Map> Levels { get => levels; set => levels = value; }

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
        gameState = new GameState(null, new List<PlayableData>()
        { new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Axeman, 1, 100, 0, null, null, 1500),
          new PlayableData(System.Guid.NewGuid().ToString(), PlayableTypes.Paladin, 1, 100, 0, null, null, 2000),
        }, null, false, true, new Vector3(0, 0, 0), money: 10000, inventory: new Inventory(15));
        SceneManager.LoadSceneAsync("TownScene");

    }

    public void LoadCurrentLevel()
    {
        gameState.LastPosition = gameState.CurrentLevel.GroupPosition;
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

    public async Task SaveGameState(string fileName)
    {
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
                playables.Add(new PlayableData(guid, player.Type, player.Level, player.Health, player.Experience, player.Weapon, player.Armor, player.Price));
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
                enemies.Add(new EnemyData(guid, enemy.Type, enemy.Level, enemy.Health));
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

        if (gameState.IsInFight)
        {
            SceneManager.LoadScene("RoomScene");
        }

        if (gameState.IsInMap)
        {
            SceneManager.LoadScene("MapScene");
        }

        sr.Close();
        fs.Close();

        return;
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
            StartCoroutine(LoadLevelAfterDelay("MapScene", 5.0f));
            GameState.IsInFight = false;
            GameState.IsInMap = true;
        } else
        {
            GameState.IsInFight = false;
            GameState.IsInMap = false;
            // Display Level cleared text
            GameState.CurrentLevel.Cleared = true;
            var level = levels.Find(level => level.LevelName == GameState.CurrentLevel.LevelName);
            level.Cleared = true;
            GameState.CurrentLevel = null;
            LoadLevelAfterDelay("TownScene", 5.0f);
        }
       
    }

    public void OnRoundLose()
    {
        GameState.PlayableCharacters = new List<PlayableData>();
        GameState.CurrentLevel = null;
        GameState.CurrentRoomId = 0;
        GameState.FightData = null;
        GameState.IsInFight = false;
        GameState.IsInMap = false;
        LoadLevelAfterDelay("TownScene", 5.0f);
    }

    IEnumerator LoadLevelAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
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
