using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Carp {
    public class PlayerInteract : MonoBehaviour
    {
        public float collisionOffset = 0.7f;
        public ContactFilter2D interactionFilters;

        private Rigidbody2D rb;
        private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        private PlayerState playerStateManager;
        private PlayerInventory playerInventory;

        private GameObject engagedGO;

        void Awake() {
            playerStateManager = GetComponent<PlayerState>();
            playerInventory = GetComponent<PlayerInventory>();
            EvtSystem.EventDispatcher.AddListener<RequestItemUse>(UseItem);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Interact(InputAction.CallbackContext context) {
            if (context.canceled) {
                if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.GAME) {
                    CallInteraction();
                }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DIALOGUE) {
                    bool success = ConversationManager.Instance.ContinueConversation();
                    if (!success) {
                        //ClueManager.Instance.ClearCurrentNPC();
                        playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                        Debug.Log("State: " + playerStateManager.GetCurrentState());
                        engagedGO = null;
                    }
                }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DESCRIPTION) {
                    // TODO: close description window
                    EvtSystem.EventDispatcher.Raise<ToggleDescriptionBox>(new ToggleDescriptionBox {
                            text = "" });
                    playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                    Debug.Log("State: " + playerStateManager.GetCurrentState());
                    engagedGO = null;
                }
            }
        }

        private void CallInteraction() {
            List<Vector2> directions = new List<Vector2> {
                new Vector2(0,1),
                new Vector2(1,1),
                new Vector2(1,0),
                new Vector2(1,-1),
                new Vector2(0,-1),
                new Vector2(-1,-1),
                new Vector2(-1,0),
                new Vector2(-1,1),
            };

            foreach (Vector2 direction in directions) {
                int count = rb.Cast(
                        direction,
                        interactionFilters,
                        castCollisions,
                        collisionOffset);

                // no collisions, skip to checking next ray
                if (count == 0) { continue; }

                foreach (RaycastHit2D hit in castCollisions) {
                    if (hit.transform.gameObject.GetComponent<Interactable>() == null) {
                        continue;
                    }
                    if (hit.transform.gameObject.GetComponent<Pickup>() == null) {
                        engagedGO = hit.transform.gameObject;
                    }
                    
                    // Conversation Starters go to DIALOGUE state
                    if (hit.transform.gameObject.GetComponent<ConversationStarter>() != null) {
                        playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DIALOGUE);
                    // Inspectables go to DESCRIPTION state
                    } else if (hit.transform.gameObject.GetComponent<Inspectable>() != null) {
                        playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DESCRIPTION);
                    }
                    // All others stay in GAME state

                    Debug.Log("State: " + playerStateManager.GetCurrentState());
                    hit.transform.gameObject.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }

        public void UseItem(RequestItemUse evt) {
            if (engagedGO == null) { return; }
            engagedGO.GetComponent<Interactable>().HandleItemUse(evt.item);
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.AddListener<RequestItemUse>(UseItem);
        }
    }
}
