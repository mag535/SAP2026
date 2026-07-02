using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ResponseV2", menuName = "Scriptable Objects/ResponseV2")]
public class ResponseV2 : ScriptableObject
{
    // Can only be WHAT, WHERE, WHEN.
    public string question;

    // The main response to the question.
    public string response;

    // Optional. Follow-ups to get more information.
    // This is just the names of the follow-ups. Preferrably make this whatever
    // word or phrase would be highlighted in the main response text.
    public List<FollowUp> follow_ups;
}
