using UnityEngine;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    public string currentScreen;
    public List<GameObject> screens;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // loop through list, turn off all screens except main
        foreach (GameObject screen in screens) {
            if (screen.name == "MainScreen") {
                screen.SetActive(true);
                continue;
            }
            screen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoNothing() {
    }

    public void ChangeScreen(string targetScreen) {
        // loop through list, turn on target screen
        foreach (GameObject screen in screens) {
            if (screen.name == targetScreen) {
                screen.SetActive(true);
                break;
            }
        }
        // loop through list, turn off current screen
        foreach (GameObject screen in screens) {
            if (screen.name == currentScreen) {
                screen.SetActive(false);
                break;
            }
        }

        currentScreen = targetScreen;
    }
}
