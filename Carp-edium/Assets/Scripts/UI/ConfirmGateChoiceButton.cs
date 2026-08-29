using UnityEngine;

namespace Carp {
    public class ConfirmGateChoiceButton : MonoBehaviour
    {
        public void DoConfirmChoice() {
            EvtSystem.EventDispatcher.Raise<ConfirmGateChoice>( new
                    ConfirmGateChoice {});
        }
    }
}
