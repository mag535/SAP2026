using System;
using UnityEngine;


public enum DialogueType {
    SIMPLE,
    BRANCH,
    GIVEITEM,
    SETFLAG,
}


[Serializable]
public class DialogueNodeData
{
    public DialogueType type;
    public string Guid;
    public string DialogueText;
    // TODO: add choices parameter
    public Object cost;
    public Object trade;
    public string flag;

    public Vector2 Position;
}
