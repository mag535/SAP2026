using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class Trader : Interactable
    {
        public Object objectData;
        public List<Listing> possibleTrades = new List<Listing>();

        void Start() {
            if (GameManager.Instance.AmIAModifiedTrader(objectData.objectID)) {
                List<Listing> removedTrades = GameManager.Instance.
                    GetModifiedTraderData(objectData.objectID);
                foreach (Listing removed in removedTrades) {
                    foreach (Listing trade in possibleTrades) {
                        if (trade.cost == removed.cost &&
                                trade.trade == removed.trade) {
                            possibleTrades.Remove(trade);
                            Debug.Log("trade removed");
                            break;
                        }
                    }
                }
            }
        }

        public override void Interact() {
            AudioManager.Instance.Play(soundEffect.name);
            
            // Display out of stock description if no more trades available
            if (possibleTrades.Count == 0) {
                EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                        new RequestDisplayInspected { 
                        useLong = true,
                        objectData = objectData });
                return;
            }

            // Send signal to have description and sprite displayed. This is 
            // magnifying
            EvtSystem.EventDispatcher.Raise<RequestDisplayInspected>(
                    new RequestDisplayInspected { 
                    useLong = false,
                    objectData = objectData });
        }

        public override bool HandleItemUse(Object item) {
            // Attempt trade, set hasAlreadyTraded as result
            return MakeTrade(item);
        }

        public bool MakeTrade(Object offer) {
            foreach (Listing listing in possibleTrades) {
                if (offer.objectID == listing.cost.objectID) {
                    possibleTrades.Remove(listing);
                    // Update GM of modification
                    GameManager.Instance.AddModifiedTrader(objectData.objectID,
                            listing);
                    // Remove offer from inventory
                    EvtSystem.EventDispatcher.Raise<RequestRemoveItem>( 
                            new RequestRemoveItem {
                                item = offer 
                            });
                    // Add trade to inventory
                    EvtSystem.EventDispatcher.Raise<RequestAddItem>( 
                            new RequestAddItem {
                                item = listing.trade 
                            });
                    return true;
                }
            }
            return false;
        }
    }
}
