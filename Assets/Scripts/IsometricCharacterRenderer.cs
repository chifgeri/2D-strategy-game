using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    Animator animator;
    private readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    private readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    int lastDirection;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;
        if(direction.magnitude < .01f)
        {
            directionArray = staticDirections;
        } else
        {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 8);
        }
        var dir = directionArray[lastDirection];
         animator.Play(dir);
    }

    int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;
        float step = 360f / sliceCount;
        float halfStep = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfStep;
        if(angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        var num = Mathf.FloorToInt(stepCount);
        return num;
    }
}
