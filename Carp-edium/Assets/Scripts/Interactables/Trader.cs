using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class Trader : Interactable
    {
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
