using UnityEngine;

public class CloseUIControls : MonoBehaviour
{
    public void SendCloseInventoryEvent() {
        EvtSystem.EventDispatcher.Raise<RequestCloseInventory>(new
                RequestCloseInventory {});
    }

    public void SendCloseNotebookEvent() {
        EvtSystem.EventDispatcher.Raise<RequestCloseNotebook>(new
                RequestCloseNotebook {});
    }
}
