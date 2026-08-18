using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class ConversationStarter : Interactable
    {
        public DialogueContainer conversationStart;
        public DialogueContainer conversationDefault;
        public List<ItemConvoPair> itemConvoPairList = new List<ItemConvoPair>();

        private bool goToDefaultDialogue = false;
        private DialogueContainer wrongItemConversation = null;

        void Start() {
            foreach (ItemConvoPair pair in itemConvoPairList) {
                if (pair.itemTrigger == null) {
                    wrongItemConversation = pair.conversation;
                }
            }
        }

        public override void Interact() {
            // sfx
            AudioManager.Instance.Play(soundEffect);
            // start dialogue, send dialogue id to dialogue manager
            if (!goToDefaultDialogue) {
                ConversationManager.Instance.StartConversation(conversationStart);
                goToDefaultDialogue = true;
            } else {
                ConversationManager.Instance.StartConversation(conversationDefault);
            }

        }

        public override bool HandleItemUse(Object item) {
            bool wrongItemFlag = true;
            DialogueContainer correspondingConversation = null;

            foreach (ItemConvoPair pair in itemConvoPairList) {
                if (item.objectID == pair.itemTrigger.objectID) {
                    correspondingConversation = pair.conversation;
                    wrongItemFlag = false;
                    break;
                }
            }

            if (wrongItemFlag) {
                correspondingConversation = wrongItemConversation;
            }

            ConversationManager.Instance.InterruptConversation(
                    correspondingConversation);

            return !wrongItemFlag;
        }
    }
}
