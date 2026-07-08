using UnityEngine;

namespace Carp {
    public class Door : Openable
    {
        public override void Interact() {
            base.Interact();

            if (!isLocked) {
                DoSomething();
            }
        }

        void DoSomething() {
            Debug.Log("Do the Door thing (teleport and change level).");
        }

    }
}
