using UnityEngine;

namespace Carp {
    public class Door : Openable
    {
        public RoomName nextRoomName;

        private bool isFirstTime = true;

        public override void Interact() {
            if (isLocked) { 
                base.Interact(); 
            } else { 
                AudioManager.Instance.Play(unlockingSoundEffect);
                GoToNextRoom();
            }
        }

        public override void Unlock() {
            base.Unlock();

            //gameObject.GetComponent<Collider2D>().enabled = false;
            //gameObject.GetComponent<Rigidbody2D>().enabled = false;
        }

        public override void Lock() {
            base.Lock();

            //gameObject.GetComponent<Collider2D>().enabled = true;
            //gameObject.GetComponent<Rigidbody2D>().enabled = false;
        }

        public bool GetIsFirstTime() {
            return isFirstTime;
        }

        public void GoToNextRoom() {
            RoomTransitionManager.Instance.DoRoomTransition(nextRoomName);

            if (isFirstTime) {
                isFirstTime = false;
                ChangeFoxPosition someScript = GetComponent<ChangeFoxPosition>();
                if (someScript != null) {
                    someScript.RaiseChange();
                }
            }
        }
    }
}
