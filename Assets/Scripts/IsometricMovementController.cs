using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricMovementController : MonoBehaviour
{
    public float movementSpeed = 2f;
    IsometricCharacterRenderer isoRenderer;
    // Start is called before the first frame update
    Rigidbody2D rbody;
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    void FixedUpdate() {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        isoRenderer.SetDirection(movement);
        Vector2 newPos = currentPos+movement* Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }
}
