using UnityEngine;

namespace Carp {
    public class Gate : Interactable
    {
        public EndScreen screenToTrigger;

        public override void Interact() {
            Debug.Log("Gate: Interact() not yet implemented.");

            if (screenToTrigger == EndScreen.DRAGON) {
                EvtSystem.EventDispatcher.Raise<TriggerWinScreen>(new
                        TriggerWinScreen {});
            } else if (screenToTrigger == EndScreen.LOSE1) {
                EvtSystem.EventDispatcher.Raise<TriggerLoseScreen>(new
                        TriggerLoseScreen {});
            } else if (screenToTrigger == EndScreen.LOSE2) {
                EvtSystem.EventDispatcher.Raise<TriggerLoseScreen>(new
                        TriggerLoseScreen {});
            }
        }

        public override bool HandleItemUse(Object item) {
            Debug.Log("Gate: HandleItemUse() not yet implemented.");
            return false;
        }

    }
}
