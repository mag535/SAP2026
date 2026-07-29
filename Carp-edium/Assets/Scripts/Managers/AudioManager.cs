using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public string startBGM;
    //public Sound[] sounds;

    public List<Sound> sounds;

    [SerializeField]
    private AudioMixer audioMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // FIXME: Play BGM
        //Play(startBGM);
    }

    public void Play(Sound sound) {
        if (sound == null) {
            Debug.LogWarning("Sound: " + sound.name + " not found.");
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
    }

    public void Stop(Sound sound) {
        if (sound == null) {
            Debug.LogWarning("Sound: " + sound.name + " not found.");
            return;
        }
        sound.source.Stop();
    }

    /*
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        if (s.source.isPlaying) {
            //Debug.LogWarning("Sound: " + name + " is already playing.");
            Stop(name);
            //return;
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
    */

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
