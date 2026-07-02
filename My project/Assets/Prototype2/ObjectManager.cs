using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : Singleton<ObjectManager>
{
    [System.Serializable]
    public struct objectPairing {
        public string host;
        public Object key;
        public Object val;
    }
    public List<objectPairing> objectUnlocks = new List<objectPairing>();

    private GameObject focusedInteractable;

    public void SetFocusedInteractable(GameObject focused) {
        focusedInteractable = focused;
    }
    public void ClearFocusedInteractable() {
        focusedInteractable = null;
    }

    public bool CheckValidItemUse(string itemID) {
        foreach (objectPairing pair in objectUnlocks) {
            if (pair.key.objectID == itemID && pair.host == focusedInteractable.name) {
                return true;
            }
        }
        return false;
    }

    public void ExchangeItem(string itemID) {
        foreach (objectPairing pair in objectUnlocks) {
            if (pair.key.objectID == itemID) {
                EvtSystem.EventDispatcher.Raise<RequestAddItem>(new RequestAddItem {
                        item = pair.val });
                return;
            }
        }
    }
}
