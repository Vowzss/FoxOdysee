using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private Manager manager;
    public Sound[] sounds;

    public bool musicIsPlaying = false;

    private void Awake()
    {
        manager = GameObject.FindObjectOfType<Manager>();

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = true;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == name);
        s.audioSource.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == name);
        s.audioSource.Stop();
    }

    private void Update()
    {
        if(manager.currentLevel >= 1 && !musicIsPlaying)
            PlaySoundGame();
        else if(manager.currentLevel < 1 && !musicIsPlaying)
            PlaySoundMenu();
    }

    private void PlaySoundMenu()
    {
        PlaySound("WaterTheme");
        PlaySound("ForestTheme");

        StopSound("PlayTheme");
        musicIsPlaying = true;
    }

    private void PlaySoundGame()
    {
        StopSound("WaterTheme");
        StopSound("ForestTheme");

        PlaySound("PlayTheme");
        musicIsPlaying = true;
    }
}
