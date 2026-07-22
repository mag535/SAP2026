using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class Fox : MonoBehaviour
    {
        public FoxUnlockPosition barPosition = new FoxUnlockPosition("Bar", Vector2.zero);
        public FoxUnlockPosition templePosition = new FoxUnlockPosition("Temple", Vector2.zero);
        public FoxUnlockPosition palacePosition = new FoxUnlockPosition("Palace", Vector2.zero);

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<PropagateFlag>(HandleFlag);
        }

        void HandleFlag(PropagateFlag evt) {
            if (evt.flag == "Bar_Unlocked") {
                gameObject.transform.position = new Vector3(
                        barPosition.position.x,
                        barPosition.position.y,
                        0);
            } else if (evt.flag == "Temple_Unlocked") {
                gameObject.transform.position = new Vector3(
                        templePosition.position.x,
                        templePosition.position.y,
                        0);
            } else if (evt.flag == "Palace_Unlocked") {
                gameObject.transform.position = new Vector3(
                        palacePosition.position.x,
                        palacePosition.position.y,
                        0);
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<PropagateFlag>(HandleFlag);
        }
    }
}
