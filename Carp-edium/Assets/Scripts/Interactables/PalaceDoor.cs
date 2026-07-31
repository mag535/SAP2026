using UnityEngine;

namespace Carp {
    public class PalaceDoor : Door
    {
        public override void Unlock() {
            base.Unlock();

            EvtSystem.EventDispatcher.Raise<PropagateFlag>(new PropagateFlag
                    { flag = "PalaceIsUnlocked" });
        }
    }
}
