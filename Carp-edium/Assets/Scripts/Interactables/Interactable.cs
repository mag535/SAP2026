using UnityEngine;

namespace Carp {
    public abstract class Interactable : MonoBehaviour
    {
        public Sound soundEffect;
        public Object objectData;

        public abstract void Interact();
        public abstract bool HandleItemUse(Object item);
    }
}
