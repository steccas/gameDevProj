using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
       
    }

    public void SetMaster(float volume)
    {
        SetVolume("MasterVolume", volume);
        SetPref("MasterVolume", volume);
    }

    public void SetMusic(float volume)
    {
        SetVolume("MusicVolume", volume);
        SetPref("MusicVolume", volume);
    }

    public void SetSFX(float volume)
    {
        SetVolume("SFXMasterVolume", volume);
        SetPref("SFXMasterVolume", volume);
    }
    private void SetVolume(string value, float volume)
    {
        audioMixer.SetFloat(value, volume);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        int pref;
        if (isFullScreen) pref = 1;
        else pref = 0;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("Fullscreen", pref);
        PlayerPrefs.Save();
    }
    void SetPref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
}
