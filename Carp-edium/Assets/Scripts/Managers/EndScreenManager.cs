using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public GameObject endScreenParent;
    public GameObject winScreenPrefab;
    public GameObject loseScreenPrefab;

    void Awake() {
         EvtSystem.EventDispatcher.AddListener<TriggerWinScreen>(ShowWinScreen);
         EvtSystem.EventDispatcher.AddListener<TriggerLoseScreen>(ShowLoseScreen);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endScreenParent.SetActive(false);
    }

    void ShowWinScreen(TriggerWinScreen evt) {
        GameObject screen = Instantiate(winScreenPrefab, endScreenParent.transform);
        endScreenParent.SetActive(true);
    }

    void ShowLoseScreen(TriggerLoseScreen evt) {
        GameObject screen = Instantiate(loseScreenPrefab, endScreenParent.transform);
        endScreenParent.SetActive(true);
    }

    void OnDestroy() {
         EvtSystem.EventDispatcher.RemoveListener<TriggerWinScreen>(ShowWinScreen);
         EvtSystem.EventDispatcher.RemoveListener<TriggerLoseScreen>(ShowLoseScreen);
    }
}
