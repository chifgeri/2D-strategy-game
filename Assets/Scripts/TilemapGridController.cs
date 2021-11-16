using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System;

public class TilemapGridController : Singleton<TilemapGridController>
{
    Map level;
    Tilemap[] tilemaps;
    public TMP_InputField input;
    public TMP_InputField minLvlInput;

    public Map Level { get => level; }

   protected override void Awake()
    {
        base.Awake();
        tilemaps = GetComponentsInChildren<Tilemap>();
    }

    private void Start()
    {
        if (MainStateManager.IsInitialized)
        {
            level = MainStateManager.Instance.GameState.CurrentLevel;
            BuildTilemaps.SetUpTileMaps(tilemaps, MainStateManager.Instance.GameState.CurrentLevel);
            MainStateManager.Instance.GameState.IsInMap = true;
            MainStateManager.Instance.GameState.IsInFight = false;

        }
    }

    public void SaveLevel()
    {
        var tiles = ExtractTilemap.ExtractTilemapData(tilemaps);

        var text = input.text;
        var minLvl = minLvlInput.text;
        Int32.TryParse(minLvl, out int minLevel);
        if (text != null && minLevel != 0)
        {
            var map = new Map("TESZT", new List<string>(tiles), new Vector2Int(0, 0), tilemaps[0].size.x, tilemaps[0].size.y, minLevel, null, 1);
            MapSaver.WriteData(JsonUtility.ToJson(map), Application.dataPath + $"/{text}.json");
        }
    }

    public async void LoadLevel()
    {
        var text = input.text;
        if (text != null)
        {
            level = await MapLoader.LoadData(Application.dataPath + $"/{text}.json");
            BuildTilemaps.SetUpTileMaps(tilemaps, level);

            List<Vector2Int> doorPos = new List<Vector2Int>();

            if (level.Rooms.Count == 0)
            {
                var bounds = tilemaps[3].cellBounds;

                var xmin = bounds.xMin;
                var xmax = bounds.xMax;
                var ymin = bounds.yMin;
                var ymax = bounds.yMax;

                // Go through the layer's tiles and save the corresponding Type strings
                for (int i = ymin; i < ymax; i++)
                {
                    for (int j = xmin; j < xmax; j++)
                    {
                        var tile = tilemaps[3].GetTile(new Vector3Int(j, i, 2));

                        if (tile != null && (tile.name == TileReferences.Instance.verticalDoor.name || tile.name == TileReferences.Instance.horizontalDoor.name))
                        {
                            doorPos.Add(new Vector2Int(j, i));
                        }
                    }
                }

                FillMapWithRooms(level, doorPos);

                MapSaver.WriteData(JsonUtility.ToJson(level), Application.dataPath + $"/{text}.json");
            }
        }
    }

    private void FillMapWithRooms(Map map, List<Vector2Int> doorPos)
    {
        List<EnemyTypes> enemyTypes = new List<EnemyTypes>()
        {
            EnemyTypes.Golem,
            EnemyTypes.Orc,
            EnemyTypes.Knight,
            EnemyTypes.Skeleton,
            EnemyTypes.Zombie
        };
        int minLvl = map.LevelRequirement - 2 < 1 ? 1 : map.LevelRequirement - 2;
        int maxLvl = map.LevelRequirement + 1;

        List<Room> rooms = new List<Room>();
        int id = 0;

        foreach (var pos in doorPos)
        {
            int enemyCount = UnityEngine.Random.Range(2, 5);
            List<EnemyData> enemies = new List<EnemyData>();
            for (int i = 0; i < enemyCount; i++)
            {
                enemies.Add(new EnemyData(System.Guid.NewGuid().ToString(), enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count - 1)], UnityEngine.Random.Range(minLvl, maxLvl+1), 100));
            }
            rooms.Add(new Room(id, enemies, new List<Item>(), UnityEngine.Random.Range(100, map.LevelRequirement * 1000), pos, false));

            id++;
        }
        map.Rooms = rooms;
    }

}
