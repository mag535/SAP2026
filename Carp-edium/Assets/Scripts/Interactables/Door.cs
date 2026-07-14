using UnityEngine;

namespace Carp {
    public class Door : Openable
    {
        public override void Interact() {
            base.Interact();

            if (!isLocked) {
                DoSomething();
            }
        }

        public override void Unlock() {
            base.Unlock();

            gameObject.GetComponent<Collider2D>().enabled = false;
            //gameObject.GetComponent<Rigidbody2D>().enabled = false;
        }

        void DoSomething() {
            Debug.Log("Do the Door thing (teleport and change level).");
        }

    }
}
