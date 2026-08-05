using UnityEngine;

namespace Carp {
    public class Gate : Interactable
    {
        public EndScreen screenToTrigger;

        public override void Interact() {
            Debug.Log("Gate: Interact() not yet implemented.");
        }

        public override bool HandleItemUse(Object item) {
            Debug.Log("Gate: HandleItemUse() not yet implemented.");
            return false;
        }

    }
}
