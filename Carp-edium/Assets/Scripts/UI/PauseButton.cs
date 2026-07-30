using UnityEngine;

namespace Carp {
    public class PauseButton : MonoBehaviour
    {
        public void Pause() {
            PauseMenu.Instance.Pause();
        }

        public void Resume() {
            PauseMenu.Instance.Resume();
        }

        public void TogglePause() {
            if (PauseMenu.Instance.IsActive()) {
                PauseMenu.Instance.Resume();
            } else {
                PauseMenu.Instance.Pause();
            }
        }
    }
}
