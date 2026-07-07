using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConversationManager : Singleton<ConversationManager>
{
    public GameObject background;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;

    //private string conversationsFolder = "Conversations/";
    private DialogueContainer currentConversation;

    public void StartConversation(DialogueContainer start) {
        currentConversation = start;
        DisplayDialogue();
        ShowDialogueWindow();
    }

    // FIXME: use DialogueContainer data
    public bool ContinueConversation() {
        // TODO: end dialogue when END_OF_CONVERSATION reached
        
        // TODO: go to next
        Debug.Log("continue conversation");

        // TODO: display dialogue
        return true;
    }

    public void EndConversation() {
        Debug.Log("end conversation");
        currentConversation = null;
        HideDialogueWindow();
    }

    // TODO: end current converstaion and start new one
    public void InterruptConversation(DialogueContainer newConversation) {
    }

    // FIXME: parse dialogue node types and get data
    public void DisplayDialogue() {
        // TODO: set speaker
        // TODO: display dialogue text
    }

    public void ShowDialogueWindow() {
        background.SetActive(true);
        dialogueBox.SetActive(true);
    }

    public void HideDialogueWindow() {
        background.SetActive(false);
        dialogueBox.SetActive(false);
    }

    private void ParseConversationData() {
        if (currentConversation == null) { return; }
        // TODO: get some information on current dialogue node then parse
        // current conversation to get next node
        // TODO: HELP! How to I determine the first node in the graph??
        // Work backwards from ENDOFCONVERSATION type?
    }
}
