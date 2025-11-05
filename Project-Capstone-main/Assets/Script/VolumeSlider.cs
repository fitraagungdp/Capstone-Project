using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource[] audioSources;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 1.0f);
        SetAudioSourceVolumes(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float newVolume)
    {
        SetAudioSourceVolumes(newVolume);
        PlayerPrefs.SetFloat("GameVolume", newVolume);
        PlayerPrefs.Save();
    }

    private void SetAudioSourceVolumes(float volume)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volume;
        }
    }
}
