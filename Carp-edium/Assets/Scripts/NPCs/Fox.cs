using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class Fox : MonoBehaviour
    {
        public FoxUnlockPosition palacePosition = new FoxUnlockPosition("Palace", Vector2.zero);

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<PropagateFlag>(HandleFlag);
        }

        void Start() {
            if (GameManager.Instance.AmIAModifiedFox()) {
                Vector2 newPosition = GameManager.Instance.GetModifiedFoxData();
                gameObject.transform.position = new Vector3 (
                        newPosition.x,
                        newPosition.y,
                        0);
            }
        }

        void HandleFlag(PropagateFlag evt) {
            /*
            if (evt.flag == "OpenBarDoor") {
                gameObject.transform.position = new Vector3(
                        barPosition.position.x,
                        barPosition.position.y,
                        0);
                GameManager.Instance.AddModifiedFox(barPosition.position);
            } else if (evt.flag == "OpenTempleDoor") {
                gameObject.transform.position = new Vector3(
                        templePosition.position.x,
                        templePosition.position.y,
                        0);
                GameManager.Instance.AddModifiedFox(templePosition.position);
            } else*/ if (evt.flag == "PalaceIsUnlocked") {
                gameObject.transform.position = new Vector3(
                        palacePosition.position.x,
                        palacePosition.position.y,
                        0);
                GameManager.Instance.AddModifiedFox(palacePosition.position);
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<PropagateFlag>(HandleFlag);
        }
    }
}
