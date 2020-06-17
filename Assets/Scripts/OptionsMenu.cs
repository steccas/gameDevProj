using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        LoadPref("MasterVolume");
        LoadPref("MusicVolume");
        LoadPref("SFXMasterVolume");
        LoadPref("Fullscreen");
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

    void LoadPref(string key)
    {
        if (PlayerPrefs.HasKey(key) && key == "Fullscreen")
        {
            int value = PlayerPrefs.GetInt(key);
            if (value == 0) Screen.fullScreen = false;
            else Screen.fullScreen = true;
            return;
        }
        else if (PlayerPrefs.HasKey(key)) 
        {
            float value = PlayerPrefs.GetFloat(key);
            SetVolume(key, value);
        }
    }
}
