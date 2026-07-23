using UnityEngine;

namespace Carp {
    public class ImportantInspectable : Inspectable
    {
        private bool hasBeenInspected = false;

        public override void Interact() {
            base.Interact();

            // If this is the only interactable script, do normal stuff
            // If this is a first time inspection:
            // - add to notebook
            // - set bool
            if (!hasBeenInspected) {
                EvtSystem.EventDispatcher.Raise<RequestAddToNotebook>(
                        new RequestAddToNotebook { objectData = objectData });
                EvtSystem.EventDispatcher.Raise<RequestCreateNotification>( new
                        RequestCreateNotification {
                        isNoteEntry = objectData.isNoteEntry,
                        objectName = objectData.name });
                hasBeenInspected = true;
            }

        }
    }
}
