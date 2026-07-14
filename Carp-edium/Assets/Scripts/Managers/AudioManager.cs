using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public string startBGM;
    public Sound[] sounds;

    [SerializeField]
    private AudioMixer audioMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add AudioSource and initialize
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        // FIXME: Play BGM
        //Play(startBGM);
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        if (s.source.isPlaying) {
            Debug.LogWarning("Sound: " + name + " is already playing.");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Stop();
    }

    public void StopAll() {
        foreach (Sound s in sounds) {
            if (s == null) {
                Debug.LogWarning("Sound: " + name + " not found.");
                return;
            }
            s.source.Stop();
        }
    }

    public void StopAllSFX() {
        foreach (Sound s in sounds) {
            if (s == null) {
                Debug.LogWarning("Sound: " + name + " not found.");
                return;
            }
            if (s.type != SoundType.SFX) {
                continue;
            }
            s.source.Stop();
        }
    }

    // TODO: connect to sliders in a settings menu

    public void SetMasterVolume(float level) {
        audioMixer.SetFloat("MasterVolume", level);
    }

    public void SetSoundEffectsVolume(float level) {
        audioMixer.SetFloat("SoundEffectsVolume", level);
    }

    public void SetMusicVolume(float level) {
        audioMixer.SetFloat("MusicVolume", level);
    }

}
