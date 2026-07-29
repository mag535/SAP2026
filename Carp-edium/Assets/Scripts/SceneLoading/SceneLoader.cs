using UnityEngine;
using UnityEngine.SceneManagement;

namespace Carp {
    public class SceneLoader : MonoBehaviour
    {
        public void StartGame() {
            SceneManager.LoadScene("Gameplay");
        }

        public void ExitGame() {
            Application.Quit();
        }

        public void ReturnToMain() {
            SceneManager.LoadScene("MainMenu");
        }

        public void GoToScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public void GoToScene(int sceneIndex) {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
