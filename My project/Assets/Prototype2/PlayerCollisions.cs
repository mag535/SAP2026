using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private Collider2D playerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
