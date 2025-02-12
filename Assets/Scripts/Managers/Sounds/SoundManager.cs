﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum soundStates
{
    WeaponReload,
    WeaponFiring,
    WeaponOutOfAmmo
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_SoundManagerInstance;
    private AudioSource m_MusicAudioSource;
    private AudioSource m_SFXAudioSource;
    private AudioClip m_MainLevelMusic;

    public static SoundManager Instance()
    {
        if (m_SoundManagerInstance == null)
            //Create a gameObject in the hiearchy of type sound manager
            m_SoundManagerInstance = new GameObject("Sound Manager", typeof(SoundManager)).GetComponent<SoundManager>();

        return m_SoundManagerInstance;
    }

    #region INITILISATION
    //Used for initialising variables or game states
    private void Awake()
    {
        //We want the manager not to be destroyed as it may contain essential data (e.g music)
        DontDestroyOnLoad(this.gameObject);

        m_MusicAudioSource = this.gameObject.AddComponent<AudioSource>();
        m_SFXAudioSource = this.gameObject.AddComponent<AudioSource>();

        //Make sure music always loops
        m_MusicAudioSource.loop = true;

        m_MainLevelMusic = Resources.Load<AudioClip>("Music/Music");
    }

    #endregion

    #region MUSIC
    public void PlayMainLevelMusic()
    {
        m_MusicAudioSource.clip = m_MainLevelMusic;
        m_MusicAudioSource.Play();
    }

    public void StopMusic() { m_MusicAudioSource.Stop(); }
    public void PauseMusic() { m_MusicAudioSource.Pause(); }
    public void ResumeMusic() { m_MusicAudioSource.UnPause(); }

    #endregion

    #region SOUND EFFECTS
    public void PlaySFX(AudioClip SFXClip)
    {
        m_SFXAudioSource.PlayOneShot(SFXClip);
    }
    public void PlaySFX(AudioClip SFXClip, float volume)
    {
        m_SFXAudioSource.Stop();
        m_SFXAudioSource.PlayOneShot(SFXClip, volume);
    }

    public void PlayRandomSFX(AudioClip[] SFXClips)
    {
        m_SFXAudioSource.PlayOneShot(SFXClips[Random.Range(0, SFXClips.Length - 1)]);
    }

    public void StopSFX()
    {
        m_SFXAudioSource.Stop();
    }

    #endregion
}

