using UnityEngine;

namespace Carp {
    public class Inspectable : Interactable
    {
        public Object objectData;

        private bool hasBeenInspected = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        public override void Interact() {
            if (!hasBeenInspected) {
                EvtSystem.EventDispatcher.Raise<RequestAddToNotebook>(
                        new RequestAddToNotebook {
                            spriteIcon = objectData.spriteIcon,
                            longDescription = objectData.longDescription
                        });
                hasBeenInspected = true;
            }

            // Send signal to have description and sprite displayed this is 
            // magnifying
            EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                    new RequestDisplayInspected {
                        spriteMagnified = objectData.spriteMagnified,
                        longDescription = objectData.longDescription
                    });
        }

        public override void HandleItemUse(Object item) {}
    }
}
