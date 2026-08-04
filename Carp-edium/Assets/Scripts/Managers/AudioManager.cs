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

    private List<Sound> currentlyPlayingSounds = new List<Sound>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play(startBGM);
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(Sound sound) {
        if (sound == null) {
            Debug.LogWarning($"Sound: not found.");
            return;
        }
        if (sfxSource.isPlaying) {
            sfxSource.Stop();
        }
        if (sound.clip != sfxSource.clip) {
            sound.source = gameObject.AddComponent<AudioSource>();

            sfxSource.clip = sound.clip;
            sfxSource.volume = sound.volume;
            sfxSource.pitch = sound.pitch;
            sfxSource.loop = sound.loop;
        }
        sfxSource.Play();
    }

    public void Stop(Sound sound) {
        if (sound == null) {
            Debug.LogWarning($"Sound: not found.");
            return;
        }
        sfxSource.Stop();
    }

    public void StopAll() {
        foreach (Sound s in currentlyPlayingSounds) {
            if (s == null) {
                Debug.LogWarning($"Sound: not found.");
                return;
            }
            s.source.Stop();
        }
        currentlyPlayingSounds.Clear();
    }

    public void StopAllSFX() {
        foreach (Sound s in currentlyPlayingSounds) {
            if (s == null) {
                Debug.LogWarning($"Sound: not found.");
                return;
            }
            if (s.type != SoundType.SFX) {
                continue;
            }
            s.source.Stop();
            currentlyPlayingSounds.Remove(s);
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
