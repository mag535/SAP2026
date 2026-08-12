using UnityEngine;
using UnityEngine.InputSystem;

namespace Carp {
    public class PlayerMenu : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        public void TogglePauseMenu(InputAction.CallbackContext context) {
            if (context.canceled) {
                if (PauseMenu.Instance.IsActive()) {
                    PauseMenu.Instance.Resume();
                } else {
                    PauseMenu.Instance.Pause();
                }
            }
        }
    }
}
