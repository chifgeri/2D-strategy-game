using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class TilemapGridController : MonoBehaviour
{
    Tilemap[] tilemaps;
    public TMP_InputField input;

    private void Awake()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //  SaveLevel();
        // LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveLevel()
    {
        var tiles = ExtractTilemap.ExtractTilemapData(tilemaps);

        var map = new Map("TESZT", "123", new List<string>(tiles), new Vector2Int(0, 0), tilemaps[0].size.x, tilemaps[0].size.y);
        var text = input.text;
        if (text != null)
        {
            MapSaver.WriteData(JsonUtility.ToJson(map), Application.dataPath + $"/{text}.json");
        }
    }

    public async void LoadLevel()
    {
        var text = input.text;
        if (text != null)
        {
            Map level = await MapLoader.LoadData(Application.dataPath + $"/{text}.json");
            BuildTilemaps.SetUpTileMaps(tilemaps, level);
        }
    }
}
