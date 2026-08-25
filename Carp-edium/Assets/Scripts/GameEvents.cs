using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// SCENE & ROOM LOADING --------------------------------------------------------
public class RequestLoadRoom : EvtSystem.Event
{
    public string roomName;
}
public class RequestChangePlayerPosition : EvtSystem.Event
{
    public Vector2 newPosition;
}
public class RequestChangePlayerState : EvtSystem.Event
{
    public string newState;
}

public class TurnOffPlayerControls : EvtSystem.Event {}
public class TurnOnPlayerControls : EvtSystem.Event {}

// UIs -------------------------------------------------------------------------
public class TrackUIMenuOpen : EvtSystem.Event
{
    // false means menu is closing
    public bool isOpening;
}

public class RequestCollectedCheck : EvtSystem.Event
{
    public Object objectData;
}
public class RequestCreateNotification : EvtSystem.Event
{
    public bool isNoteEntry;
    public string objectName;
}

public class TriggerWinScreen : EvtSystem.Event {}
public class TriggerLoseScreen : EvtSystem.Event {}

public class RequestOpenConfirmationScreen : EvtSystem.Event {
    public Carp.EndScreen endingScreenName;
}
public class RequestCloseConfirmationScreen : EvtSystem.Event {}
public class ConfirmGateChoice : EvtSystem.Event {}

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

public class RequestOpenNotebookPre : EvtSystem.Event {}
public class RequestOpenNotebook : EvtSystem.Event 
{
    public List<Object> notes;
}
public class RequestCloseNotebook : EvtSystem.Event {}
public class RequestAddToNotebook : EvtSystem.Event
{
    public Object objectData;
}

public class RequestNextPage : EvtSystem.Event {}
public class RequestPreviousPage : EvtSystem.Event {}
public class SendNextPage : EvtSystem.Event
{
    public List<Object> notes;
}

// PLAYER INTERACT -------------------------------------------------------------
public class RequestItemUse : EvtSystem.Event
{
    public Object item;
}

public class SignalCameraPositionUpdate : EvtSystem.Event {}
public class ResetCameraPositionToPlayers : EvtSystem.Event {}


// INTERACTABLES -----------------------------------------------------------------------
public class OpenChest : EvtSystem.Event
{
    public string key;
}

public class PropagateFlag : EvtSystem.Event
{
    public string flag;
}
