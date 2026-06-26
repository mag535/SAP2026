using UnityEngine;
using TMPro;

public class ClueManager : Singleton<ClueManager>
{
    public GameObject notebook;
    public GameObject clueTextPrefab;

    private string currentNPC;

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<NewCurrentNPC>(UpdateCurrentNPC);
        EvtSystem.EventDispatcher.AddListener<SendItemName>(CheckValidItemUse);
    }

    void UpdateCurrentNPC(NewCurrentNPC evt) {
        if (evt.set) {
            SetCurrentNPC(evt.npcName);
        } else {
            ClearCurrentNPC();
        }
    }

    private void SetCurrentNPC(string text) {
        currentNPC = text;
    }

    private void ClearCurrentNPC() {
        currentNPC = "";
    }

    public void CheckValidItemUse(SendItemName evt) {
        if (currentNPC == "NPC1" && evt.itemName == "$10") {
            EvtSystem.EventDispatcher.Raise<RequestRemoveItem>(
                    new RequestRemoveItem { itemName = evt.itemName });
        }
        return;
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<NewCurrentNPC>(UpdateCurrentNPC);
        EvtSystem.EventDispatcher.RemoveListener<SendItemName>(CheckValidItemUse);
    }
}
