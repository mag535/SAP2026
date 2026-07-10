using UnityEngine;

namespace Carp {
    public class ImportantInspectable : Inspectable
    {
        private bool hasBeenInspected = false;

        public override void Interact() {
            base.Interact();

            // If this is a first time inspection:
            // - add to notebook
            // - set bool
            if (!hasBeenInspected) {
                // TODO: add signal for notification
                EvtSystem.EventDispatcher.Raise<RequestAddToNotebook>(
                        new RequestAddToNotebook { objectData = objectData });
                hasBeenInspected = true;
            }

        }
    }
}
