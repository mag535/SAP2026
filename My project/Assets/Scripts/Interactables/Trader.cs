using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class Trader : Interactable
    {
        [System.Serializable]
        public struct Listing {
            public Object cost;
            public Object trade;
        }

        public bool hasAlreadyTraded = false;
        public Object objectData;
        public List<Listing> possibleTrades = new List<Listing>();

        public override void Interact() {
            AudioManager.Instance.Play(soundEffect.name);
            
            // Display out of stock description if no more trades available
            if (possibleTrades.Count == 0) {
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { 
                        useLong = true,
                        objectData = objectData });
                Debug.Log("OUT OF STOCK");
                return;
            }

            // Send signal to have description and sprite displayed. This is 
            // magnifying
            EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                    new RequestDisplayInspected { 
                    useLong = false,
                    objectData = objectData });
        }

        public override void HandleItemUse(Object item) {
            // Attempt trade, set hasAlreadyTraded as result
            hasAlreadyTraded = MakeTrade(item);
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
    }
}
