using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Model;

public static class TileTypes {

    public static readonly string VerticalWall = "VW";
    public static readonly string HorizontalWall = "HW";
    public static readonly string CornerWall = "CW";
    public static readonly string Ground1 = "G1";
    public static readonly string Ground2 = "G2";
    public static readonly string Ground3 = "G3";
    public static readonly string Ground4 = "G4";
    public static readonly string Potion = "P";
    public static readonly string TreasureLeft = "TL";
    public static readonly string TreasureRight = "TR";
    public static readonly string Barrel = "B";
    public static readonly string VerticalDoor = "VD";
    public static readonly string HorizontalDoor = "HD";
    public static readonly string NonTile = "N";
}


public static class BuildTilemaps {
    public static void SetUpTileMaps(Tilemap[] tilemaps, Map level)
    {
        // Tilemap index
        int i = 0;
        // Start building from the center
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
                    if (i == 0)
                    {
                        SetGroundTile(tilemap, level, currentCellPosition, w, h);
                    }
                    // Wall layer
                    if (i == 1)
                    {
                        SetWallTile(tilemap, level, currentCellPosition, w, h);
                    }
                    // Object layer
                    if (i == 2)
                    {
                        SetObjectTile(tilemap, level, currentCellPosition, w, h);
                    }
                    // Door layer
                    if (i == 3)
                    {
                       SetDoorTile(tilemap, level, currentCellPosition, w, h);
                    }
                    currentCellPosition = new Vector3Int(
                        (int)(cellSize.x + currentCellPosition.x),
                        currentCellPosition.y, origin.z);
                }
                currentCellPosition = new Vector3Int(origin.x, (currentCellPosition.y+1), origin.z);
            }
            tilemap.CompressBounds();
            // increment tilemap index
            i++;
        }
    }

    private static void SetGroundTile(Tilemap tilemap, Map level, Vector3Int cellPosition, int x, int y)
    {
        string tile = level.MapModel[y * level.Width + x];
        if (!tile.Equals(TileTypes.NonTile)) {
            if (tile.Equals(TileTypes.HorizontalWall))
            {
                if (
                    (y < level.Height - 1 && level.MapModel[(y + 1) * level.Width + x].Equals(TileTypes.NonTile))
                    || y == level.Height-1 )
                {
                    return;
                }
            }
            if (tile.Equals(TileTypes.VerticalWall))
            {
                if(x < level.Width - 1 && level.MapModel[y * level.Width + (x + 1)].Equals(TileTypes.NonTile)
                    || x == level.Width - 1)
                {
                    return;
                }
            }
            tilemap.SetTile(cellPosition, TileReferences.Instance.ground1);
            if (tile.Equals(TileTypes.Ground2))
            {
                tilemap.SetTile(cellPosition, TileReferences.Instance.ground2);
            }
            if (tile.Equals(TileTypes.Ground3))
            {
                tilemap.SetTile(cellPosition, TileReferences.Instance.ground3);
            }
            if (tile.Equals(TileTypes.Ground4))
            {
                tilemap.SetTile(cellPosition, TileReferences.Instance.ground4);
            }
        }
    }

    private static void SetWallTile(Tilemap tilemap, Map level,Vector3Int cellPosition, int x, int y)
    {
        cellPosition.z = 2;
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.CornerWall))
        {
            tilemap.SetTile(cellPosition, TileReferences.Instance.cornerWall);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.VerticalWall))
        {
            tilemap.SetTile(cellPosition, TileReferences.Instance.verticalWall);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.HorizontalWall))
        {
            tilemap.SetTile(cellPosition, TileReferences.Instance.horizontalWall);
        }
    }

    private static void SetObjectTile(Tilemap tilemap, Map level, Vector3Int cellPosition, int x, int y)
    {
        cellPosition.z = 2;
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.Potion))
        {
           tilemap.SetTile(cellPosition, TileReferences.Instance.potion);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.Barrel))
        {
            tilemap.SetTile(cellPosition, TileReferences.Instance.barrel);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.TreasureLeft))
        {
           tilemap.SetTile(cellPosition, TileReferences.Instance.leftChestFull);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.TreasureRight))
        {
           tilemap.SetTile(cellPosition, TileReferences.Instance.rightChestFull);
        }
    }

    private static void SetDoorTile(Tilemap tilemap, Map level, Vector3Int cellPosition, int x, int y)
    {
        cellPosition.z = 2;
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.VerticalDoor))
        {
            tilemap.SetTile(cellPosition, TileReferences.Instance.verticalDoor);
        }
        if (level.MapModel[y * level.Width + x].Equals(TileTypes.HorizontalDoor))
        {
            tilemap.SetTile(cellPosition, TileReferences.Instance.horizontalDoor);
        }
    }
}

