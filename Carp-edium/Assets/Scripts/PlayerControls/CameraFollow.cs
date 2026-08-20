using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Carp {
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private bool isWithinBounds = true;
        private Camera playerCamera;
        private List<Vector2> spawnBounds;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            spawnBounds = new List<Vector2>();
            foreach (Transform childTransform in gameObject.transform) {
                if (childTransform.gameObject.name == "SPAWN") {
                    foreach (Transform vertTransform in childTransform) {
                        Vector2 pos = new Vector2(
                                vertTransform.position.x,
                                vertTransform.position.y);
                        spawnBounds.Add(pos);
                    }
                }
            }
            Vector3 newPos = new Vector3(
                    playerTransform.position.x,
                    playerTransform.position.y,
                    gameObject.transform.position.z);
            gameObject.transform.position = newPos;
        }

        void FixedUpdate()
        {
            switch(RoomTransitionManager.Instance.GetCurrentRoom()) {
            case RoomName.SPAWN:
                isWithinBounds = CheckBounds(spawnBounds);
                break;
            default:
                Debug.Log("WIP");
                break;
            }

            if (isWithinBounds) {
                Vector3 newPos = new Vector3(
                        playerTransform.position.x,
                        playerTransform.position.y,
                        gameObject.transform.position.z);
                gameObject.transform.position = newPos;
            }
        }

        bool CheckBounds(List<Vector2> bounds) {
            bool collision = false;
            Vector2 cp = new Vector2(
                    playerTransform.position.x,
                    playerTransform.position.y);
            int next = 0;
            for (int i = 0; i < bounds.Count; i++) {
                next = i+1;
                if (next == bounds.Count) {
                    next = 0;
                }
                Vector2 vc = bounds[i];
                Vector2 vn = bounds[next];
                if (((vc.y > cp.y) != (vn.y >cp.y)) &&
                        (cp.x < (vn.x-vc.x) * (cp.y-vc.y) / (vn.y-vc.y) + vc.x)) {
                    collision = !collision;
                }
            }
            return collision;
        }
    }
}
