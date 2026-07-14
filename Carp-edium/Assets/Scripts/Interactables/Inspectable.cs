using UnityEngine;

namespace Carp {
    public class Inspectable : Interactable
    {
        public Object objectData;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        public override void Interact() {
            AudioManager.Instance.Play(soundEffect.name);

            // Send signal to have description and sprite displayed. This is 
            // magnifying
            EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                    new RequestDisplayInspected { 
                    useLong = true,
                    objectData = objectData });
        }

        public override bool HandleItemUse(Object item) { return false; }
    }
}
