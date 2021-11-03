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
            var position = collision.gameObject.transform.position;
            Debug.Log(position);

            var tilemap = this.GetComponent<Tilemap>();
            var cell = tilemap.WorldToCell(new Vector3(position.x, position.y, position.z));

            Debug.Log(cell);

            var room = TilemapGridController.Instance.Level.Rooms.Find(room => room.DoorPosition.Equals(new Vector2Int(cell.x, cell.y)));
            if (room != null)
            {
                Debug.Log(room.RoomId);
                if (!room.Cleared)
                {
                    sceneIsLoading = true;
                    SceneManager.LoadSceneAsync("RoomScene");
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!sceneIsLoading)
        {
            var position = collision.gameObject.transform.position;
            Debug.Log(position);

            var tilemap = this.GetComponent<Tilemap>();
            var cell = tilemap.WorldToCell(new Vector3(position.x, position.y, position.z));

            Debug.Log(cell);

            var room = TilemapGridController.Instance.Level.Rooms.Find(room => room.DoorPosition.Equals(new Vector2Int(cell.x, cell.y)));
            if (room != null)
            {
                Debug.Log(room.RoomId);
                if (!room.Cleared)
                {
                    sceneIsLoading = true;
                    SceneManager.LoadSceneAsync("RoomScene");
                }
            }
        }
    }
}
