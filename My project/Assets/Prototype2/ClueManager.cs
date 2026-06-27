using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ClueManager : Singleton<ClueManager>
{
    public GameObject notebook;
    public GameObject clueTextPrefab;
    public List<string> foundClues = new List<string>();

    private string currentNPC;

    public void UpdateFoundClueList(string id) {
        if (CheckHasClueBeenAdded(id)) {
            return;
        }
        foundClues.Add(id);
    }

    public bool CheckHasClueBeenAdded(string id) {
        foreach (string clueID in foundClues) {
            if (clueID == id) {
                return true;
            }
        }
        return false;
    }

    public void SetCurrentNPC(string text) {
        currentNPC = text;
    }

    public void ClearCurrentNPC() {
        currentNPC = "";
    }

    public bool CheckValidItemUse(string itemID) {
        if (currentNPC == "NPC1" && itemID == "item-01") {
            return true;
        }
        return false;
    }

    void OnDestroy() {
    }
}
