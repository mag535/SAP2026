using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class Chest : Interactable
    {
        public bool isLocked = true;
        public string description;
        public Object key;
        public List<Object> inventory = new List<Object>();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            isLocked = true;
        }

        public override void Interact() {
            if (isLocked) {
                /* FIXME
                EvtSystem.EventDispatcher.Raise<ToggleDescriptionBox>(new ToggleDescriptionBox {
                        text = description });
                */
                return;
            }
            DisplayInventory();
        }

        public override bool HandleItemUse(Object item) {
            if (item.objectID != key.objectID) { return false; }
            Unlock();
            return true;
        }

        void Unlock() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // make transparent
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            sr.color = newColor;
            // lock
            isLocked = false;
        }

        void DisplayInventory() {
            string text = "Chest Inventory:\n";
            foreach (Object item in inventory) {
                text += " - " + item.objectID + "\n";
            }
            Debug.Log(text);
        }
    }
}
