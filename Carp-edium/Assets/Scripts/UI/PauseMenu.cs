using UnityEngine;
using UnityEngine.EventSystems;


namespace Carp {
    public class PauseMenu : Singleton<PauseMenu>
    {
        [SerializeField]
        private GameObject menu;
        [SerializeField]
        private GameObject firstFocusedButton;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            menu.SetActive(false);
        }

        public void Pause() {
            menu.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(firstFocusedButton);
            EvtSystem.EventDispatcher.Raise<SetActionMap>( new SetActionMap {
                    actionMap = "UI" });
        }
        
        public void Resume() {
            menu.SetActive(false);
            Time.timeScale = 1f;
            EventSystem.current.SetSelectedGameObject(null);
            EvtSystem.EventDispatcher.Raise<SetActionMap>( new SetActionMap {
                    actionMap = "Game" });
        }

        public bool IsActive() {
            return menu.activeSelf;
        }
    }
}
