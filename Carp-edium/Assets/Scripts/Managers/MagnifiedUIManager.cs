using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagnifiedUIManager : MonoBehaviour
{
    public GameObject descriptionDisplay;
    public Image imageDisplay;
    public TextMeshProUGUI descriptionTextBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EvtSystem.EventDispatcher.AddListener<RequestDisplayInspected>(ShowMagnifyWindow);
        EvtSystem.EventDispatcher.AddListener<RequestCloseDisplayInspected>(HideMagnifyWindow);
    }
    
    void ShowMagnifyWindow(RequestDisplayInspected evt) {
        if (evt.useLong) {
            descriptionTextBox.text = evt.objectData.longDescription;
        } else {
            descriptionTextBox.text = evt.objectData.description;
        }
        imageDisplay.sprite = evt.objectData.spriteMagnified;
        descriptionDisplay.SetActive(true);
    }

    void HideMagnifyWindow(RequestCloseDisplayInspected evt) {
        descriptionDisplay.SetActive(false);
        descriptionTextBox.text = "";
        imageDisplay.sprite = null;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<RequestDisplayInspected>(ShowMagnifyWindow);
        EvtSystem.EventDispatcher.RemoveListener<RequestCloseDisplayInspected>(HideMagnifyWindow);
    }
}
