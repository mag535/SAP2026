using UnityEngine;


namespace Carp {
    public class PauseMenu : Singleton<PauseMenu>
    {
        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestPauseGame>(HandleRequestPauseGame);
            EvtSystem.EventDispatcher.AddListener<RequestResumeGame>(HandleRequestResumeGame);
        }

        private void HandleRequestPauseGame(RequestPauseGame _) {
            Pause();
        }

        private void HandleRequestResumeGame(RequestResumeGame _) {
            Resume();
        }

        private void Pause() {
            Time.timeScale = 0;
            EvtSystem.EventDispatcher.Raise<SetActionMap>( new SetActionMap {
                    actionMap = "UI" });
        }
        
        private void Resume() {
            Time.timeScale = 1f;
            EvtSystem.EventDispatcher.Raise<SetActionMap>( new SetActionMap {
                    actionMap = "Game" });
        }

        public void ReturnToMainFromPause() {
            EvtSystem.EventDispatcher.Raise<TogglePauseMenu>( new TogglePauseMenu {});
            SceneLoader thisSL = GetComponent<SceneLoader>();
            thisSL.ReturnToMain();
        }

        public void QuitFromPause() {
            EvtSystem.EventDispatcher.Raise<TogglePauseMenu>( new TogglePauseMenu {});
            SceneLoader thisSL = GetComponent<SceneLoader>();
            thisSL.ExitGame();
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestPauseGame>(HandleRequestPauseGame);
            EvtSystem.EventDispatcher.RemoveListener<RequestResumeGame>(HandleRequestResumeGame);
        }
    }
}
