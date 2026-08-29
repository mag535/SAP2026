using UnityEngine;

public class Item : MonoBehaviour
{
    public Object objectData;

    public void AttemptItemUse() {
        Debug.Log(" attempting item use...");
        EvtSystem.EventDispatcher.Raise<RequestItemUse>(
                new RequestItemUse { item = objectData });
        EvtSystem.EventDispatcher.Raise<RequestCloseNotebook>( new 
                RequestCloseNotebook {});
        EvtSystem.EventDispatcher.Raise<RequestCloseInventory>( new 
                RequestCloseInventory {});
    }
}
