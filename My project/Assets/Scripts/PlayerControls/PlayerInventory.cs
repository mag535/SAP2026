using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventoryDisplay;
    public GameObject clueTextPrefab;
    public List<GameObject> inventory = new List<GameObject>();

    private PlayerInteract playerInteract;

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<RequestAddItem>(AddItem);
        EvtSystem.EventDispatcher.AddListener<RequestRemoveItem>(RemoveItem);
    }

    public void AddItem(RequestAddItem evt) {
        GameObject newItem = Instantiate(clueTextPrefab, inventoryDisplay.transform);
        newItem.name = evt.item.objectID;
        newItem.GetComponent<TextMeshProUGUI>().text = evt.item.description;

        Item newItemItem = newItem.GetComponent<Item>();
        newItemItem.objectData = evt.item;

        Button newItemBtn = newItem.GetComponent<Button>();
        newItemBtn.onClick.AddListener(newItemItem.AttemptItemUse);

        inventory.Add(newItem);
        Debug.Log("populated *inventoryDisplay* with item [" + evt.item.objectID + "]");
    }

    public void RemoveItem(RequestRemoveItem evt) {
        GameObject target = null;
        for(int i = 0; i < inventory.Count; i++) {
            if (inventory[i].name == evt.item.objectID) {
                target = inventory[i];
                inventory.RemoveAt(i);
                break;
            }
        }
        if (target != null) {
            Destroy(target);
        }
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<RequestAddItem>(AddItem);
        EvtSystem.EventDispatcher.RemoveListener<RequestRemoveItem>(RemoveItem);
    }
}
