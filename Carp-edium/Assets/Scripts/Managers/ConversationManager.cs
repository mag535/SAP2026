using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace Carp {
    public class ConversationManager : Singleton<ConversationManager>
    {
        public GameObject displayWindow;
        public GameObject dialogueBox;
        public TextMeshProUGUI textBox;
        public TextMeshProUGUI nameTag;
        public GameObject continueObject;
        public float delayForTypeWriterEffect = 1;

        //private string conversationsFolder = "Conversations/";
        [SerializeField]
        private DialogueContainer _currentConversation;
        private string _currentGuid;

        [SerializeField]
        private bool conversationIsOver = false;

        void Start() {
            EvtSystem.EventDispatcher.AddListener<RequestStartConversation>(StartConversation);
            EvtSystem.EventDispatcher.AddListener<RequestSkipTWEffectConversation>(SkipTWEffect);
            EvtSystem.EventDispatcher.AddListener<RequestContinueConversation>(ContinueConversation);
            EvtSystem.EventDispatcher.AddListener<RequestInterruptConversation>(InterruptConversation);
            EvtSystem.EventDispatcher.AddListener<DialogueFullyShown>(HandleDialogueFullyShown);
            EvtSystem.EventDispatcher.AddListener<NoMoreToShow>(HandleNoMoreToShow);
            continueObject.SetActive(false);
            dialogueBox.SetActive(false);
            displayWindow.SetActive(false);
        }

        private void StartConversation(RequestStartConversation evt) {
            if (evt.start == null) {
                Debug.Log("No dialogue provided");
                return;
            }
            _currentConversation = evt.start;
            ParseConversationData();
            HandleSpecialDialogue(_currentConversation.DialogueNodeData.Find(
                        x => x.Guid == _currentGuid));
            ShowDialogueWindow();
            SetDialogue();
        }

        private bool CheckForMoreDialogue() {
            if (_currentConversation == null) { return false; }
            NodeLinkData nextLinkData = _currentConversation.NodeLinks.Find(
                    x => x.BaseNodeGuid == _currentGuid);
            DialogueNodeData nextNode = _currentConversation.DialogueNodeData.Find(
                    x => x.Guid == nextLinkData.TargetNodeGuid);
            // If next node marks end of conversation, end conversation
            if (nextNode.type == DialogueType.ENDOFCONVERSATION) { 
                return false;
            }
            return true;
        }

        private void SkipTWEffect(RequestSkipTWEffectConversation _) {
            EvtSystem.EventDispatcher.Raise<ShowFullDialogue>( new
                    ShowFullDialogue {});
        }

        private void HandleNoMoreToShow(NoMoreToShow _) {
            ContinueConversation(null);
        }

        private void HandleDialogueFullyShown(DialogueFullyShown _) {
            if (!CheckForMoreDialogue()) {
                conversationIsOver = true;
                return;
            }
            continueObject.SetActive(true);
        }

        private void ContinueConversation(RequestContinueConversation _) {
            // Advance to next dialogue node data
            NodeLinkData currentLinkData = _currentConversation.NodeLinks.Find(
                    x => x.BaseNodeGuid == _currentGuid);
            // If none found, end conversation
            if (currentLinkData == null) { 
                EndConversation();
                EvtSystem.EventDispatcher.Raise<ContinueResult>( new
                        ContinueResult { result = false });
                return;
            }

            DialogueNodeData nextNode = _currentConversation.DialogueNodeData.Find(
                    x => x.Guid == currentLinkData.TargetNodeGuid);
            // If next node marks end of conversation, end conversation
            if (nextNode.type == DialogueType.ENDOFCONVERSATION) { 
                EndConversation(); 
                EvtSystem.EventDispatcher.Raise<ContinueResult>( new
                        ContinueResult { result = false });
                return;
            }

            // Otherwise, update what current GUID is:
            _currentGuid = nextNode.Guid;
            conversationIsOver = false;

            // Display dialogue
            HandleSpecialDialogue(nextNode);
            SetDialogue();
            AudioManager.Instance.PlayContinueSFX();
            EvtSystem.EventDispatcher.Raise<ContinueResult>( new
                    ContinueResult { result = true });
        }

        public void EndConversation() {
            _currentConversation = null;
            _currentGuid = string.Empty;
            HideDialogueWindow();
            EvtSystem.EventDispatcher.Raise<DialogueEnd>( new DialogueEnd {});
            conversationIsOver = false;
        }

        // End current converstaion and start new one
        private void InterruptConversation(RequestInterruptConversation evt) {
            if (evt.newConversation == null) { return; }
            conversationIsOver = false;
            HideDialogueWindow();
            EvtSystem.EventDispatcher.Raise<RequestStartConversation>( new
                    RequestStartConversation { start = evt.newConversation });
            /*
            _currentConversation = evt.newConversation;
            ParseConversationData();
            HandleSpecialDialogue(_currentConversation.DialogueNodeData.Find( x =>
                        x.Guid == _currentGuid));
            ShowDialogueWindow();
            SetDialogue();
            */
        }

        public void SetDialogue() {
            // Set speaker
            nameTag.text = _currentConversation.DialogueNodeData.Find(x =>
                    x.Guid == _currentGuid).speaker;
            string dialogueText = _currentConversation.DialogueNodeData.Find(x =>
                    x.Guid == _currentGuid).DialogueText;
            // Display dialogue text
            StartCoroutine(Delay(delayForTypeWriterEffect, dialogueText));
        }

        private IEnumerator Delay(float duration, string dialogueText) {
            yield return new WaitForSeconds(duration);
            EvtSystem.EventDispatcher.Raise<SendDialogueText>( new SendDialogueText
                    { dialogueText = dialogueText });
        }

        public void ShowDialogueWindow() {
            dialogueBox.SetActive(true);
            displayWindow.SetActive(true);
        }

        public void HideDialogueWindow() {
            continueObject.SetActive(false);
            dialogueBox.SetActive(false);
            displayWindow.SetActive(false);
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
                break;
            }
            // If no ends found, return
            if (_endOfConversation == null) { return; }

            // Find first node in dialogue tree
            _currentGuid = FindFirstNode(_endOfConversation.Guid);
        }

        string FindFirstNode(string endGuid) {
            bool restart = false;
            string firstNodeGuid = endGuid;

            // This actually finds the GUID for the Start node
            for (int i = 0; i < _currentConversation.NodeLinks.Count; i++) {
                if (restart) {
                    restart = false;
                    i = 0;
                }
                // if found newer node, update first node guid and reset loop
                if (_currentConversation.NodeLinks[i].TargetNodeGuid ==
                        firstNodeGuid) {
                    firstNodeGuid = _currentConversation.NodeLinks[i].BaseNodeGuid;
                    restart = true;
                    if (i == _currentConversation.NodeLinks.Count-1) { i=0; }
                }
                // otherwise, continue checking
            }

            // Shift to actual first dialogue node
            firstNodeGuid = _currentConversation.NodeLinks.Find( x =>
                    x.BaseNodeGuid == firstNodeGuid ).TargetNodeGuid;

            return firstNodeGuid;
        }

        void HandleSpecialDialogue(DialogueNodeData node) {
            switch (node.type) {
            case DialogueType.BRANCH:
                Debug.Log("Not yet implemented.");
                break;
            case DialogueType.GIVEITEM:
                HandleGiveItemDialogue(node);
                break;
            case DialogueType.SETFLAG:
                HandleSetFlagDialogue(node);
                break;
            }
        }

        void HandleGiveItemDialogue(DialogueNodeData node) {
            if (node.cost == null) {
                if (node.trade.isNoteEntry) {
                    EvtSystem.EventDispatcher.Raise<RequestAddToNotebook>( new
                            RequestAddToNotebook { objectData = node.trade });
                } else {
                    EvtSystem.EventDispatcher.Raise<RequestAddItem>( new
                            RequestAddItem { item = node.trade });
                }
                AudioManager.Instance.PlayDeduction();
                return;
            }

            // TODO: what to do? How to handle cost item?
        }

        void HandleSetFlagDialogue(DialogueNodeData node) {
            if (node.flag == string.Empty) { return; }

            EvtSystem.EventDispatcher.Raise<PropagateFlag>( new
                    PropagateFlag { flag = node.flag });
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestStartConversation>(StartConversation);
            EvtSystem.EventDispatcher.RemoveListener<RequestSkipTWEffectConversation>(SkipTWEffect);
            EvtSystem.EventDispatcher.RemoveListener<RequestContinueConversation>(ContinueConversation);
            EvtSystem.EventDispatcher.RemoveListener<RequestInterruptConversation>(InterruptConversation);
            EvtSystem.EventDispatcher.RemoveListener<DialogueFullyShown>(HandleDialogueFullyShown);
        }
    }
}
