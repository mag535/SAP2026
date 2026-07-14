using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Carp {
    public class PlayerInventory : MonoBehaviour
    {
        public List<Object> inventory = new List<Object>();

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestAddItem>(AddItem);
            EvtSystem.EventDispatcher.AddListener<RequestRemoveItem>(RemoveItem);
        }

        public void AddItem(RequestAddItem evt) {
            // Add item to inventory list
            inventory.Add(evt.item);

            // Send signal to inventory UI manager to create new
            // item display
            EvtSystem.EventDispatcher.Raise<RequestAddToInventoryDisplay>(
                    new RequestAddToInventoryDisplay { objectData = evt.item });
        }

        public void RemoveItem(RequestRemoveItem evt) {
            // Remove item from list
            for (int i = 0; i < inventory.Count; i++) {
                if (inventory[i].objectID == evt.item.objectID) {
                    inventory.RemoveAt(i);
                    break;
                }
            }
            Debug.Log("removed item [" + evt.item.objectID + "] from *inventory*");

            // TODO: send signal to remove item from inventory display
            EvtSystem.EventDispatcher.Raise<RequestRemoveFromInventoryDisplay>(
                    new RequestRemoveFromInventoryDisplay { 
                        objectData = evt.item });
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestAddItem>(AddItem);
            EvtSystem.EventDispatcher.RemoveListener<RequestRemoveItem>(RemoveItem);
        }
    }
}
