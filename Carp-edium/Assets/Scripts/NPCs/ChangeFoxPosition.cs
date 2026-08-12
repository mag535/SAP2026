using UnityEngine;

namespace Carp {
    public class ChangeFoxPosition : MonoBehaviour
    {
        public void RaiseChange() {
            EvtSystem.EventDispatcher.Raise<PropagateFlag>(new PropagateFlag
                    { flag = "FoxToBarPosition" });
        }
    }
}
