using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public Sound startBGM;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource sfxSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play(startBGM);
        //bgmSource = gameObject.AddComponent<AudioSource>();
        //sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(Sound sound) {
        if (sound == null) {
            Debug.LogWarning($"Sound: not found.");
            return;
        }

        if (sound.type == SoundType.SFX) {
            AudioSource[] existingSources = GetComponents<AudioSource>();
            foreach(AudioSource src in existingSources) {
                if (src.clip == sound.clip) {
                    src.Stop();
                    src.Play();
                    Debug.Log($"old sound [{sound.name}] replayed");
                    return;
                }
            }
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = sound.clip;
            newSource.volume = sound.volume;
            newSource.pitch = sound.pitch;
            newSource.loop = sound.loop;
            newSource.Play();
            Debug.Log($"new sound [{sound.name}] created");
        } else {
            bgmSource.Stop();
            bgmSource.clip = sound.clip;
            bgmSource.volume = sound.volume;
            bgmSource.pitch = sound.pitch;
            bgmSource.loop = sound.loop;
            bgmSource.Play();
        }
    }

    public void Stop(Sound sound) {
        if (sound == null) {
            Debug.LogWarning($"Sound: not found.");
            return;
        }
        if (sound.type == SoundType.SFX) {
            foreach(AudioSource src in GetComponents<AudioSource>()) {
                if (src.clip == sound.clip) {
                    src.Stop();
                }
            }
        } else {
            bgmSource.Stop();
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
