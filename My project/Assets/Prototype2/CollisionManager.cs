using UnityEngine;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour
{
    public float maxCollisionDistance = 0.5f;
    public List<GameObject> collidables;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collidables = new List<GameObject>();
    }

    public bool DidCollide(GameObject client) {
        bool didCollide = false;

        foreach (GameObject go in collidables) {
            if (go == client) {
                continue;
            }
            if (go.GetComponent<CircleCollider2D>() == null) {
                continue;
            }
            Vector2 goPos = new Vector2(go.transform.position.x, go.transform.position.y);
            float goRad = go.GetComponent<CircleCollider2D>().radius;
            Vector2 clientPos = new Vector2(client.transform.position.x, client.transform.position.y);
            float clientRad = client.GetComponent<CircleCollider2D>().radius;

            didCollide = CircCircCollision(clientPos, clientRad, goPos, goRad);
        }

        Debug.Log("checked collision: " + didCollide);

        return didCollide;
    }

    private bool CircCircCollision(Vector2 p1, float r1, Vector2 p2, float r2) {
        bool didCollide = false;

        float distX = p2.x - p1.x;
        float distY = p2.y - p1.y;
        float distSqr = (distX*distX) + (distY*distY);

        float radiiSqr = (r1+r2) * (r1+r2);
        if (distSqr < radiiSqr) {
            didCollide = true;
        }

        return didCollide;
    }
}
