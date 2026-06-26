using UnityEngine;
using TMPro;

public class ConversationManager : Singleton<ConversationManager>
{
    public GameObject background;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;

    private bool isScreenOn = false;

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<ToggleDialogueWindow>(ToggleWindow);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        background.SetActive(false);
        dialogueBox.SetActive(false);
        isScreenOn = false;
    }

    public void ToggleWindow(ToggleDialogueWindow evt) {
        if (isScreenOn) {
            HideWindow();
            isScreenOn = false;
        } else {
            ShowWindow(evt.text);
            isScreenOn = true;
        }
    }

    public void ShowWindow(string text) {
        background.SetActive(true);
        textBox.text = text;
        dialogueBox.SetActive(true);
    }

    public void HideWindow() {
        background.SetActive(false);
        dialogueBox.SetActive(false);
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<ToggleDialogueWindow>(ToggleWindow);
    }
}
