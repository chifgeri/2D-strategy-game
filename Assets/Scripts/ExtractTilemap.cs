using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ExtractTilemap
{
    public static string[] ExtractTilemapData(Tilemap[] tilemaps)
    {
        var size = tilemaps[0].size;
        var tilenames = new string[size.x *size.y];
        var tilemapId = 0;

        foreach (var tilemap in tilemaps) {
            // Boundaries min and max values
            var xmin = tilemap.cellBounds.xMin;
            var xmax = tilemap.cellBounds.xMax;
            var ymin = tilemap.cellBounds.yMin;
            var ymax = tilemap.cellBounds.yMax;
            // index in the new array
            var index = 0;

            // Go through the layer's tiles and save the corresponding Type strings
            for (int i = ymin; i < ymax; i++)
            {
                for (int j = xmin; j < xmax; j++)
                {
                    TileBase tile;
                    // Only on the ground layer there is tiles on the 0 z positin
                    if (tilemapId == 0)
                    {
                        tile = tilemap.GetTile(new Vector3Int(j, i, 0));
                    }
                    // The other tiles has tiles at z position 2
                    else
                    {
                        tile = tilemap.GetTile(new Vector3Int(j, i, 2));
                    }
                    // Check for indexing error
                    if ((Mathf.Abs(ymin)+i) * (Mathf.Abs(xmin)+j) < tilenames.Length)
                    {
                        if (tile != null)
                        {
                            // When match tiles gives back null (e. g. this tile isnt needed)
                            // Then the tile which already in the tilenames is remains
                            tilenames[index] = MatchTiles(tile.name) ?? tilenames[index];
                        }
                        else
                        {
                            // If the tile is null but already is a name for this coordinate then it remains
                            // Otherwise its a NonTile
                            tilenames[index] = tilenames[index] ?? TileTypes.NonTile;
                        }
                    }
                    else
                    {
                        Debug.LogError((Mathf.Abs(ymin) + i) * (Mathf.Abs(xmin) + ymax));
                        Debug.LogError("Tile out of bounds");
                    }
                    index++;
                }
            }
            tilemapId++;
        }

        return tilenames;
    }

    private static string MatchTiles(string tileName)
    {
        if (tileName == TileReferences.Instance.ground1.name)
        {
            return TileTypes.Ground1;
        }
        if (tileName == TileReferences.Instance.horizontalWall.name)
        {
            return TileTypes.HorizontalWall;
        }
        if (tileName == TileReferences.Instance.verticalWall.name)
        {
            return TileTypes.VerticalWall;
        }
        if (tileName == TileReferences.Instance.cornerWall.name)
        {
            return TileTypes.CornerWall;
        }
        if (tileName == TileReferences.Instance.horizontalDoor.name)
        {
            return TileTypes.HorizontalDoor;
        }
        if (tileName == TileReferences.Instance.verticalDoor.name)
        {
            return TileTypes.VerticalDoor;
        }
        if (tileName == TileReferences.Instance.ground2.name)
        {
            return TileTypes.Ground3;
        }
        if (tileName == TileReferences.Instance.ground3.name)
        {
            return TileTypes.Ground3;
        }
        if (tileName == TileReferences.Instance.ground4.name)
        {
            return TileTypes.Ground4;
        }
        if (tileName == TileReferences.Instance.potion.name)
        {
            return TileTypes.Potion;
        }
        if (tileName == TileReferences.Instance.barrel.name)
        {
            return TileTypes.Barrel;
        }
        if (tileName == TileReferences.Instance.leftChestFull.name)
        {
            return TileTypes.TreasureLeft;
        }
        if (tileName == TileReferences.Instance.rightChestFull.name)
        {
            return TileTypes.TreasureRight;
        }
        return null;
    }


}
