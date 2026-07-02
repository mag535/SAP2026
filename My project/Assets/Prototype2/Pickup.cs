using UnityEngine;

public class Pickup : Interactable
{
    public static float destroyDelay = 0.1f;

    public Object objectData;

    public override void Interact() {
        // sfx
        // display description
        EvtSystem.EventDispatcher.Raise<ToggleDescriptionBox>(new ToggleDescriptionBox {
                text = objectData.description });
        // add to inventory
        EvtSystem.EventDispatcher.Raise<RequestAddItem>(new RequestAddItem {
                item = objectData });
        // destroy
        Destroy(gameObject, destroyDelay);
    }

    public override void HandleItemUse(Object item) {}
}
