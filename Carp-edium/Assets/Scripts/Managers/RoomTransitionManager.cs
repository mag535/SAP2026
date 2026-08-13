using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


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

        [SerializeField]
        private GameObject loadingScreen;
        [SerializeField]
        private List<string> excludedScenes;

        private RoomName _currentRoom;

        void OnEnable() {
            SceneManager.sceneLoaded += TurnOffLoadingScreen;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            loadingScreen.SetActive(false);
            _currentRoom = RoomName.NONE;
            DoRoomTransition(initialRoom);
        }


        void TurnOnLoadingScreen() {
            loadingScreen.SetActive(true);
            EvtSystem.EventDispatcher.Raise<TurnOffPlayerControls>( new
                    TurnOffPlayerControls {});
        }

        // Do after new sceen had been loaded
        void TurnOffLoadingScreen(Scene scene, LoadSceneMode mode) {
            Debug.Log($"Finished loading scene [{scene.name}] in mode [{mode}]");
            EvtSystem.EventDispatcher.Raise<TurnOnPlayerControls>( new
                    TurnOnPlayerControls {});
            loadingScreen.SetActive(false);
            // TODO: play corresponding BGM of rooms
        }

        public void DoRoomTransition(RoomName roomName) {
            // Turns on loading screen and turns off player controls
            TurnOnLoadingScreen();

            // Send signal for room loading / unloading
            EvtSystem.EventDispatcher.Raise<RequestLoadRoom>(new RequestLoadRoom
                    { roomName = Stuff.roomNameDict[roomName] });

            // Send signal for player position change
            EvtSystem.EventDispatcher.Raise<RequestChangePlayerPosition>(new RequestChangePlayerPosition
                    { newPosition = GetEntrancePosition(roomName) });

            // Turnoff loading screen is handled by callback

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

        void OnDisable() {
            SceneManager.sceneLoaded -= TurnOffLoadingScreen;
        }

    }
}
