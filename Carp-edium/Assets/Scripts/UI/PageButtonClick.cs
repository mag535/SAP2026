using UnityEngine;
using UnityEngine.UI;

namespace Carp {
    public class PageButtonClick : MonoBehaviour
    {
        void Start() {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(PlayPageFlip);
        }

        void PlayPageFlip() {
            AudioManager.Instance.PlayPageFlip();
        }
    }
}
