using UnityEngine;

namespace Carp {
    public class UnlockDoorByFlag : MonoBehaviour
    {
        public string flagToWatch;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<PropagateFlag>(HandleFlag);
        }

        void HandleFlag(PropagateFlag evt) {
            if (evt.flag == flagToWatch) {
                GetComponent<Door>().Unlock();
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<PropagateFlag>(HandleFlag);
        }
    }
}
