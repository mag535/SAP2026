using UnityEngine;
using System.Collections.Generic;

namespace Carp {
    public class PlayerNotebook : MonoBehaviour
    {

        // Only `spriteIcon` and `longDescription` will be relevant for a note
        // entry.
        public List<Object> noteEntries = new List<Object>();
        public int maxNotesPerPage = 10;
        public int currentNotePosition = 0;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestAddToNotebook>(AddNote);
            EvtSystem.EventDispatcher.AddListener<RequestPreviousPage>(HandlePreviousPageRequest);
            EvtSystem.EventDispatcher.AddListener<RequestNextPage>(HandleNextPageRequest);
            EvtSystem.EventDispatcher.AddListener<RequestOpenNotebookPre>(HandleOpenNotebookPreRequest);
            EvtSystem.EventDispatcher.AddListener<RequestCloseNotebook>(HandleCloseNotebookRequest);
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
        }

        void HandleOpenNotebookPreRequest(RequestOpenNotebookPre evt) {
            int noteCount = maxNotesPerPage;
            if (currentNotePosition + noteCount > noteEntries.Count) {
                noteCount = noteEntries.Count - currentNotePosition;
            }
            EvtSystem.EventDispatcher.Raise<RequestOpenNotebook>(new
                    RequestOpenNotebook {
                    notes = noteEntries.GetRange(currentNotePosition, noteCount) });
        }

        void HandlePreviousPageRequest(RequestPreviousPage evt) {
            if (currentNotePosition - maxNotesPerPage < 0) {
                // TODO: have some feedback for EOF
                return;
            }

            currentNotePosition -= maxNotesPerPage;
            int noteCount = maxNotesPerPage;
            if (noteEntries.Count < currentNotePosition + maxNotesPerPage) {
                noteCount = noteEntries.Count - currentNotePosition;
            }

            List<Object> notesForPage = new List<Object>();
            for (int i = currentNotePosition; i < currentNotePosition + noteCount; i++) {
                notesForPage.Add(noteEntries[i]);
            }
            EvtSystem.EventDispatcher.Raise<SendNextPage>(new SendNextPage {
                    notes = notesForPage });
        }

        void HandleNextPageRequest(RequestNextPage evt) {
            if (currentNotePosition + maxNotesPerPage > noteEntries.Count) {
                // TODO: have some feedback for EOF
                return;
            }

            currentNotePosition += maxNotesPerPage;
            int noteCount = maxNotesPerPage;
            if (noteEntries.Count < currentNotePosition + maxNotesPerPage) {
                noteCount = noteEntries.Count - currentNotePosition;
            }

            List<Object> notesForPage = new List<Object>();
            for (int i = currentNotePosition; i < currentNotePosition + noteCount; i++) {
                notesForPage.Add(noteEntries[i]);
            }
            EvtSystem.EventDispatcher.Raise<SendNextPage>(new SendNextPage {
                    notes = notesForPage });
        }

        void HandleCloseNotebookRequest(RequestCloseNotebook evt) {
            currentNotePosition = 0;
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestAddToNotebook>(AddNote);
            EvtSystem.EventDispatcher.RemoveListener<RequestPreviousPage>(HandlePreviousPageRequest);
            EvtSystem.EventDispatcher.RemoveListener<RequestNextPage>(HandleNextPageRequest);
            EvtSystem.EventDispatcher.RemoveListener<RequestOpenNotebookPre>(HandleOpenNotebookPreRequest);
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseNotebook>(HandleCloseNotebookRequest);
        }
    }
}
