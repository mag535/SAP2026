using UnityEngine;

public class PlayerUIControls : MonoBehaviour
{
    public void SendOpenInventoryEvent() {
        EvtSystem.EventDispatcher.Raise<RequestOpenInventory>(new
                RequestOpenInventory {});
    }

    public void SendOpenNotebookEvent() {
        EvtSystem.EventDispatcher.Raise<RequestOpenNotebook>(new
                RequestOpenNotebook {});
    }
}
