using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    // For Inventory
    public GameObject inventoryDisplay;
    public GameObject inventoryParent;
    public GameObject itemDisplayPrefab;

    // For Notebook
    public GameObject notebookDisplay;
    public GameObject notebookParent;
    public GameObject noteEntryPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EvtSystem.EventDispatcher.AddListener<RequestOpenInventory>(HandleOpenInventoryRequest);
        EvtSystem.EventDispatcher.AddListener<RequestAddToInventoryDisplay>(AddToInventoryDisplay);
        EvtSystem.EventDispatcher.AddListener<RequestCloseInventory>(HandleCloseInventoryRequest);
        EvtSystem.EventDispatcher.AddListener<RequestOpenNotebook>(HandleOpenNotebookRequest);
        EvtSystem.EventDispatcher.AddListener<RequestAddToNotebookDisplay>(AddToNotebookDisplay);
        EvtSystem.EventDispatcher.AddListener<RequestCloseNotebook>(HandleCloseNotebookRequest);
    }

    void HandleOpenInventoryRequest(RequestOpenInventory evt) {
        inventoryDisplay.SetActive(true);
    }
    void HandleCloseInventoryRequest(RequestCloseInventory evt) {
        inventoryDisplay.SetActive(false);
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

    // Notebook
    void HandleOpenNotebookRequest(RequestOpenNotebook evt) {
        notebookDisplay.SetActive(true);
    }
    void HandleCloseNotebookRequest(RequestCloseNotebook evt) {
        notebookDisplay.SetActive(false);
    }
    void AddToNotebookDisplay(RequestAddToNotebookDisplay evt) {
        GameObject newEntry = Instantiate(noteEntryPrefab, notebookParent.transform);
        newEntry.name = evt.objectData.objectID;
        foreach (Transform childTransform in newEntry.transform) {
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

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<RequestOpenInventory>(HandleOpenInventoryRequest);
        EvtSystem.EventDispatcher.RemoveListener<RequestAddToInventoryDisplay>(AddToInventoryDisplay);
        EvtSystem.EventDispatcher.RemoveListener<RequestCloseInventory>(HandleCloseInventoryRequest);
        EvtSystem.EventDispatcher.RemoveListener<RequestOpenNotebook>(HandleOpenNotebookRequest);
        EvtSystem.EventDispatcher.RemoveListener<RequestAddToNotebookDisplay>(AddToNotebookDisplay);
        EvtSystem.EventDispatcher.RemoveListener<RequestCloseNotebook>(HandleCloseNotebookRequest);
    }
}
