using UnityEngine;
using UnityEngine.InputSystem;

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

        private PlayerInput playerInput;

        void Awake() {
            playerState = initialPlayerState;
            playerInput = GetComponent<PlayerInput>();
            EvtSystem.EventDispatcher.AddListener<RequestChangePlayerState>(
                    HandleChangePlayerState);
            EvtSystem.EventDispatcher.AddListener<TurnOffPlayerControls>(
                    TurnControlsOff);
            EvtSystem.EventDispatcher.AddListener<TurnOnPlayerControls>(
                    TurnControlsOn);
        }

        void Start() {
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

        void TurnControlsOff(TurnOffPlayerControls evt) {
            playerInput.SwitchCurrentActionMap("Loading");
        }
        void TurnControlsOn(TurnOnPlayerControls evt) {
            playerInput.SwitchCurrentActionMap("Game");
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestChangePlayerState>(
                    HandleChangePlayerState);
            EvtSystem.EventDispatcher.RemoveListener<TurnOffPlayerControls>(
                    TurnControlsOff);
            EvtSystem.EventDispatcher.RemoveListener<TurnOnPlayerControls>(
                    TurnControlsOn);
        }
    }
}
