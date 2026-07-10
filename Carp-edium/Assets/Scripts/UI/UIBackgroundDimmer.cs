using UnityEngine;

public class UIBackgroundDimmer : MonoBehaviour
{
    public GameObject background;

    private int uiMenusOpen = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiMenusOpen = 0;
        background.SetActive(false);
        EvtSystem.EventDispatcher.AddListener<TrackUIMenuOpen>(HandleUIMenuTracking);
    }

    void HandleUIMenuTracking(TrackUIMenuOpen evt) {
        if (evt.isOpening) {
            uiMenusOpen += 1;
        } else {
            uiMenusOpen -= 1;
        }

        if (uiMenusOpen > 0) {
            background.SetActive(true);
        } else if (uiMenusOpen == 0) {
            background.SetActive(false);
        }
    }


    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<TrackUIMenuOpen>(HandleUIMenuTracking);
    }
}
