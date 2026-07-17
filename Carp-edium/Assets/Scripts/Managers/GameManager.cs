using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class GameManager : Singleton<GameManager>
    {
        public Dictionary<string, bool> modifiedDoors = 
            new Dictionary<string, bool>();
        public List<string> modifiedPickups = new List<string>();
        public Dictionary<string, List<Listing>> modifiedTraders =
            new Dictionary<string, List<Listing>>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        public void AddModifiedDoor(string id, bool isLocked) {
            modifiedDoors[id] = isLocked;
            return;
        }

        public void AddModifiedPickup(string id) {
            foreach (string existingID in modifiedPickups) {
                if (id == existingID) {
                    return;
                }
            }

            modifiedPickups.Add(id);
            return;
        }

        public void AddModifiedTrader(string id, Listing removed) {
            if (modifiedTraders.ContainsKey(id)) {
                // append removed to existing list
                List<Listing> oldList = modifiedTraders[id];
                oldList.Add(removed);
                modifiedTraders[id] = oldList;
                return;
            }

            List<Listing> newList = new List<Listing>();
            newList.Add(removed);
            modifiedTraders[id] = newList;
            return;
        }

        public bool AmIAModifiedDoor(string id) {
            return modifiedDoors.ContainsKey(id);
        }

        public bool AmIAModifiedPickup(string id) {
            foreach (string existingID in modifiedPickups) {
                if (existingID == id) {
                    return true;
                }
            }

            return false;
        }

        public bool AmIAModifiedTrader(string id) {
            return modifiedTraders.ContainsKey(id);
        }

        public bool GetModifiedDoorData(string id) {
            return modifiedDoors[id];
        }

        // Modified Pickup data just tells the pickup whether or not to destroy
        // itself.

        public List<Listing> GetModifiedTraderData(string id) {
            return modifiedTraders[id];
        }
    }
}
