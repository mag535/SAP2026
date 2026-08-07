using UnityEngine;

namespace Carp {
    public class PlayerInteractionEnable : MonoBehaviour
    {
        private PlayerInteract playerInteract;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerInteract = gameObject.transform.parent.gameObject.GetComponent<PlayerInteract>();
        }

        void OnTriggerEnter2D(Collider2D other) {
            playerInteract.EnableInteraction(other.gameObject);
        }

        void OnTriggerExit2D(Collider2D _) {
            playerInteract.CancelInteraction();
        }
    }
}
