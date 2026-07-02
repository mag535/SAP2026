using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager2 : Singleton<DialogueManager2>
{
    public GameObject background;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;
    // TODO: have this load these in later
    public List<Dialogue> allDialogues = new List<Dialogue>();

    private Dialogue currentDialogue;

    public void StartDialogueThread(Dialogue start) {
        currentDialogue = start;
        DisplayDialogue();
        ShowDialogueWindow();
    }

    public void StartDialogueThread(string dialogueID) {
        foreach (Dialogue dialogue in allDialogues) {
            if (dialogue.id == dialogueID) {
                StartDialogueThread(dialogue);
                return;
            }
        }
    }

    public bool ContinueDialogueThread() {
        // end dialogue when END_OF_CONVERSATION reached
        if (currentDialogue.nextDialogueID == "END_OF_CONVERSATION") {
            EndDialogueThread();
            return false;
        }
        // go to next when player presses E or something
        Debug.Log("continue dialogue");
        currentDialogue = GetDialogue(currentDialogue.nextDialogueID);
        DisplayDialogue();
        return true;
    }

    public void EndDialogueThread() {
        Debug.Log("end dialogue");
        currentDialogue = null;
        HideDialogueWindow();
    }

    public void DisplayDialogue() {
        // TODO: set speaker
        // display dialogue
        textBox.text = currentDialogue.text;
    }

    public void ShowDialogueWindow() {
        background.SetActive(true);
        dialogueBox.SetActive(true);
    }

    public void HideDialogueWindow() {
        background.SetActive(false);
        dialogueBox.SetActive(false);
    }

    private Dialogue GetDialogue(string id) {
        foreach (Dialogue dialogue in allDialogues) {
            if (dialogue.id == id) {
                return dialogue;
            }
        }
        return null;
    }
}
