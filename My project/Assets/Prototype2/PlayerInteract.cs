using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    public float collisionOffset = 0.7f;
    public ContactFilter2D interactionFilters;

    public ConvoManager convoManager;
    public ClueManager clueManager;

    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private PlayerState playerStateManager;

    void Awake() {
        playerStateManager = GetComponent<PlayerState>();
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
                convoManager.HideConversation();
                playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                Debug.Log("State: " + playerStateManager.GetCurrentState());
                clueManager.PopulateNotebook();
            }else if (playerStateManager.GetCurrentState() == PlayerState.PlayerStates.DESCRIPTION) {
                // close description
                convoManager.HideConversation();
                playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.GAME);
                Debug.Log("State: " + playerStateManager.GetCurrentState());
                clueManager.PopulateNotebook();
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

            if (count != 0) {
                foreach (RaycastHit2D hit in castCollisions) {
                    if (hit.transform.gameObject.GetComponent<Clue>() != null) {
                        string tmp = hit.transform.gameObject.GetComponent<Clue>().text;
                        clueManager.SetPendingClue(tmp);
                    }
                    if (hit.transform.gameObject.GetComponent<Interactable>() != null) {
                        // start conversation
                        convoManager.ShowConversation(hit.transform.gameObject.GetComponent<Interactable>().text);
                        playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DIALOGUE);
                        Debug.Log("State: " + playerStateManager.GetCurrentState());
                        return;
                    } else if (hit.transform.gameObject.GetComponent<Examinable>() != null) {
                        // show description
                        convoManager.ShowConversation(hit.transform.gameObject.GetComponent<Examinable>().description);
                        playerStateManager.ChangeCurrentState(PlayerState.PlayerStates.DESCRIPTION);
                        Debug.Log("State: " + playerStateManager.GetCurrentState());
                        return;
                    }
                }
            }
        }
    }
}
