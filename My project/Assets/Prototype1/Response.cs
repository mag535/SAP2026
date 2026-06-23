using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Response", menuName = "Scriptable Objects/Response")]
[System.Serializable]
public class Response : ScriptableObject
{
    public enum eQuestions {
        WHAT,
        WHERE,
        WHEN
    };
    public enum eNPCs {
        TIMMY,
        JIMMY,
        DAD,
        MOM
    };
    
    // Who is saying the response
    public eNPCs npc;

     // Can only be WHAT, WHERE, WHEN.
    public eQuestions question;

    // The main response to the question.
    public string response;
}
