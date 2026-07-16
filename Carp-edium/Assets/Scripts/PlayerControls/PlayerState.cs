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
            EvtSystem.EventDispatcher
                .AddListener<RequestChangePlayerState>(HandlePlayerStateChange);
        }

        public PlayerStates GetCurrentState() {
            return playerState;
        }

        public void ChangeCurrentState(PlayerStates newState) {
            playerState = newState;
        }

        void HandlePlayerStateChange(RequestChangePlayerState evt) {
            if (evt.newState == "GAME") {
                playerState = PlayerStates.GAME;
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher
                .RemoveListener<RequestChangePlayerState>(HandlePlayerStateChange);
        }
    }
}
