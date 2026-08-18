using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private Sound startBGM;
    [SerializeField]
    private Sound uiClick;
    [SerializeField]
    private Sound deduction;
    [SerializeField]
    private Sound pageFlip;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource uicSource;
    [SerializeField]
    private AudioSource dedSource;
    [SerializeField]
    private AudioSource pagSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            Play(startBGM);
        }
        //bgmSource = gameObject.AddComponent<AudioSource>();
        //sfxSource = gameObject.AddComponent<AudioSource>();
        uicSource = gameObject.AddComponent<AudioSource>();
        uicSource.clip = uiClick.clip;
        uicSource.volume = uiClick.volume;
        uicSource.pitch = uiClick.pitch;
        uicSource.loop = uiClick.loop;
        dedSource = gameObject.AddComponent<AudioSource>();
        dedSource.clip = deduction.clip;
        dedSource.volume = deduction.volume;
        dedSource.pitch = deduction.pitch;
        dedSource.loop = uiClick.loop;
        if (pageFlip != null) {
            pagSource = gameObject.AddComponent<AudioSource>();
            pagSource.clip = pageFlip.clip;
            pagSource.volume = pageFlip.volume;
            pagSource.pitch = pageFlip.pitch;
            pagSource.loop = pageFlip.loop;
        }
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

    public void PlayUIClick() {
        if (uiClick == null) { return; }
        if (uicSource.isPlaying) {
            uicSource.Stop();
        }
        uicSource.Play();
        Debug.Log($"UI Click played");
    }
    public void PlayDeduction() {
        if (deduction == null) { return; }
        if (dedSource.isPlaying) {
            dedSource.Stop();
        }
        dedSource.Play();
        Debug.Log($"Deduction SFX played");
    }
    public void PlayPageFlip() {
        if (pageFlip == null) { return; }
        if (pagSource.isPlaying) {
            pagSource.Stop();
        }
        pagSource.Play();
        Debug.Log($"Page Flip played");
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
