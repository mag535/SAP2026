using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// DESCRIPTION BOX UI --------------------------------------------------------
public class ToggleDescriptionBox : EvtSystem.Event
{
    public string text;
}

// PLAYER INVENTORY ------------------------------------------------------------
public class RequestAddItem : EvtSystem.Event
{
    public Object item;
}
public class RequestRemoveItem : EvtSystem.Event
{
    public Object item;
}

// PLAYER INTERACT -------------------------------------------------------------
public class RequestItemUse : EvtSystem.Event
{
    public Object item;
}

// ITEMS -----------------------------------------------------------------------
public class OpenChest : EvtSystem.Event
{
    public string key;
}
