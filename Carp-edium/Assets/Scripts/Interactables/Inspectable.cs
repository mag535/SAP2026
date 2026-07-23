using UnityEngine;

namespace Carp {
    public class Inspectable : Interactable
    {
        private int otherScriptsCount;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Interactable[] interactableScripts = GetComponents<Interactable>();
            foreach (Interactable script in interactableScripts) {
                otherScriptsCount++;
                if (script == this) { continue; }
                objectData = script.objectData;
                soundEffect = script.soundEffect;
                break;
            }

            foreach (Interactable _ in interactableScripts) { otherScriptsCount++; }
        }

        public override void Interact() {
            if (otherScriptsCount == 1) {
                AudioManager.Instance.Play(soundEffect.name);

                // Send signal to have description and sprite displayed. This is 
                // magnifying
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { 
                        useLong = true,
                        objectData = objectData });
                return;
            }

            // otherwise, handle special cases
            if (GetComponent<Openable>() != null) {
                OpenableInspection();
            } else if (GetComponent<Trader>() != null) {
                TraderInspection();
            } else {
                Debug.Log("missing interactable type?");
            }
        }

        public override bool HandleItemUse(Object item) { return false; }

        public void OpenableInspection() {
            Openable script = GetComponent<Openable>();
            if (script.isLocked) {
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { 
                        useLong = true,
                        objectData = script.objectData });
            }
        }

        public void TraderInspection() {
            Trader script = GetComponent<Trader>();
            // Display out of stock description if no more trades available
            if (script.possibleTrades.Count == 0) {
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { 
                        useLong = true,
                        objectData = script.objectData });
                return;
            }
            // Send signal to have description and sprite displayed. This is 
            // magnifying
            EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                    new RequestDisplayInspected { 
                    useLong = false,
                    objectData = script.objectData });
        }
    }
}
