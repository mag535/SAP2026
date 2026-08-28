using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Carp {
    public class ConfirmationScreenManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject confirmationScreen;
        [SerializeField]
        private GameObject firstFocusedButton;

        [SerializeField]
        private EndScreen currentGate = EndScreen.NONE;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestOpenConfirmationScreen>(HandleOpen);
            EvtSystem.EventDispatcher.AddListener<RequestCloseConfirmationScreen>(HandleClose);
            EvtSystem.EventDispatcher.AddListener<ConfirmGateChoice>(HandleGateChoice);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            confirmationScreen.SetActive(false);
        }

        void HandleOpen(RequestOpenConfirmationScreen evt) {
            currentGate = evt.endingScreenName;
            confirmationScreen.SetActive(true);
            EvtSystem.EventDispatcher.Raise<SetActionMap>( new SetActionMap {
                    actionMap = "UI" });
            EventSystem.current.SetSelectedGameObject(firstFocusedButton);
        }

        void HandleClose(RequestCloseConfirmationScreen evt) {
            currentGate = EndScreen.NONE;
            EventSystem.current.SetSelectedGameObject(null);
            confirmationScreen.SetActive(false);
            EvtSystem.EventDispatcher.Raise<SetActionMap>( new SetActionMap {
                    actionMap = "Game" });
        }

        void HandleGateChoice(ConfirmGateChoice evt) {
            SceneLoader thisSL = GetComponent<SceneLoader>();
            if (thisSL == null) { return; }
            thisSL.GoToScene(Stuff.endScreenDict[currentGate]);
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestOpenConfirmationScreen>(HandleOpen);
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseConfirmationScreen>(HandleClose);
            EvtSystem.EventDispatcher.RemoveListener<ConfirmGateChoice>(HandleGateChoice);
        }
    }
}
