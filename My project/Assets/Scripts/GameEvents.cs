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
public class RequestDisplayDescription : EvtSystem.Event
{
    public string longDescription;
}

public class RequestDisplayInspected : EvtSystem.Event
{
    public string longDescription;
    public Sprite spriteMagnified;
}

// PLAYER INVENTORY & NOTEBOOK -------------------------------------------------
public class RequestAddItem : EvtSystem.Event
{
    public Object item;
}
public class RequestRemoveItem : EvtSystem.Event
{
    public Object item;
}

public class RequestAddToNotebook : EvtSystem.Event
{
    public string longDescription;
    public Sprite spriteIcon;
}

public class RequestAddToInventoryDisplay : EvtSystem.Event
{
    public Object objectData;
}
public class RequestRemoveFromInventoryDisplay : EvtSystem.Event
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

public class RequestItemTrade : EvtSystem.Event
{
    public Object offer;
}
