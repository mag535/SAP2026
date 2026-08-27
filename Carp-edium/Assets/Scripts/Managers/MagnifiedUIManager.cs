using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagnifiedUIManager : MonoBehaviour
{
    public GameObject displayWindow;
    public GameObject imageDisplay;
    public GameObject descriptionBox;
    public TextMeshProUGUI descriptionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EvtSystem.EventDispatcher.AddListener<RequestDisplayInspected>(ShowMagnifyWindow);
        EvtSystem.EventDispatcher.AddListener<RequestCloseDisplayInspected>(HideMagnifyWindow);
        
        imageDisplay.SetActive(false);
        descriptionBox.SetActive(false);
        //displayWindow.SetActive(false);
    }
    
    void ShowMagnifyWindow(RequestDisplayInspected evt) {
        if (evt.useLong) {
            descriptionText.text = evt.objectData.longDescription;
        } else {
            descriptionText.text = evt.objectData.description;
        }
        imageDisplay.GetComponent<Image>().sprite = evt.objectData.spriteMagnified;
        imageDisplay.SetActive(true);
        descriptionBox.SetActive(true);
        displayWindow.SetActive(true);
    }

    void HideMagnifyWindow(RequestCloseDisplayInspected evt) {
        displayWindow.SetActive(false);
        descriptionBox.SetActive(false);
        imageDisplay.SetActive(false);
        descriptionText.text = "";
        imageDisplay.GetComponent<Image>().sprite = null;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<RequestDisplayInspected>(ShowMagnifyWindow);
        EvtSystem.EventDispatcher.RemoveListener<RequestCloseDisplayInspected>(HideMagnifyWindow);
    }
}
