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
    private string pendingClue = "";

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<RequestRemoveItem>(RemoveItem);
    }

    public void SetPendingClue(string text) {
        playerInteract = GetComponent<PlayerInteract>();
        pendingClue = text;
        Debug.Log("pending clue: \"" + pendingClue + "\"");
    }

    public void PopulateNotebook() {
        if (string.IsNullOrEmpty(pendingClue)) {
            return;
        }
        GameObject newClue = Instantiate(clueTextPrefab, inventoryDisplay.transform);
        newClue.name = pendingClue;
        newClue.GetComponent<TextMeshProUGUI>().text = pendingClue;

        Button newClueBtn = newClue.GetComponent<Button>();
        //newClueBtn.onClick.AddListener(playerInteract.UseItem(pendingClue));

        inventory.Add(newClue);
        Debug.Log("populated *inventoryDisplay* with clue \"" + pendingClue + "\"");
        pendingClue = "";
    }

    public void RemoveItem(RequestRemoveItem evt) {
        GameObject target = null;
        for(int i = 0; i < inventory.Count; i++) {
            if (inventory[i].name == evt.itemName) {
                target = inventory[i];
                inventory.RemoveAt(i);
                break;
            }
        }
        if (target != null) {
            Destroy(target);
        }
        // TODO: also get NPC dialogue to update!
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<RequestRemoveItem>(RemoveItem);
    }
}
