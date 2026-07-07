using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;


// Simple
public class DialogueNode : Node
{
    // only for entry point node
    public bool EntryPoint = false;

    // all / SIMPLE
    public DialogueType type;
    public string GUID;
    public string DialogueText;

    // GIVEITEM
    public Object cost;
    public Object trade;

    // SETFLAG
    public string flag;
}
