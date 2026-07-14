using UnityEngine;

namespace Carp {
    public abstract class Interactable : MonoBehaviour
    {
        public Sound soundEffect;

        public abstract void Interact();
        public abstract bool HandleItemUse(Object item);
    }
}
