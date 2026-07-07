using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class ConversationStarter : Interactable
    {
        public struct ItemConvoPair {
            public Object itemTrigger;
            public DialogueContainer conversation;
        }

        public DialogueContainer conversationStart;
        public List<ItemConvoPair> itemConvoPairList = new List<ItemConvoPair>();

        public override void Interact() {
            // sfx
            AudioManager.Instance.Play(soundEffect.name);
            // start dialogue, send dialogue id to dialogue manager
            ConversationManager.Instance.StartConversation(conversationStart);
        }

        public override void HandleItemUse(Object item) {
            DialogueContainer correspondingConversation = null;
            foreach (ItemConvoPair pair in itemConvoPairList) {
                if (item.objectID == pair.itemTrigger.objectID) {
                    correspondingConversation = pair.conversation;
                    break;
                }
            }

            ConversationManager.Instance.InterruptConversation(
                    correspondingConversation);
        }
    }
}
