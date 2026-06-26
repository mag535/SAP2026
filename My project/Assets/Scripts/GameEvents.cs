using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ExampleTrigger : EvtSystem.Event
{
    public string data1;
    public float data2;
}

// CONVERSATION MANAGER --------------------------------------------------------
public class ToggleDialogueWindow : EvtSystem.Event
{
    public string text;
}

// DESCRIPTION MANAGER --------------------------------------------------------
public class ShowDescriptionWindow : EvtSystem.Event
{
}
public class HideDescriptionWindow : EvtSystem.Event
{
}

// CLUE MANAGER ----------------------------------------------------------------
public class NewCurrentNPC : EvtSystem.Event
{
    // true == set
    // false == clear
    public bool set;
    public string npcName;
}
public class SendItemName : EvtSystem.Event
{
    public string itemName;
}

// PLAYER INVENTORY ------------------------------------------------------------
public class RequestRemoveItem : EvtSystem.Event
{
    public string itemName;
}
