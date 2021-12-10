using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTableUp : MonoBehaviour
{
    [SerializeField]
    private LevelChooserController controller;

    private void OnMouseDown()
    {
        controller.MoveForwardLevelChooserPage();
    }
}
