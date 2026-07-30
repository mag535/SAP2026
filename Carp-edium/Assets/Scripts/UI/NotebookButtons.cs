using UnityEngine;

namespace Carp {
    public class NotebookButtons : MonoBehaviour
    {
        public void PageUp() {
            EvtSystem.EventDispatcher.Raise<RequestPreviousPage>(new
                    RequestPreviousPage {});
        }

        public void PageDown() {
            EvtSystem.EventDispatcher.Raise<RequestNextPage>(new
                    RequestNextPage {});
        }
    }
}
