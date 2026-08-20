using UnityEngine;

namespace Carp {
    public class Gate : Openable
    {
        [SerializeField]
        private EndScreen endScreen;
        [SerializeField]
        private float lockedOpacity = 0.5f;

        private SpriteRenderer sr;

        public override void Start() {
            if (GameManager.Instance.GetAreGatesUnlocked()) {
                isLocked = false;
            }

            if (isLocked) {
                Lock();
            } else {
                Unlock();
            }
        }

        public override void Interact() {
            base.Interact();

            if (!isLocked) {
                Debug.Log($"Going to [{Stuff.endScreenDict[endScreen]}]");
                EvtSystem.EventDispatcher.Raise<RequestOpenConfirmationScreen>(
                        new RequestOpenConfirmationScreen {
                        endingScreenName = Stuff.endScreenDict[endScreen] });
            }
        }

        public override bool HandleItemUse(Object item) {
            Debug.Log("Gate: HandleItemUse() not yet implemented.");
            return false;
        }

        public override void Unlock() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // make transparent
            sr.color = new Color(1,1,1,1); // white tint

            // unlock
            isLocked = false;
            // Update GM of status
            GameManager.Instance.AddModifiedDoor(objectData.objectID, isLocked);
        }

        public override void Lock() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // make transparent
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 
                    lockedOpacity);
            sr.color = newColor;

            // unlock
            isLocked = true;
            // Update GM of status
            GameManager.Instance.AddModifiedDoor(objectData.objectID, isLocked);
        }

    }
}
