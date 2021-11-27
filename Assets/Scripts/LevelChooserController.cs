using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelChooserController : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraTarget;
    // Start is called before the first frame update
    private int levelPage;

    private const float sceneSizeInWorldCoordinate = 10.0f;
    private const float offset = 2.0f;

    private List<LevelMarker> markers = new List<LevelMarker>();
    private void Awake()
    {
        levelPage = 0;

        var lmarkers = GetComponentsInChildren<LevelMarker>();
        foreach(var marker in lmarkers)
        {
            markers.Add(marker);
        }
        markers = markers.OrderBy(m => m.order).ToList();
    }

    public void MoveForwardLevelChooserPage()
    {
        var transform = cameraTarget.gameObject.transform;
        if (levelPage > 0)
        {
            cameraTarget.gameObject.transform.SetPositionAndRotation(
                new Vector3(transform.position.x,
                            transform.position.y + sceneSizeInWorldCoordinate,
                            transform.position.z), Quaternion.identity);
            levelPage++;
        } else
        {
            cameraTarget.gameObject.transform.SetPositionAndRotation(
                new Vector3(transform.position.x,
                            transform.position.y + sceneSizeInWorldCoordinate + offset,
                            transform.position.z), Quaternion.identity);
            levelPage++;
            SetCurrentLevelValues();
        }
    }

    public void MoveBackInLevelChooser()
    {
        var transform = cameraTarget.gameObject.transform;

        if (levelPage > 1)
        {
            cameraTarget.gameObject.transform.SetPositionAndRotation(
                new Vector3(transform.position.x,
                            transform.position.y - sceneSizeInWorldCoordinate,
                            transform.position.z), Quaternion.identity);
            levelPage--;
            SetCurrentLevelValues();
        }
        else
        {
            cameraTarget.gameObject.transform.SetPositionAndRotation(
                new Vector3(0,0,0), Quaternion.identity);
            levelPage--;
        }
    }

    private void SetCurrentLevelValues()
    {
        int index = (levelPage - 1) * 3;
        var levels = MainStateManager.Instance.Levels;
        foreach(var marker in markers)
        {
            if (levels.Count > index) {
                marker.SetLevelNumber(index, levels[index].Cleared);
                marker.Enable();
            }
            else
            {
                marker.Disable();
            }
            index++;
        }
    }
}
