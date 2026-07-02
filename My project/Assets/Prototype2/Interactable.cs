using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string soundEffect = "NONE";

    public abstract void Interact();
    public abstract void HandleItemUse(Object item);
}
