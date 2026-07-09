using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConversationManager : Singleton<ConversationManager>
{
    public GameObject background;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI nameTag;

    //private string conversationsFolder = "Conversations/";
    private DialogueContainer _currentConversation;
    private string _currentGuid;

    public void StartConversation(DialogueContainer start) {
        _currentConversation = start;
        ParseConversationData();
        SetDialogue();
        ShowDialogueWindow();
    }

    public bool ContinueConversation() {
        // Advance to next dialogue node data
        // FIXME: account for options (BRANCH type)
        Debug.Log("continuing conversation...");
        NodeLinkData currentLinkData = _currentConversation.NodeLinks.Find(
                x => x.BaseNodeGuid == _currentGuid);
        // If none found, end conversation
        if (currentLinkData == null) { 
            EndConversation();
            return false;
        }

        DialogueNodeData nextNode = _currentConversation.DialogueNodeData.Find(
                x => x.Guid == currentLinkData.TargetNodeGuid);
        // If next node marks end of conversation, end conversation
        if (nextNode.type == DialogueType.ENDOFCONVERSATION) { 
            EndConversation(); 
            return false;
        }

        // Otherwise, update what current GUID is:
        _currentGuid = nextNode.Guid;

        // Display dialogue
        SetDialogue();
        ShowDialogueWindow();
        return true;
    }

    public void EndConversation() {
        Debug.Log("end conversation");
        _currentConversation = null;
        _currentGuid = string.Empty;
        HideDialogueWindow();
    }

    // End current converstaion and start new one
    public void InterruptConversation(DialogueContainer newConversation) {
        EndConversation();
        _currentConversation = newConversation;
        ParseConversationData();
        SetDialogue();
        ShowDialogueWindow();
    }

    public void SetDialogue() {
        // TODO: set speaker
        nameTag.text = _currentConversation.DialogueNodeData.Find(x =>
                x.Guid == _currentGuid).speaker;
        // Display dialogue text
        textBox.text = _currentConversation.DialogueNodeData.Find(x =>
                x.Guid == _currentGuid).DialogueText;
    }

    public void ShowDialogueWindow() {
        background.SetActive(true);
        dialogueBox.SetActive(true);
    }

    public void HideDialogueWindow() {
        textBox.text = string.Empty;
        background.SetActive(false);
        dialogueBox.SetActive(false);
    }

    // Get some information on current dialogue node then parse
    // current conversation to get next node
    // HELP! How to I determine the first node in the graph??
    // Work backwards from ENDOFCONVERSATION type?
    private void ParseConversationData() {
        if (_currentConversation == null) { return; }

        // Find one END_OF_CONVERSATION nodes
        DialogueNodeData _endOfConversation = null;
        foreach (DialogueNodeData data in _currentConversation.DialogueNodeData) {
            if (data.type != DialogueType.ENDOFCONVERSATION) { continue; }
            _endOfConversation = data;
        }
        // If no ends found, return
        if (_endOfConversation == null) { return; }

        // Find first node in dialogue tree
        _currentGuid = FindFirstNode(_endOfConversation.Guid);
    }

    string FindFirstNode(string endGuid) {
        string firstNodeGuid = endGuid;

        for (int i = 0; i < _currentConversation.NodeLinks.Count; i++) {
            // if found newer node, update first node guid and reset loop
            if (_currentConversation.NodeLinks[i].TargetNodeGuid ==
                    firstNodeGuid) {
                firstNodeGuid = _currentConversation.NodeLinks[i].BaseNodeGuid;
                i = 0;
            }
            // otherwise, continue checking
        }

        return firstNodeGuid;
    }
}
