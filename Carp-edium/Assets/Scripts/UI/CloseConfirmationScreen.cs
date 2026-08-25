using UnityEngine;


namespace Carp {
    public class CloseConfirmationScreen : MonoBehaviour
    {
        public void AttemptCloseConfirmationScreen() {
            EvtSystem.EventDispatcher.Raise<RequestCloseConfirmationScreen>( new
                    RequestCloseConfirmationScreen {});
        }
    }
}
