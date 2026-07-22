using UnityEngine;

namespace Carp {
    public class Openable : Interactable
    {
        public Sound unlockingSoundEffect;
        public Object objectData;
        public Object key;

        public bool isLocked = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            // Check for modified data
            if (GameManager.Instance.AmIAModifiedDoor(objectData.objectID)) {
                isLocked = GameManager.Instance
                    .GetModifiedDoorData(objectData.objectID);
            }

            if (!isLocked) {
                Unlock();
            }
        }

        public override void Interact() {
            if (isLocked) {
                AudioManager.Instance.Play(soundEffect.name);
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { 
                        useLong = true,
                        objectData = objectData });
                EvtSystem.EventDispatcher.Raise<RequestAddToNotebook>(
                        new RequestAddToNotebook { objectData = objectData });
            } else {
                AudioManager.Instance.Play(unlockingSoundEffect.name);
            }
        }

        public override bool HandleItemUse(Object item) {
            if (isLocked && item.objectID != key.objectID) { return false; }
            Unlock();
            return true;
        }

        public virtual void Unlock() {
            // TODO: change?
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // make transparent
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);
            sr.color = newColor;

            // unlock
            isLocked = false;
            // Update GM of status
            GameManager.Instance.AddModifiedDoor(objectData.objectID, isLocked);
        }

        public virtual void Lock() {
            // TODO: change?
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // make transparent
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            sr.color = newColor;

            // unlock
            isLocked = true;
            // Update GM of status
            GameManager.Instance.AddModifiedDoor(objectData.objectID, isLocked);
        }
    }
}
