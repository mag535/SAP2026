using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BGM,
    SFX,
}

[CreateAssetMenu(fileName = "Sound", menuName = "Custom Game Assets/Sound")]
[System.Serializable]
public class Sound : ScriptableObject
{
    public SoundType type;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    [Range(0.1f, 3f)]
    public float pitch = 1.0f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
