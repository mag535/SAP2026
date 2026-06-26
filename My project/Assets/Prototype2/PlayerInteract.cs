using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    public float collisionOffset = 0.7f;
    public ContactFilter2D interactionFilters;

    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private PlayerState playerStateManager;
    private PlayerInventory playerInventory;

    void Awake() {
        playerStateManager = GetComponent<PlayerState>();
        playerInventory = GetComponent<PlayerInventory>();
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
                // close dialogue
                EvtSystem.EventDispatcher.Raise<ToggleDialogueWindow>(
                        new ToggleDialogueWindow {text=""});
                playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                Debug.Log("State: " + playerStateManager.GetCurrentState());
                playerInventory.PopulateNotebook();
                //clueManager.ClearCurrentNPC();
                EvtSystem.EventDispatcher.Raise<NewCurrentNPC>(
                        new NewCurrentNPC { set = false, npcName = "" });
            }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DESCRIPTION) {
                // close description
                EvtSystem.EventDispatcher.Raise<ToggleDialogueWindow>(
                        new ToggleDialogueWindow {text=""});
                playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                Debug.Log("State: " + playerStateManager.GetCurrentState());
                playerInventory.PopulateNotebook();
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
                if (hit.transform.gameObject.GetComponent<Clue>() != null) {
                    Clue hitClue = hit.transform.gameObject.GetComponent<Clue>();
                    if (!hitClue.wasFound) {
                        string tmp = hitClue.text;
                        hitClue.wasFound = true;
                        playerInventory.SetPendingClue(tmp);
                    }
                }
                if (hit.transform.gameObject.GetComponent<Interactable>() != null) {
                    Interactable hitInteractable = hit.transform.gameObject.GetComponent<Interactable>();
                    // start conversation
                    EvtSystem.EventDispatcher.Raise<ToggleDialogueWindow>(
                            new ToggleDialogueWindow {text = hitInteractable.text});
                    //clueManager.SetCurrentNPC(hitInteractable.speaker);
                    EvtSystem.EventDispatcher.Raise<NewCurrentNPC>(
                            new NewCurrentNPC { set = true, npcName = hitInteractable.speaker });
                    playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DIALOGUE);
                    Debug.Log("State: " + playerStateManager.GetCurrentState());
                    return;
                } else if (hit.transform.gameObject.GetComponent<Examinable>() != null) {
                    Examinable hitExaminable = hit.transform.gameObject.GetComponent<Examinable>();
                    // show description
                    EvtSystem.EventDispatcher.Raise<ToggleDialogueWindow>(
                            new ToggleDialogueWindow {text = hitExaminable.description});
                    playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DESCRIPTION);
                    Debug.Log("State: " + playerStateManager.GetCurrentState());
                    return;
                }
            }
        }
    }

    public void UseItem(string itemName) {
        EvtSystem.EventDispatcher.Raise<SendItemName>(
                new SendItemName { itemName = itemName });
    }
}
