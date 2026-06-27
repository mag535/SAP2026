using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string id;
    public string speaker;
    public string text;
    // END_OF_CONVERSATION for end of dialogue thread
    public string nextDialogueID;
}
