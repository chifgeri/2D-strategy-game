using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Doors : MonoBehaviour
{
    bool sceneIsLoading = false;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!sceneIsLoading)
        {
            LoadRoom(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!sceneIsLoading)
        {
            LoadRoom(collision);
        }
    }

    private void LoadRoom(Collider2D collision)
    {
        var position = collision.gameObject.transform.position;

        var tilemap = this.GetComponent<Tilemap>();
        var cell = tilemap.WorldToCell(new Vector3(position.x, position.y, position.z));

        var room = TilemapGridController.Instance.Level.Rooms.Find(room => room.DoorPosition.Equals(new Vector2Int(cell.x, cell.y)));
        if (room != null)
        {
            if (!room.Cleared)
            {
                sceneIsLoading = true;
                MainStateManager.Instance.GameState.CurrentRoomId = room.RoomId;
                MainStateManager.Instance.GameState.IsInMap = false;
                MainStateManager.Instance.GameState.IsInFight = true;
                MainStateManager.Instance.LoadScene("RoomScene");
            }
        }
    }
}
