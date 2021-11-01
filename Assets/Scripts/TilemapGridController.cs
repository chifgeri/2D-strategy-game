using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGridController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var tilemaps = GetComponentsInChildren<Tilemap>();
        var tiles = ExtractTilemap.ExtractTilemapData(tilemaps);

        var map = new Map("TESZT", "123", new List<string>(tiles), new Vector2Int(0, 0), tilemaps[0].size.x, tilemaps[0].size.y);

        MapSaver.WriteData(JsonUtility.ToJson(map));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
