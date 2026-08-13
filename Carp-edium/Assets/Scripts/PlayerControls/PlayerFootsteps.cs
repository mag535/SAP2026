using UnityEngine;

namespace Carp {
    public class PlayerFootsteps : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private AudioSource footsteps;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            foreach (Transform childtrasform in gameObject.transform) {
                footsteps = childtrasform.gameObject.GetComponent<AudioSource>();
                if (footsteps != null) { break; }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (playerMovement.GetIsMoving()) {
                if (footsteps != null && !footsteps.isPlaying) {
                    footsteps.Play();
                }
            } else {
                if (footsteps != null && footsteps.isPlaying) {
                    footsteps.Stop();
                }
            }
        }
    }
}
