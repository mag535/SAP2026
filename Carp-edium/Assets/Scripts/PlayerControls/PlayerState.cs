using UnityEngine;
using UnityEngine.InputSystem;

namespace Carp {
    public class PlayerState : MonoBehaviour
    {
        public PlayerStates initialPlayerState;
        public PlayerStates playerState = PlayerStates.GAME;

        private PlayerInput playerInput;

        void Awake() {
            playerState = initialPlayerState;
            playerInput = GetComponent<PlayerInput>();
            EvtSystem.EventDispatcher.AddListener<RequestSetPlayerState>(
                    SetPlayerState);
            EvtSystem.EventDispatcher.AddListener<TurnOffPlayerControls>(
                    TurnControlsOff);
            EvtSystem.EventDispatcher.AddListener<TurnOnPlayerControls>(
                    TurnControlsOn);
        }

        void Start() {
        }

        void SetPlayerState(RequestSetPlayerState evt) {
            playerState = evt.state;
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
            EvtSystem.EventDispatcher.RemoveListener<RequestSetPlayerState>(
                    SetPlayerState);
            EvtSystem.EventDispatcher.RemoveListener<TurnOffPlayerControls>(
                    TurnControlsOff);
            EvtSystem.EventDispatcher.RemoveListener<TurnOnPlayerControls>(
                    TurnControlsOn);
        }
    }
}
