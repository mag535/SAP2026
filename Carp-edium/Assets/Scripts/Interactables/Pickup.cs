using UnityEngine;

namespace Carp {
    public class Pickup : Interactable
    {
        public static float destroyDelay = 0.1f;

        public Object objectData;

        void Start() {
            // set sprite on load
            if (objectData.sprite == null) { return; }
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = objectData.sprite;
        }

        public override void Interact() {
            // sfx
            AudioManager.Instance.Play(soundEffect.name);
            // add to inventory
            EvtSystem.EventDispatcher.Raise<RequestAddItem>(new RequestAddItem {
                    item = objectData });
            // destroy
            Destroy(gameObject, destroyDelay);
        }

        public override bool HandleItemUse(Object item) { return false; }
    }
}
