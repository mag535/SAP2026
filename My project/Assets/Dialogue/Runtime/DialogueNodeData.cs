using System;
using System.Collections.Generic;
using UnityEngine;


public enum DialogueType {
    SIMPLE,
    BRANCH,
    GIVEITEM,
    SETFLAG,
    ENDOFCONVERSATION,
}


[Serializable]
public class DialogueNodeData
{
    public DialogueType type;
    public string Guid;
    public string DialogueText;
    public Object cost;
    public Object trade;
    public string flag;

    public Vector2 Position;
}
