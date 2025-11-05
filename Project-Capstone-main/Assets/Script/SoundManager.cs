using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip uiClick;
    public AudioClip openPhone;
    public AudioClip closePhone;
    public AudioClip fuel;
    public AudioClip gameOver;

    public AudioSource[] audioSources;

    private new AudioSource audio;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameOver);
        else
            instance = this;

        audio = GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1.0f);
        SetAudioSourceVolumes(savedVolume);
    }

    private void SetAudioSourceVolumes(float volume)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volume;
        }
    }

    public void UIClickSfx()
    {
        audio.PlayOneShot(uiClick);
    }

    public void openSfx()
    {
        audio.PlayOneShot(openPhone);
    }

    public void closeSfx()
    {
        audio.PlayOneShot(closePhone);
    }

    public void FuelSfx()
    {
        audio.PlayOneShot(fuel);
    }

    public void GameOverSfx()
    {
        audio.PlayOneShot(gameOver);
    }
}
