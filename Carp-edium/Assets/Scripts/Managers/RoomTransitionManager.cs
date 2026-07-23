using UnityEngine;
using System.Collections.Generic;


namespace Carp {
    public class RoomTransitionManager : Singleton<RoomTransitionManager>
    {
        [System.Serializable]
        public struct PlayerEntrancePosition {
            public string roomName;
            public Vector2 playerEntrancePosition;
        }

        public string initialRoom;
        public List<PlayerEntrancePosition> playerEntrancePositions = 
            new List<PlayerEntrancePosition>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LoadInitialRoom();
        }

        void LoadInitialRoom() {
            EvtSystem.EventDispatcher.Raise<RequestLoadRoom>(new RequestLoadRoom
                    { roomName = initialRoom });

            Vector2 newPosition = Vector2.zero;
            foreach (PlayerEntrancePosition pair in playerEntrancePositions) {
                if (pair.roomName == initialRoom) {
                    newPosition = pair.playerEntrancePosition;
                    break;
                }
            }
            EvtSystem.EventDispatcher.Raise<RequestChangePlayerPosition>(new RequestChangePlayerPosition
                    { newPosition = newPosition });

            EvtSystem.EventDispatcher.Raise<RequestChangePlayerState>(new RequestChangePlayerState
                    { newState = "GAME" });
        }

        public void DoRoomTransition(string roomName) {
            // TODO: Turn on loading screen

            // Send signal for room loading / unloading
            EvtSystem.EventDispatcher.Raise<RequestLoadRoom>(new RequestLoadRoom
                    { roomName = roomName });

            // Send signal for player position change
            Vector2 newPosition = Vector2.zero;
            foreach (PlayerEntrancePosition pair in playerEntrancePositions) {
                if (pair.roomName == roomName) {
                    newPosition = pair.playerEntrancePosition;
                    break;
                }
            }
            EvtSystem.EventDispatcher.Raise<RequestChangePlayerPosition>(new RequestChangePlayerPosition
                    { newPosition = newPosition });

            // TODO: Turnoff loading screen

            // Send signal for player state change
            EvtSystem.EventDispatcher.Raise<RequestChangePlayerState>(new RequestChangePlayerState
                    { newState = "GAME" });
        }
    }
}
