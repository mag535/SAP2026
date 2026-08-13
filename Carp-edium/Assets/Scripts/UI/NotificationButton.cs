using UnityEngine;

namespace Carp {
    public class NotificationButton : MonoBehaviour
    {
        void Start() {
            Destroy(gameObject, 8);
        }

        public void Close() {
            Destroy(gameObject);
        }
    }
}
