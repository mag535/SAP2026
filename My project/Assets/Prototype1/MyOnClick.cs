using UnityEngine;
using UnityEngine.UI;

public class MyOnClick : MonoBehaviour
{
    public Response.eQuestions question;
    public Response.eNPCs who;
    public DialogueManager dialogueManager;

    private Button thisButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(ShowResponse); 
    }

    void ShowResponse() {
        dialogueManager.ShowResponse(who, question);
    }
}
