using UnityEngine;

public class ConvoStarter : Interactable
{
    public Dialogue dialogueStart; // TODO: should this be a Dialogue instead?
    public Dialogue dialogueResponse;
    public Dialogue dialogueDefaultNo;

    public override void Interact() {
        // sfx
        // start dialogue, send dialogue id to dialogue manager
        DialogueManager2.Instance.StartDialogueThread(dialogueStart);
    }

    public override void HandleItemUse(Object item) {
        Trader npcTrader = GetComponent<Trader>();
        bool success = npcTrader.MakeTrade(item);
        if (success) {
            DialogueManager2.Instance.StartDialogueThread(dialogueResponse);
        } else {
            DialogueManager2.Instance.StartDialogueThread(dialogueDefaultNo);
        }
    }
}
