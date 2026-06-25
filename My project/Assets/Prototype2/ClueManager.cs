using UnityEngine;
using TMPro;

public class ClueManager : MonoBehaviour
{
    public GameObject notebook;
    public GameObject clueTextPrefab;

    private string pendingClue;

    public void SetPendingClue(string text) {
        pendingClue = text;
        Debug.Log("pending clue: \"" + pendingClue + "\"");
    }

    public void PopulateNotebook() {
        if (string.IsNullOrEmpty(pendingClue)) {
            return;
        }
        GameObject newClue = Instantiate(clueTextPrefab, notebook.transform);
        newClue.GetComponent<TextMeshProUGUI>().text = pendingClue;
        Debug.Log("populated *notebook* with clue \"" + pendingClue + "\"");
        pendingClue = "";
    }
}
