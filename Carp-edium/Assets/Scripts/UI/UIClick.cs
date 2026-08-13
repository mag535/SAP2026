using UnityEngine;
using UnityEngine.UI;

public class UIClick : MonoBehaviour
{
    void Start() {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlayClick);
    }

    void PlayClick() {
        AudioManager.Instance.PlayUIClick();
    }
}
