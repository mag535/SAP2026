using UnityEngine;

public class ResponseManager : MonoBehaviour
{
    public Response[] allResponses;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Response FindResponse(Response.eNPCs who, Response.eQuestions question) {
        Response target = null;

        foreach (Response res in allResponses) {
            if (res.npc == who && res.question == question) {
                target = res;
                break;
            }
        }

        return target;
    }

    public Response FindResponseWhat(Response.eNPCs who) {
        foreach (Response res in allResponses) {
            if (res.npc == who && res.question == Response.eQuestions.WHAT) {
                return res;
            }
        }

        return null;
    }
}
