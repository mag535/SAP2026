using UnityEngine;

[CreateAssetMenu(fileName = "FollowUp", menuName = "Scriptable Objects/FollowUp")]
public class FollowUp : ScriptableObject
{
    // The highlighted word or phrase in the main response.
    public string name;

    public string text;
}
