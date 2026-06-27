using UnityEngine;

public class Item : MonoBehaviour
{
    public Object objectData;

    public void AttemptItemUse() {
        Debug.Log(" attempting item use...");
        EvtSystem.EventDispatcher.Raise<RequestItemUse>(
                new RequestItemUse { item = objectData });
    }
}
