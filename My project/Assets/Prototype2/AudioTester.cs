using UnityEngine;

public class AudioTester : MonoBehaviour
{
    public Sound sfx;

    public void PlaySFX() {
        AudioManager.Instance.Play(sfx.name);
    }
}
