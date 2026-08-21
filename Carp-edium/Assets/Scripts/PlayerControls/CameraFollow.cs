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

        [SerializeField]
        private bool isInitialized = false;
        private List<Vector2> spawnBounds;
        private List<Vector2> barBounds;
        private List<Vector2> templeBounds;
        private List<Vector2> palaceBounds;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<SignalCameraPositionUpdate>(HandleUpdatePositionSignal);
            EvtSystem.EventDispatcher.AddListener<ResetCameraPositionToPlayers>(HandleResetPositionSignal);
        }

        void HandleUpdatePositionSignal(SignalCameraPositionUpdate _)
        {
            if (!isInitialized && 
                    RoomTransitionManager.Instance.GetCurrentRoom() == RoomName.SPAWN) {
                Initialize();
                return;
            }

            UpdatePosition();
        }

        void HandleResetPositionSignal(ResetCameraPositionToPlayers _) {
            SetToPlayerPosition();
        }

        void Initialize() {
            spawnBounds = new List<Vector2>();
            barBounds = new List<Vector2>();
            templeBounds = new List<Vector2>();
            palaceBounds = new List<Vector2>();

            foreach (Transform childTransform in gameObject.transform) {
                if (childTransform.gameObject.name == "SPAWN") {
                    foreach (Transform vertTransform in childTransform) {
                        Vector2 pos = new Vector2(
                                vertTransform.position.x,
                                vertTransform.position.y);
                        spawnBounds.Add(pos);
                    }
                } else if (childTransform.gameObject.name == "BAR") {
                    foreach (Transform vertTransform in childTransform) {
                        Vector2 pos = new Vector2(
                                vertTransform.position.x,
                                vertTransform.position.y);
                        barBounds.Add(pos);
                    }
                } else if (childTransform.gameObject.name == "TEMPLE") {
                    foreach (Transform vertTransform in childTransform) {
                        Vector2 pos = new Vector2(
                                vertTransform.position.x,
                                vertTransform.position.y);
                        templeBounds.Add(pos);
                    }
                } else if (childTransform.gameObject.name == "PALACE") {
                    foreach (Transform vertTransform in childTransform) {
                        Vector2 pos = new Vector2(
                                vertTransform.position.x,
                                vertTransform.position.y);
                        palaceBounds.Add(pos);
                    }
                }
            }
            SetToPlayerPosition();
            isInitialized = true;
        }

        void SetToPlayerPosition() {
            Vector3 newPos = new Vector3(
                    playerTransform.position.x,
                    playerTransform.position.y,
                    gameObject.transform.position.z);
            gameObject.transform.position = newPos;
        }

        void UpdatePosition() {
            switch(RoomTransitionManager.Instance.GetCurrentRoom()) {
            case RoomName.SPAWN:
                isWithinBounds = CheckBounds(spawnBounds);
                break;
            case RoomName.BAR:
                isWithinBounds = CheckBounds(barBounds);
                break;
            case RoomName.TEMPLE:
                isWithinBounds = CheckBounds(templeBounds);
                break;
            case RoomName.PALACE:
                isWithinBounds = CheckBounds(palaceBounds);
                break;
            default:
                Debug.Log("CameraFollower: not a valid room");
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

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<SignalCameraPositionUpdate>(HandleUpdatePositionSignal);
            EvtSystem.EventDispatcher.RemoveListener<ResetCameraPositionToPlayers>(HandleResetPositionSignal);
        }
    }
}
