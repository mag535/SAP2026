using UnityEngine;

namespace Carp {
    public class Pickup : Interactable
    {
        public static float destroyDelay = 0.1f;

        void Start() {
            // set sprite on load
            if (objectData.sprite != null) {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = objectData.sprite;
            }

            if (GameManager.Instance.AmIAModifiedPickup(objectData.objectID)) {
                // destroy
                Destroy(gameObject, destroyDelay);
            }
        }

        public override void Interact() {
            // sfx
            AudioManager.Instance.Play(soundEffect.name);
            // add to inventory
            EvtSystem.EventDispatcher.Raise<RequestAddItem>(new RequestAddItem {
                    item = objectData });
            // create notification
            EvtSystem.EventDispatcher.Raise<RequestCreateNotification>(new
                    RequestCreateNotification { 
                    isNoteEntry = objectData.isNoteEntry,
                    objectName = objectData.name });
            // Update GM on modification
            GameManager.Instance.AddModifiedPickup(objectData.objectID);
            // destroy
            Destroy(gameObject, destroyDelay);
        }

        public override bool HandleItemUse(Object item) { return false; }
    }
}
