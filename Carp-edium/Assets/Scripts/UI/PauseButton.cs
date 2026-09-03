using UnityEngine;

namespace Carp {
    public class PauseButton : MonoBehaviour
    {
        public void Pause() {
            EvtSystem.EventDispatcher.Raise<TogglePauseMenu>( new TogglePauseMenu {});
        }

        public void Resume() {
            EvtSystem.EventDispatcher.Raise<TogglePauseMenu>( new TogglePauseMenu {});
        }
    }
}
