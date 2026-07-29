using UnityEngine;


namespace Carp {
    public class PauseMenu : Singleton<PauseMenu>
    {
        public GameObject menu;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            menu.SetActive(false);
        }

        public void Pause() {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
        
        public void Resume() {
            menu.SetActive(false);
            Time.timeScale = 1f;
        }

        public bool IsActive() {
            return menu.activeSelf;
        }
    }
}
