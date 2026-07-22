using UnityEngine;


namespace Carp {
    // Used in Trader
    [System.Serializable]
    public struct Listing {
        public Object cost;
        public Object trade;
    }

    // Used in ConversationStarter
    [System.Serializable]
    public struct ItemConvoPair {
        public Object itemTrigger;
        public DialogueContainer conversation;
    }

    [System.Serializable]
    public struct FoxUnlockPosition {
        public string areaName;
        public Transform position;
    }
}
