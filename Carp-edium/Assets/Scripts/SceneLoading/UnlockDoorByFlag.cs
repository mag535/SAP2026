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
                Door thisDoor = GetComponent<Door>();
                thisDoor.Unlock();
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<PropagateFlag>(HandleFlag);
        }
    }
}
