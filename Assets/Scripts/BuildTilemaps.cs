using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Model;

public static class TileTypes {

    public static readonly string VerticalWall = "VW";
    public static readonly string HorizontalWall = "HW";
    public static readonly string CornerWall = "CW";
    public static readonly string Ground = "G";
    public static readonly string Potion = "P";
    public static readonly string Treasure = "T";
    public static readonly string VerticalDoor = "VD";
    public static readonly string HorizontalDoor = "HD";
    public static readonly string NonTile = "N";
}


public class BuildTilemaps : MonoBehaviour
{
    // Multi layer tilemap
    // With 4 layers
    // 1 = Ground
    // 2 = Walls
    // 3 = Objects
    // 4 = Doors (Rooms)
    private Tilemap[] tilemaps;
    private Map level;

    public Tile groundTile;
    public Tile horizontalWallTile;
    public Tile verticalWallTile;
    public Tile cornerTile;
    public Tile verticalDoorTile;
    public Tile horizontalDoorTile;

    private async void Awake()
    {
       tilemaps = GetComponentsInChildren<Tilemap>();
        level = await MapLoader.LoadData("/test.json");
        SetUpTileMaps();
    }

    private void SetUpTileMaps()
    {
        int i = 0;
        Vector3Int origin = new Vector3Int(0, 0, 0);

        foreach (var tilemap in tilemaps) { 
            var cellSize = tilemap.cellSize;
            tilemap.ClearAllTiles();
            var currentCellPosition = origin;
            var width = level.Width;
            var height = level.Height;
            for (var h = 0; h < height; h++)
            {
                for (var w = 0; w < width; w++)
                {
                    // Ground layer
                    if(i == 0)
                    {
                       SetGroundTile(tilemap, currentCellPosition, w, h);
                    }
                    if(i == 1)
                    {
                       SetWallTile(tilemap, currentCellPosition, w, h);
                    }
                    if (i == 2)
                    {
                       // SetObjectTile(tilemap, currentCellPosition, w, h);
                    }
                    if(i == 3)
                    {
                       // SetDoorTile(tilemap, currentCellPosition, w, h);
                    }
                    currentCellPosition = new Vector3Int(
                        (int)(cellSize.x + currentCellPosition.x),
                        currentCellPosition.y, origin.z);
                }
                currentCellPosition = new Vector3Int(origin.x, (currentCellPosition.y-1), origin.z);
            }
            //tilemap.CompressBounds();
            i++;
        }
    }

    private void SetGroundTile(Tilemap tilemap, Vector3Int cellPosition, int x, int y)
    {
        if (!level.MapModel[y*level.Width + x].Equals(TileTypes.NonTile)) {
            // TODO: Randomize ground tiles
            tilemap.SetTile(cellPosition, groundTile);
        }
    }

    private void SetWallTile(Tilemap tilemap, Vector3Int cellPosition, int x, int y)
    {
        cellPosition.z = 2;
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.CornerWall))
        {
            tilemap.SetTile(cellPosition, cornerTile);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.VerticalWall))
        {
            tilemap.SetTile(cellPosition, verticalWallTile);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.HorizontalWall))
        {
            tilemap.SetTile(cellPosition, horizontalWallTile);
        }
    }
}

