using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Object> inventory = new List<Object>();

    void AddItem(Object obj) {
        if (CheckIfObjectInInventory(obj)) {
            return;
        }
        inventory.Add(obj);
    }

    void RemoveItem(Object obj) {
        if (!CheckIfObjectInInventory(obj)) {
            return;
        }
        inventory.Remove(obj);
    }

    void DisplayInventory() {
        Debug.Log("Inventory:");
        foreach (Object obj in inventory) {
            Debug.Log("    - " + obj.objectID);
        }
    }

    bool CheckIfObjectInInventory(Object newObj) {
        foreach (Object obj in inventory) {
            if (obj.objectID == newObj.objectID) {
                return true;
            }
        }
        return false;
    }
}
