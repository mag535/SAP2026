using UnityEngine;

namespace Carp {
    public class PlayerState : MonoBehaviour
    {
        public enum PlayerStates {
            GAME,
            DIALOGUE,
            DESCRIPTION,
            ROOMTRANSITION,
        }

        public PlayerStates initialPlayerState;
        public PlayerStates playerState = PlayerStates.GAME;

        void Awake() {
            playerState = initialPlayerState;
        }

        void Start() {
            EvtSystem.EventDispatcher.AddListener<RequestChangePlayerState>(
                    HandleChangePlayerState);
        }

        void HandleChangePlayerState(RequestChangePlayerState evt) {
            if (evt.newState == "GAME") {
                playerState = PlayerStates.GAME;
            }
        }

        public PlayerStates GetCurrentState() {
            return playerState;
        }

        public void ChangeCurrentState(PlayerStates newState) {
            playerState = newState;
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestChangePlayerState>(
                    HandleChangePlayerState);
        }
    }
}
