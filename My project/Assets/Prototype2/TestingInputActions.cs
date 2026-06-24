using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputActions : MonoBehaviour
{
    public float speed = 2f;
    
    private bool isMoving;
    private Vector2 inputVector;

    void Awake() {
        isMoving = false;
        inputVector = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            Vector3 updateVector = new Vector3(inputVector.x, inputVector.y, 0);
            gameObject.transform.position += updateVector * Time.deltaTime * speed;
        }
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.started) {
            isMoving = true;
            inputVector = context.ReadValue<Vector2>();
        } else if (context.canceled) {
            isMoving = false;
        }

        //Debug.Log(context);
    }

    public void Interact(InputAction.CallbackContext context) {
        if (context.canceled) {
            Debug.Log("Interact " + context.phase);
        }
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("collided");
    }
}
