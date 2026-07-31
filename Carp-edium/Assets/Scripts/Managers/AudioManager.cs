using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public Sound startBGM;

    [SerializeField]
    private AudioMixer audioMixer;

    private List<Sound> currentlyPlayingSounds = new List<Sound>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play(startBGM);
    }

    public void Play(Sound sound) {
        if (sound == null) {
            Debug.LogWarning($"Sound: not found.");
            return;
        }
        if (sound.source != null && sound.source.isPlaying) {
            Stop(sound);
        } else {
        // Add audio source
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        // Play
        sound.source.Play();
        currentlyPlayingSounds.Add(sound);
    }

    public void Stop(Sound sound) {
        if (sound == null) {
            Debug.LogWarning($"Sound: not found.");
            return;
        }
        sound.source.Stop();
        foreach(Sound s in currentlyPlayingSounds) {
            if (s == sound) {
                currentlyPlayingSounds.Remove(sound);
            }
        }
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
