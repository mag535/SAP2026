using UnityEngine;
using TMPro;

public class DescriptionBoxUI : MonoBehaviour
{
    public GameObject background;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;

    private bool isOn;

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<ToggleDescriptionBox>(HandleToggleDescriptionBox);
    }

    void Start() {
        HideWindow();
    }

    public void HandleToggleDescriptionBox(ToggleDescriptionBox evt) {
        if (isOn) {
            HideWindow();
            return;
        } 
        ShowWindow(evt.text);
    }

    void ShowWindow(string text) {
        textBox.text = text;
        background.SetActive(true);
        dialogueBox.SetActive(true);
        isOn = true;
    }

    void HideWindow() {
        background.SetActive(false);
        dialogueBox.SetActive(false);
        isOn = false;
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<ToggleDescriptionBox>(HandleToggleDescriptionBox);
    }
}
