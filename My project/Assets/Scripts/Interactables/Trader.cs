using UnityEngine;
using System.Collections.Generic;

public class Trader : MonoBehaviour
{
    [System.Serializable]
    public struct Listing {
        public Object cost;
        public Object trade;
    }

    public bool hasAlreadyTraded = false;
    public Object objectData;
    public List<Listing> possibleTrades = new List<Listing>();

    void Start() {
        EvtSystem.EventDispatcher.AddListener<RequestItemTrade>(HandleTrade);
    }

    public void HandleTrade(RequestItemTrade evt) {
        if (hasAlreadyTraded) {
            // Displays OUT_OF_STOCK description
            EvtSystem.EventDispatcher.Raise<RequestDisplayDescription>(
                    new RequestDisplayDescription {
                        longDescription = objectData.longDescription
                    });
            Debug.Log("OUT OF STOCK");
            return;
        }

        // Attempt trade, set hasAlreadyTraded as result
        hasAlreadyTraded = MakeTrade(evt.offer);
    }

    public bool MakeTrade(Object offer) {
        foreach (Listing listing in possibleTrades) {
            if (offer.objectID == listing.cost.objectID) {
                Debug.Log("traded [" + offer.objectID + "] for [" + 
                        listing.trade.objectID + "]");
                possibleTrades.Remove(listing);
                EvtSystem.EventDispatcher.Raise<RequestRemoveItem>( 
                        new RequestRemoveItem {
                            item = offer 
                        });
                EvtSystem.EventDispatcher.Raise<RequestAddItem>( 
                        new RequestAddItem {
                            item = listing.trade 
                        });
                return true;
            }
        }
        Debug.Log("I don't want anything you have.");
        return false;
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<RequestItemTrade>(HandleTrade);
    }
}
