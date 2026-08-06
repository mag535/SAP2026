using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Carp {
    public class PlayerInteract : MonoBehaviour
    {
        private PlayerState playerStateManager;
        private bool interactionsAreEnabled = false;
        private GameObject engagedGO;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestItemUse>(UseItem);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerStateManager = GetComponent<PlayerState>();
            foreach(Transform childTransform in gameObject.transform) {
            }
        }

        // Triggers when the player walks close enough to an interactable game
        // object that it is inside the InteractionEnableRadius.
        public void EnableInteraction(GameObject detectedGO) {
            interactionsAreEnabled = true;
            engagedGO = detectedGO;
            Debug.Log($"Interactions Enabled by [{detectedGO.name}]");
        }

        // Triggers when the player walks far enough away from the engagedGO
        // that it is no longer within the InteractionCancelRadius.
        public void CancelInteraction() {
            if (engagedGO == null) { return; }
            Debug.Log($"Cancelling interactions with [{engagedGO.name}]");

            Cancel();
            interactionsAreEnabled = false;
        }

        private void Cancel() {
            if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DIALOGUE) {
                ConversationManager.Instance.EndConversation();
            }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DESCRIPTION) {
                EvtSystem.EventDispatcher.Raise<RequestCloseDisplayInspected>(
                        new RequestCloseDisplayInspected {});
            }
            playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
            //Debug.Log("State: " + playerStateManager.GetCurrentState());
            engagedGO = null;
        }

        public void Interact(InputAction.CallbackContext context) {
            if (!interactionsAreEnabled) { return; }

            if (context.canceled) {
                if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.GAME) {
                    CallInteraction();
                }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DIALOGUE) {
                    bool success = ConversationManager.Instance.ContinueConversation();
                    if (!success) {
                        playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                        Debug.Log("State: " + playerStateManager.GetCurrentState());
                        engagedGO = null;
                    }
                }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DESCRIPTION) {
                    EvtSystem.EventDispatcher.Raise<RequestCloseDisplayInspected>(
                            new RequestCloseDisplayInspected {});
                    playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                    Debug.Log("State: " + playerStateManager.GetCurrentState());
                    engagedGO = null;
                }
            }
        }

        private void CallInteraction() {
            GameObject tempGO = engagedGO;
            if (tempGO.GetComponent<Interactable>() == null) {
                return;
            }

            
            if (tempGO.GetComponent<Pickup>() != null) {
                engagedGO = null;
                interactionsAreEnabled = false;
            }
            
            // Conversation Starters go to DIALOGUE state
            if (tempGO.GetComponent<ConversationStarter>() != null) {
                playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DIALOGUE);
            // Doors go to ROOMTRANSITION state if unlocked, DESCRIPTION otherwise
            } else if (tempGO.GetComponent<Door>() != null) {
                Door targetDoor = tempGO.GetComponent<Door>();
                if (targetDoor.isLocked) {
                    playerStateManager.ChangeCurrentState(PlayerState
                            .PlayerStates.DESCRIPTION);
                } else {
                    playerStateManager.ChangeCurrentState(PlayerState
                            .PlayerStates.ROOMTRANSITION);
                }
            // Inspectables, Openables, Trader go to DESCRIPTION state
            } else if (tempGO.GetComponent<Inspectable>() != null) {
                playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DESCRIPTION);
            }
            // All others stay in GAME state

            Interactable[] scripts = tempGO.GetComponents<Interactable>();
            foreach (Interactable script in scripts) {
                script.Interact();
            }
            return;
        }

        public void UseItem(RequestItemUse evt) {
            if (engagedGO == null) { return; }
            bool success = engagedGO.GetComponent<Interactable>()
                .HandleItemUse(evt.item);
            // TODO: only remove coin when used on ???? and Buddha statue when
            // used on ????. Nothing else is consumable
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.AddListener<RequestItemUse>(UseItem);
        }
    }
}
