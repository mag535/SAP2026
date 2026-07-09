using UnityEngine;

namespace Carp {
    public class Openable : Interactable
    {
        public Sound unlockingSoundEffect;
        public Object objectData;
        public Object key;

        protected bool isLocked = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        public override void Interact() {
            if (isLocked) {
                AudioManager.Instance.Play(soundEffect.name);
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { objectData = objectData });
            } else {
                AudioManager.Instance.Play(unlockingSoundEffect.name);
            }
        }

        public override void HandleItemUse(Object item) {
            if (isLocked && item.objectID != key.objectID) { return; }
            Unlock();
        }

        public virtual void Unlock() {
            // TODO: change?
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // make transparent
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);
            sr.color = newColor;

            // unlock
            isLocked = false;
        }

    }
}
