using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "Custom Game Assets/World Object")]
public class Object : ScriptableObject
{
    // false for regular items
    public bool isNoteEntry;

    public string objectID;
    public string description;
    public Sprite sprite;

    // for Inspectables
    public string longDescription;
    public Sprite spriteMagnified;

    // for anything added to the Notebook
    public Sprite spriteIcon;
}
