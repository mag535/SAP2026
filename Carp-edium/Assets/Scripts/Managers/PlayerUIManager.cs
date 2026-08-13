using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Carp {
    public class PlayerUIManager : MonoBehaviour
    {
        // For Inventory
        public GameObject inventoryDisplay;
        public GameObject inventoryParent;
        public GameObject itemDisplayPrefab;
        public Sound inventoryOpenSoundEffect;
        public Sound inventoryCloseSoundEffect;

        // For Notebook
        public GameObject notebookDisplay;
        public GameObject notebookParent;
        public GameObject noteEntryPrefab;
        public Sound notebookOpenSoundEffect;
        public Sound notebookCloseSoundEffect;

        private bool notebookState = false;
        private bool inventoryState = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            EvtSystem.EventDispatcher.AddListener<RequestOpenInventory>(HandleOpenInventoryRequest);
            EvtSystem.EventDispatcher.AddListener<RequestAddToInventoryDisplay>(AddToInventoryDisplay);
            EvtSystem.EventDispatcher.AddListener<RequestRemoveFromInventoryDisplay>(RemoveFromInventoryDisplay);
            EvtSystem.EventDispatcher.AddListener<RequestCloseInventory>(HandleCloseInventoryRequest);
            EvtSystem.EventDispatcher.AddListener<RequestOpenNotebook>(HandleOpenNotebookRequest);
            EvtSystem.EventDispatcher.AddListener<RequestCloseNotebook>(HandleCloseNotebookRequest);
            EvtSystem.EventDispatcher.AddListener<SendNextPage>(HandleNextPage);

            notebookState = false;
            inventoryState = false;
        }

        void HandleOpenInventoryRequest(RequestOpenInventory evt) {
            if (inventoryState) { return; }

            EvtSystem.EventDispatcher.Raise<TrackUIMenuOpen>(new TrackUIMenuOpen {
                    isOpening = true });
            inventoryDisplay.SetActive(true);
            AudioManager.Instance.Play(inventoryOpenSoundEffect);
            inventoryState = true;

            // Set focused game object
            foreach (Transform childTransform in inventoryParent.transform) {
                EventSystem.current.SetSelectedGameObject(childTransform.gameObject);
                break;
            }
        }
        void HandleCloseInventoryRequest(RequestCloseInventory evt) {
            EvtSystem.EventDispatcher.Raise<TrackUIMenuOpen>(new TrackUIMenuOpen {
                    isOpening = false });
            inventoryDisplay.SetActive(false);
            AudioManager.Instance.Play(inventoryCloseSoundEffect);
            inventoryState = false;
        }
        void AddToInventoryDisplay(RequestAddToInventoryDisplay evt) {
            GameObject newItem = Instantiate(itemDisplayPrefab, inventoryParent.transform);

            newItem.name = evt.objectData.objectID;
            Item newItemItem = newItem.GetComponent<Item>();
            newItemItem.objectData = evt.objectData;

            foreach (Transform childTransform in newItem.transform) {
                TextMeshProUGUI tmpText = childTransform.GetComponent<TextMeshProUGUI>();
                if (tmpText != null) {
                    tmpText.text = evt.objectData.objectID;
                    continue;
                }
                Image tmpImage = childTransform.GetComponent<Image>();
                if (tmpImage != null) {
                    tmpImage.sprite = evt.objectData.spriteIcon;
                    continue;
                }
            }
        }
        void RemoveFromInventoryDisplay(RequestRemoveFromInventoryDisplay evt) {
            foreach (Transform childTransform in inventoryParent.transform) {
                if (childTransform.gameObject.name == evt.objectData.objectID) {
                    Destroy(childTransform.gameObject);
                    break;
                }
            }
        }

        
        // NOTEBOOK ----------------------------------------------------------------

        void HandleOpenNotebookRequest(RequestOpenNotebook evt) {
            if (notebookState) { return; }

            EvtSystem.EventDispatcher.Raise<TrackUIMenuOpen>(new TrackUIMenuOpen {
                    isOpening = true });
            // Create displays
            foreach (Object note in evt.notes) {
                AddToNotebookDisplay(note);
            }
            notebookDisplay.SetActive(true);
            AudioManager.Instance.Play(notebookOpenSoundEffect);
            notebookState = true;

            // Set focused game object
            foreach (Transform childTransform in notebookParent.transform) {
                EventSystem.current.SetSelectedGameObject(childTransform.gameObject);
                break;
            }
        }

        void HandleCloseNotebookRequest(RequestCloseNotebook evt) {
            EvtSystem.EventDispatcher.Raise<TrackUIMenuOpen>(new TrackUIMenuOpen {
                    isOpening = false });
            notebookDisplay.SetActive(false);
            // Destroy all displays
            foreach (Transform childTransform in notebookParent.transform) {
                Destroy(childTransform.gameObject);
            }
            AudioManager.Instance.Play(notebookCloseSoundEffect);
            notebookState = false;
        }

        void HandleNextPage(SendNextPage evt) {
            // Delete current notes
            foreach (Transform childTransform in notebookParent.transform) {
                Destroy(childTransform.gameObject);
            }
            // Make new ones
            foreach (Object note in evt.notes) {
                AddToNotebookDisplay(note);
            }
        }

        void AddToNotebookDisplay(Object note) {
            GameObject newEntry = Instantiate(noteEntryPrefab, notebookParent.transform);
            newEntry.name = note.objectID;

            Item newEntryItem = newEntry.GetComponent<Item>();
            newEntryItem.objectData = note;

            foreach (Transform childTransform in newEntry.transform) {
                TextMeshProUGUI tmpText = childTransform.GetComponent<TextMeshProUGUI>();
                if (tmpText != null) {
                    tmpText.text = note.longDescription;
                    continue;
                }
                Image tmpImage = childTransform.GetComponent<Image>();
                if (tmpImage != null) {
                    tmpImage.sprite = note.spriteIcon;
                    continue;
                }
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestOpenInventory>(HandleOpenInventoryRequest);
            EvtSystem.EventDispatcher.RemoveListener<RequestAddToInventoryDisplay>(AddToInventoryDisplay);
            EvtSystem.EventDispatcher.RemoveListener<RequestRemoveFromInventoryDisplay>(RemoveFromInventoryDisplay);
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseInventory>(HandleCloseInventoryRequest);
            EvtSystem.EventDispatcher.RemoveListener<RequestOpenNotebook>(HandleOpenNotebookRequest);
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseNotebook>(HandleCloseNotebookRequest);
            EvtSystem.EventDispatcher.RemoveListener<SendNextPage>(HandleNextPage);
        }
    }
}
