using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider BGM;
    [SerializeField] private Slider SFX;

    private const string MasterVolKey = "MasterVol";
    private const string BGMVolKey = "BGMVol";
    private const string SFXVolKey = "SFXVol";

    public static AudioManager Instance { get; private set; }

    private void Start()
    {
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        float volume;
        if (PlayerPrefs.HasKey(MasterVolKey))
        {
            volume = PlayerPrefs.GetFloat(MasterVolKey);
        }
        else
        {
            volume = 0f; // Set default value to 65 dB (0 in the linear scale used here)
            PlayerPrefs.SetFloat(MasterVolKey, volume);
        }
        audioMixer.SetFloat(MasterVolKey, volume);
        masterVolume.value = (volume + 65) / 65;

        if (PlayerPrefs.HasKey(BGMVolKey))
        {
            volume = PlayerPrefs.GetFloat(BGMVolKey);
        }
        else
        {
            volume = 0f; // Set default value to 65 dB (0 in the linear scale used here)
            PlayerPrefs.SetFloat(BGMVolKey, volume);
        }
        audioMixer.SetFloat(BGMVolKey, volume);
        BGM.value = (volume + 65) / 65;

        if (PlayerPrefs.HasKey(SFXVolKey))
        {
            volume = PlayerPrefs.GetFloat(SFXVolKey);
        }
        else
        {
            volume = 0f; // Set default value to 65 dB (0 in the linear scale used here)
            PlayerPrefs.SetFloat(SFXVolKey, volume);
        }
        audioMixer.SetFloat(SFXVolKey, volume);
        SFX.value = (volume + 65) / 65;

        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        volume = volume * 65 - 65;
        audioMixer.SetFloat(MasterVolKey, volume);
        PlayerPrefs.SetFloat(MasterVolKey, volume);
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        volume = volume * 65 - 65;
        audioMixer.SetFloat(BGMVolKey, volume);
        PlayerPrefs.SetFloat(BGMVolKey, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        volume = volume * 65 - 65;
        audioMixer.SetFloat(SFXVolKey, volume);
        PlayerPrefs.SetFloat(SFXVolKey, volume);
        PlayerPrefs.Save();
    }
}
