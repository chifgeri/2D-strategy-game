using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTableDown : MonoBehaviour
{
    [SerializeField]
    private LevelChooserController controller;

    private void OnMouseDown()
    {
        controller.MoveBackInLevelChooser();
    }
}
