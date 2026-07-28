using UnityEngine;
using System.Collections.Generic;


namespace Carp {
    public class RoomTransitionManager : Singleton<RoomTransitionManager>
    {
        [System.Serializable]
        public struct PlayerEntrancePosition {
            public RoomName currentRoom;
            public RoomName nextRoom;
            public Vector2 entrancePosition;
        }

        public RoomName initialRoom;
        public List<PlayerEntrancePosition> playerEntrancePositions = 
            new List<PlayerEntrancePosition>();

        private RoomName _currentRoom;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _currentRoom = RoomName.NONE;
            DoRoomTransition(initialRoom);
        }

        public void DoRoomTransition(RoomName roomName) {
            // TODO: Turn on loading screen

            // Send signal for room loading / unloading
            EvtSystem.EventDispatcher.Raise<RequestLoadRoom>(new RequestLoadRoom
                    { roomName = Stuff.roomNameDict[roomName] });

            // Send signal for player position change
            EvtSystem.EventDispatcher.Raise<RequestChangePlayerPosition>(new RequestChangePlayerPosition
                    { newPosition = GetEntrancePosition(roomName) });

            // TODO: Turnoff loading screen

            // Send signal for player state change
            EvtSystem.EventDispatcher.Raise<RequestChangePlayerState>(new RequestChangePlayerState
                    { newState = "GAME" });

            _currentRoom = roomName;
        }

        private Vector2 GetEntrancePosition(RoomName nextRoom) {
            foreach (PlayerEntrancePosition data in playerEntrancePositions) {
                if (_currentRoom == data.currentRoom && nextRoom == data.nextRoom) {
                    return data.entrancePosition;
                }
            }

            return Vector2.zero;
        }
    }
}
