using UnityEngine;
using UnityEngine.EventSystems;

namespace Carp {
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject menu;
        [SerializeField]
        private GameObject firstFocusedButton;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<TogglePauseMenu>(HandleToggleMenu);
        }

        void Start()
        {
            menu.SetActive(false);
        }

        void HandleToggleMenu(TogglePauseMenu _) {
            if (IsActive()) {
                Close();
            } else {
                Open();
            }
        }

        private void Open() {
            menu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstFocusedButton);
            EvtSystem.EventDispatcher.Raise<RequestPauseGame>( new RequestPauseGame {});
        }
        
        private void Close() {
            menu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EvtSystem.EventDispatcher.Raise<RequestResumeGame>( new RequestResumeGame {});
        }

        private bool IsActive() {
            return menu.activeSelf;
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<TogglePauseMenu>(HandleToggleMenu);
        }
    }
}
