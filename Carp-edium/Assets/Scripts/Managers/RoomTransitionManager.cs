using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;


namespace Carp {
    public class RoomTransitionManager : Singleton<RoomTransitionManager>
    {
        [System.Serializable]
        public struct PlayerEntrancePosition {
            public RoomName currentRoom;
            public RoomName nextRoom;
            public Vector2 entrancePosition;

            public PlayerEntrancePosition(RoomName curr, RoomName next, Vector2 pos) {
                currentRoom = curr;
                nextRoom = next;
                entrancePosition = pos;
            }
        }

        public RoomName initialRoom;
        public List<PlayerEntrancePosition> playerEntrancePositions = 
            new List<PlayerEntrancePosition>();

        [SerializeField]
        private List<PlayerEntrancePosition> pep = 
            new List<PlayerEntrancePosition>();

        [SerializeField]
        private GameObject loadingScreen;

        private RoomName _currentRoom;

        void OnEnable() {
            SceneManager.sceneLoaded += TurnOffLoadingScreen;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ParsePlayerPositionData();
            loadingScreen.SetActive(false);
            _currentRoom = RoomName.NONE;
            DoRoomTransition(initialRoom);
        }

        private void ParsePlayerPositionData() {
            foreach(Transform childTransform in gameObject.transform) {
                string[] splitName = childTransform.gameObject.name.Split('-');
                RoomName tmpCurr = RoomName.NONE;
                RoomName tmpNext = RoomName.NONE;
                Vector2 pos = Vector2.zero;

                if (splitName[0] == "Spawn") {
                    tmpCurr = RoomName.SPAWN;
                } else if (splitName[0] == "Bar") {
                    tmpCurr = RoomName.BAR;
                } else if (splitName[0] == "Temple") {
                    tmpCurr = RoomName.TEMPLE;
                } else if (splitName[0] == "Palace") {
                    tmpCurr = RoomName.PALACE;
                } else if (splitName[0] == "None") {
                    tmpCurr = RoomName.NONE;
                }
            
                if (splitName[1] == "Spawn") {
                    tmpNext = RoomName.SPAWN;
                } else if (splitName[1] == "Bar") {
                    tmpNext = RoomName.BAR;
                } else if (splitName[1] == "Temple") {
                    tmpNext = RoomName.TEMPLE;
                } else if (splitName[1] == "Palace") {
                    tmpNext = RoomName.PALACE;
                } else if (splitName[1] == "None") {
                    tmpNext = RoomName.NONE;
                }
                
                pos = new Vector2(
                        childTransform.position.x,
                        childTransform.position.y);
                pep.Add(new PlayerEntrancePosition(tmpCurr, tmpNext, pos));
            }
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
            EvtSystem.EventDispatcher.Raise<ResetCameraPositionToPlayers>( new
                    ResetCameraPositionToPlayers {});
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
            foreach (PlayerEntrancePosition data in pep) {
                if (_currentRoom == data.currentRoom && nextRoom == data.nextRoom) {
                    return data.entrancePosition;
                }
            }

            return Vector2.zero;
        }

        public RoomName GetCurrentRoom() {
            return _currentRoom;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= TurnOffLoadingScreen;
        }

    }
}
