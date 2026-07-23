using UnityEngine;

namespace Carp {
    public class NotificationButton : MonoBehaviour
    {
        public void Close() {
            Destroy(gameObject);
        }
    }
}
