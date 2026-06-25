using UnityEngine;
using TMPro;

public class ConvoManager : MonoBehaviour
{
    public GameObject background;
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;

    private bool isScreenOn = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        background.SetActive(false);
        dialogueBox.SetActive(false);
    }

    public bool GetScreenOn() {
        return isScreenOn;
    }

    public void ShowConversation(string text) {
        background.SetActive(true);
        textBox.text = text;
        dialogueBox.SetActive(true);
    }

    public void HideConversation() {
        background.SetActive(false);
        dialogueBox.SetActive(false);
    }
}
