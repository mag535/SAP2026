using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class GameManager : Singleton<GameManager>
    {
        public Dictionary<string, bool> modifiedDoors = 
            new Dictionary<string, bool>();
        public List<string> modifiedPickups = 
            new List<string>();
        public Dictionary<string, List<Listing>> modifiedTraders =
            new Dictionary<string, List<Listing>>();
        public Vector2 modifiedFox = Vector2.zero;
        public bool areGatesUnlocked = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            EvtSystem.EventDispatcher.AddListener<PropagateFlag>(HandleFlag);
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

        public void AddModifiedFox(Vector2 newPostion) {
            modifiedFox = newPostion;
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

        public bool AmIAModifiedFox() {
            if (modifiedFox == Vector2.zero) {
                return false;
            }
            return true;
        }

        public bool GetModifiedDoorData(string id) {
            return modifiedDoors[id];
        }

        // Modified Pickup data just tells the pickup whether or not to destroy
        // itself.

        public List<Listing> GetModifiedTraderData(string id) {
            return modifiedTraders[id];
        }

        public Vector2 GetModifiedFoxData() {
            return modifiedFox;
        }

        public bool GetAreGatesUnlocked() {
            return areGatesUnlocked;
        }

        void HandleFlag(PropagateFlag evt) {
            if (evt.flag == "OpenTempleDoor") {
                AddModifiedDoor("TempleDoor", false);
                AddModifiedFox(new Vector2(-4, 16));
            } else if (evt.flag == "UnlockGates") {
                areGatesUnlocked = true;
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<PropagateFlag>(HandleFlag);
        }
    }
}
