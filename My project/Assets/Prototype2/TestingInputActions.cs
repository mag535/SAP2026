using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TestingInputActions : MonoBehaviour
{
    public float speed = 5f;
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilters;

    private Vector2 inputVector;
    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private PlayerState playerStateManager;

    void Awake() {
        inputVector = Vector2.zero;
        movementDirection = Vector2.zero;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerStateManager = GetComponent<PlayerState>();
        Debug.Log("State: " + playerStateManager.GetCurrentState());
    }

    void FixedUpdate()
    {
        switch (playerStateManager.GetCurrentState()) {
            case PlayerState.PlayerStates.GAME:
                bool success = MovePlayer(movementDirection);

                if (!success) {
                    // try left/right
                    success = MovePlayer(new Vector2(movementDirection.x, 0));

                    // try up/down
                    if (!success) {
                        success = MovePlayer(new Vector2(0, movementDirection.y));

                        if (!success) { Debug.Log("CANT MOVE"); }
                    }
                }
                break;
            case PlayerState.PlayerStates.DIALOGUE:
                break;
            case PlayerState.PlayerStates.DESCRIPTION:
                break;
        }
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.started) {
            inputVector = context.ReadValue<Vector2>();
        } else if (context.canceled) {
            inputVector = Vector2.zero;
        }
        //movementDirection = inputVector;
        movementDirection = RotateDirection(inputVector);
        //Debug.Log(context);
    }

    private bool MovePlayer(Vector2 direction) {
        int count = rb.Cast(
                direction,
                movementFilters, // Layers valid for collision detection (eg. wall, NPC, object)
                castCollisions,
                speed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            Vector2 moveVector = direction * speed * Time.fixedDeltaTime;
            // no collisions
            //rb.MovePosition(rb.position + moveVector);
            rb.position += moveVector;
            return true;
        }

        // Hits present
        // print collisions
        foreach (RaycastHit2D hit in castCollisions) {
            print(hit.ToString());
        }
        return false;
    }

    // rotate 45 degrees clockwise
    private Vector2 RotateDirection(Vector2 direction) {
        // up
        if (direction.x == 0 && direction.y == 1) {
            return new Vector2(Mathf.Sqrt(2), Mathf.Sqrt(2)); // upper right
        } else
        // right
        if (direction.x == 1 && direction.y == 0) {
            return new Vector2(Mathf.Sqrt(2), -Mathf.Sqrt(2)); // lower right
        } else
        // down
        if (direction.x == 0 && direction.y == -1) {
            return new Vector2(-Mathf.Sqrt(2), -Mathf.Sqrt(2)); // lower left
        } else
        // left
        if (direction.x == -1 && direction.y == 0) {
            return new Vector2(-Mathf.Sqrt(2), Mathf.Sqrt(2)); // upper left
        }

        return Vector2.zero;
    }
}
