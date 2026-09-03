using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Carp {
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float collisionOffset = 0.1f;
        public ContactFilter2D movementFilters;

        private Vector2 inputVector;
        private Vector2 movementDirection;
        private Rigidbody2D rb;
        private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        private bool isMoving = false;

        private PlayerState playerState;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestChangePlayerPosition>(
                    HandlePlayerPositionChange);
        }

        void Start() {
            inputVector = Vector2.zero;
            movementDirection = Vector2.zero;
            rb = GetComponent<Rigidbody2D>();
            playerState = GetComponent<PlayerState>();
        }

        void FixedUpdate()
        {
            switch (playerState.GetCurrentState()) {
            case PlayerStates.DIALOGUE: 
                break;
            default:
                GetInput();
                bool success = MovePlayer(movementDirection);

                if (!success) {
                    // try left/right
                    success = MovePlayer(new Vector2(movementDirection.x, 0));

                    // try up/down
                    if (!success) {
                        success = MovePlayer(new Vector2(0, movementDirection.y));
                    }
                }

                isMoving = success;
                if (isMoving) {
                    EvtSystem.EventDispatcher.Raise<SignalCameraPositionUpdate>(
                            new SignalCameraPositionUpdate {});
                }
                break;
            }
        }

        public bool GetIsMoving() {
            return isMoving;
        }

        public Vector2 GetInputVector() {return inputVector;}

        void GetInput() {
            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");
            movementDirection = inputVector;
            movementDirection.Normalize();
        }

        /*
        public void Move(InputAction.CallbackContext context) {
            if (context.started) {
                inputVector = context.ReadValue<Vector2>();
                Debug.Log($"Input Vector: {context}");
                EvtSystem.EventDispatcher.Raise<ChangePlayerSprite>(new
                        ChangePlayerSprite { direction = inputVector });
            } else if (context.canceled) {
                inputVector = Vector2.zero;
            }
            // NSEW
            //movementDirection = inputVector;
            // isometric
            movementDirection = inputVector; //RotateDirection(inputVector);
        }
        */

        private bool MovePlayer(Vector2 direction) {
            if (direction == Vector2.zero) {
                isMoving = false;
                return false;
            }

            int count = rb.Cast(
                    direction,
                    movementFilters, // Layers valid for collision detection (eg. wall, NPC, object)
                    castCollisions,
                    speed * Time.fixedDeltaTime + collisionOffset);

            // no collisions
            if (count == 0) {
                Vector2 moveVector = direction * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + moveVector);
                isMoving = true;
                return true;
            }

            // Hits present
            // print collisions
            /*
            foreach (RaycastHit2D hit in castCollisions) {
                print(hit.ToString());
            }
            */
            isMoving = false;
            return false;
        }

        // rotate 45 degrees clockwise
        private Vector2 RotateDirection(Vector2 direction) {
            if (direction.x == 1 && direction.y == 0) {
                return new Vector2(Mathf.Sqrt(3f)/2f, -0.5f); // 11PI/6
            } else if (direction.x == 0 && direction.y == 1) {
                return new Vector2(Mathf.Sqrt(3f)/2f, 0.5f); // PI/6
            } else if (direction.x == -1 && direction.y == 0) {
                return new Vector2(-(Mathf.Sqrt(3f)/2f), 0.5f); // 5PI/6
            } else if (direction.x == 0 && direction.y == -1) {
                return new Vector2(-(Mathf.Sqrt(3f)/2f), -0.5f); // 7PI/6
            }

            return Vector2.zero;
        }

        void HandlePlayerPositionChange(RequestChangePlayerPosition evt) {
            Vector3 newPos = new Vector3(
                    evt.newPosition.x, evt.newPosition.y, 0);
            gameObject.transform.position = newPos;
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestChangePlayerPosition>(
                    HandlePlayerPositionChange);
        }
    }
}
