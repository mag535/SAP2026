using UnityEngine;

namespace Carp {
    public class NotificationButton : MonoBehaviour
    {
        [SerializeField]
        private float lifeTime = 3f;

        void Start() {
            Destroy(gameObject, lifeTime);
        }

        public void Close() {
            Destroy(gameObject);
        }
    }
}
