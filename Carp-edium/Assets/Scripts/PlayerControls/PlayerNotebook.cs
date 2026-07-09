using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class PlayerNotebook : MonoBehaviour
    {

        // Only `spriteIcon` and `longDescription` will be relevant for a note
        // entry.
        private List<Object> noteEntries = new List<Object>();

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestAddToNotebook>(AddNote);
        }

        void AddNote(RequestAddToNotebook evt) {
            foreach (Object note in noteEntries) {
                if (note.objectID == evt.objectData.objectID) {
                    Debug.Log("Note [" + note.objectID + 
                            "] is already in the notebook.");
                    return;
                }
            }

            noteEntries.Add(evt.objectData);
            // Send signal to notebook UI manager to create new note display
            EvtSystem.EventDispatcher.Raise<RequestAddToNotebookDisplay>(
                    new RequestAddToNotebookDisplay { objectData = evt.objectData });
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestAddToNotebook>(AddNote);
        }
    }
}
