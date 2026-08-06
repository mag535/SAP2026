using UnityEngine;

namespace Carp {
    public class PlayerInteractionCancel : MonoBehaviour
    {
        private PlayerInteract playerInteract;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerInteract = gameObject.transform.parent.gameObject.GetComponent<PlayerInteract>();
        }

        void OnTriggerExit2D(Collider2D _) {
            playerInteract.CancelInteraction();
        }
    }
}
