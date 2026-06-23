using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Response.eNPCs who;
    public TextMeshProUGUI dialogueBox;
    public ResponseManager resMan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResponse(Response.eNPCs who, Response.eQuestions question) {
        Response neededResponse;

        neededResponse = resMan.FindResponse(who, question);

        if (neededResponse == null) {
            Debug.Log("NULL");
            return;
        }

        dialogueBox.text = neededResponse.response;
    }
}
