using UnityEngine;
using UnityEngine.UI;

namespace Carp {
    public class ConfirmationScreenManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject confirmationScreen;
        [SerializeField]
        private Button yesButton;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestOpenConfirmationScreen>(HandleOpen);
            EvtSystem.EventDispatcher.AddListener<RequestCloseConfirmationScreen>(HandleClose);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            confirmationScreen.SetActive(false);
        }

        void HandleOpen(RequestOpenConfirmationScreen evt) {
            SceneLoader yesButtonSceneLoader = yesButton.gameObject.GetComponent<SceneLoader>();
            yesButton.onClick.AddListener(delegate { 
                    yesButtonSceneLoader.GoToScene(evt.endingScreenName); });
            confirmationScreen.SetActive(true);
        }

        void HandleClose(RequestCloseConfirmationScreen evt) {
            confirmationScreen.SetActive(false);
            yesButton.onClick.AddListener(null);
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestOpenConfirmationScreen>(HandleOpen);
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseConfirmationScreen>(HandleClose);
        }
    }
}
