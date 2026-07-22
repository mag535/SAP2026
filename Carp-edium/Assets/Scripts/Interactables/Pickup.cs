using UnityEngine;

namespace Carp {
    public class Pickup : Interactable
    {
        public static float destroyDelay = 0.1f;

        public Object objectData;

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

            Debug.Log("Pickup Start()");
        }

        public override void Interact() {
            // sfx
            AudioManager.Instance.Play(soundEffect.name);
            // add to inventory
            EvtSystem.EventDispatcher.Raise<RequestAddItem>(new RequestAddItem {
                    item = objectData });
            // Update GM on modification
            GameManager.Instance.AddModifiedPickup(objectData.objectID);
            // destroy
            Destroy(gameObject, destroyDelay);
        }

        public override bool HandleItemUse(Object item) { return false; }
    }
}
