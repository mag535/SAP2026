using UnityEngine;
using System.Collections.Generic;


namespace Carp {
    public enum PlayerStates {
        GAME,
        DIALOGUE,
        DESCRIPTION,
        ROOMTRANSITION,
    }

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

    [System.Serializable]
    public enum RoomName {
        NONE,
        SPAWN,
        HUB,
        BAR,
        TEMPLE,
        PALACE,
        TESTING,
        MELISSA,
        SERENA
    }

    [System.Serializable]
    public enum EndScreen {
        NONE,
        DRAGON,
        QILIN,
        SNAKE
    }

    public class Stuff {
        public static Dictionary<RoomName, string> roomNameDict = new 
            Dictionary<RoomName, string>() {
            { RoomName.SPAWN, "0_PlayerSpawn" },
            { RoomName.HUB, "1_CenterHub" },
            { RoomName.BAR, "2_WhiteTigerBar" },
            { RoomName.TEMPLE, "3_AzureDragonTemple" },
            { RoomName.PALACE, "4_BlackTortoisePalace" },
            // Testing Rooms
            { RoomName.TESTING, "Testing" },
            { RoomName.MELISSA, "MelissaTesting" },
            { RoomName.SERENA, "SerenaTesting" }
        };
        public static Dictionary<EndScreen, string> endScreenDict = new 
            Dictionary<EndScreen, string>() {
            { EndScreen.DRAGON, "5_DragonEnding" },
            { EndScreen.QILIN, "6_QiLinEnding" },
            { EndScreen.SNAKE, "7_SnakeEnding" }
        };
    }
}
