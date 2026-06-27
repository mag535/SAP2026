using UnityEngine;
using System.Collections.Generic;

public class Trader : MonoBehaviour
{
    [System.Serializable]
    public struct Listing {
        public Object cost;
        public Object trade;
    }
    public List<Listing> possibleTrades = new List<Listing>();

    public bool MakeTrade(Object offer) {
        foreach (Listing listing in possibleTrades) {
            if (offer.objectID == listing.cost.objectID) {
                Debug.Log("traded [" + offer.objectID + "] for [" + listing.trade.objectID + "]");
                possibleTrades.Remove(listing);
                EvtSystem.EventDispatcher.Raise<RequestRemoveItem>( new RequestRemoveItem {
                        item = offer });
                EvtSystem.EventDispatcher.Raise<RequestAddItem>( new RequestAddItem {
                        item = listing.trade });
                return true;
            }
        }
        Debug.Log("I don't want anything you have.");
        return false;
    }
}
