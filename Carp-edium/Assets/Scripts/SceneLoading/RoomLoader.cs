using UnityEngine;
using UnityEngine.SceneManagement;


namespace Carp {
    public class RoomLoader : MonoBehaviour
    {
        public bool isLoaded;
        public bool shouldLoad;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestLoadRoom>(HandleLoadRoom);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            /*
            if (SceneManager.sceneCount > 0) {
                for (int i = 0; i < SceneManager.sceneCount; i++) {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == gameObject.name) {
                        isLoaded = true;
                    }
                }
            } 
            */
        }

        void LoadScene() {
            if (isLoaded) { return; }

            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }

        void UnloadScene() {
            if (!isLoaded) { return; }

            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }

        void HandleLoadRoom(RequestLoadRoom evt) {
            if (evt.roomName != gameObject.name) { 
                UnloadScene();
                return;
            }

            Debug.Log("Loading Room [" + evt.roomName + "]");
            LoadScene();
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestLoadRoom>(HandleLoadRoom);
        }
    }
}
