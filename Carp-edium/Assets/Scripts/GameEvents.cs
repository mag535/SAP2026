using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// FIXME: UIs --------------------------------------------------------
public class ToggleDescriptionBox : EvtSystem.Event
{
    public string text;
}

// DESCRIPTION UI --------------------------------------------------------------
public class RequestDisplayInspected : EvtSystem.Event
{
    // true == use long description,
    // false == use regular description
    public bool useLong;
    public Object objectData;
}
public class RequestCloseDisplayInspected : EvtSystem.Event
{
}

// PLAYER INVENTORY & NOTEBOOK -------------------------------------------------
public class RequestOpenInventory : EvtSystem.Event {}
public class RequestCloseInventory : EvtSystem.Event {}
public class RequestAddItem : EvtSystem.Event
{
    public Object item;
}
public class RequestRemoveItem : EvtSystem.Event
{
    public Object item;
}
public class RequestAddToInventoryDisplay : EvtSystem.Event
{
    public Object objectData;
}
public class RequestRemoveFromInventoryDisplay : EvtSystem.Event
{
    public Object objectData;
}

public class RequestOpenNotebook : EvtSystem.Event {}
public class RequestCloseNotebook : EvtSystem.Event {}
public class RequestAddToNotebook : EvtSystem.Event
{
    public Object objectData;
}
public class RequestAddToNotebookDisplay : EvtSystem.Event
{
    public Object objectData;
}

// PLAYER INTERACT -------------------------------------------------------------
public class RequestItemUse : EvtSystem.Event
{
    public Object item;
}

// INTERACTABLES -----------------------------------------------------------------------
public class OpenChest : EvtSystem.Event
{
    public string key;
}
