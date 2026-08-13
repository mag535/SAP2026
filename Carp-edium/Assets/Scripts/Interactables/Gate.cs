using UnityEngine;

namespace Carp {
    public class Gate : Openable
    {
        public EndScreen screenToTrigger;
        [SerializeField]
        private string endingScene;

        [SerializeField]
        private float lockedOpacity = 0.5f;
        private SpriteRenderer sr;

        public override void Start() {
            if (GameManager.Instance.GetAreGatesUnlocked()) {
                isLocked = true;
            }

            if (isLocked) {
                Lock();
            } else {
                Unlock();
            }
        }

        public override void Interact() {
            base.Interact();

            if (isLocked) {
            } else {
                if (screenToTrigger == EndScreen.DRAGON) {
                    //EvtSystem.EventDispatcher.Raise<TriggerWinScreen>(new
                    //        TriggerWinScreen {});
                    GetComponent<SceneLoader>().GoToScene(endingScene);
                } else if (screenToTrigger == EndScreen.LOSE1) {
                    //EvtSystem.EventDispatcher.Raise<TriggerLoseScreen>(new
                    //        TriggerLoseScreen {});
                    GetComponent<SceneLoader>().GoToScene(endingScene);
                } else if (screenToTrigger == EndScreen.LOSE2) {
                    //EvtSystem.EventDispatcher.Raise<TriggerLoseScreen>(new
                    //        TriggerLoseScreen {});
                    GetComponent<SceneLoader>().GoToScene(endingScene);
                }
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
