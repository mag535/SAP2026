using UnityEngine;

namespace Carp {
    public class Door : Openable
    {
        public string nextRoomName;

        void Update() {
            if (isLocked) {
                Lock();
            } else {
                Unlock();
            }
        }

        public override void Interact() {
            if (isLocked) { 
                base.Interact(); 
            } else { 
                AudioManager.Instance.Play(unlockingSoundEffect.name);
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

        void GoToNextRoom() {
            // TODO: what needs to happen:
            // - room loading screen
            // - unload current room
            // - load next room
            // - position player
            // - remove loading screen
            EvtSystem.EventDispatcher.Raise<RequestLoadRoom>(new RequestLoadRoom
                    { roomName = nextRoomName });
        }
    }
}
