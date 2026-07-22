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
        public Vector2 position;

        public FoxUnlockPosition(string an, Vector2 pos) {
            areaName = an;
            position = pos;
        }
    }
}
