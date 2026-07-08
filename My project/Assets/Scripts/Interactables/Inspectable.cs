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
            AudioManager.Instance.Play(soundEffect.name);

            // If this is a first time inspection:
            // - add to notebook
            // - set bool
            if (!hasBeenInspected) {
                EvtSystem.EventDispatcher.Raise<RequestAddToNotebook>(
                        new RequestAddToNotebook { objectData = objectData });
                hasBeenInspected = true;
            }

            // Send signal to have description and sprite displayed. This is 
            // magnifying
            EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                    new RequestDisplayInspected { 
                    useLong = true,
                    objectData = objectData });
        }

        public override void HandleItemUse(Object item) {}
    }
}
