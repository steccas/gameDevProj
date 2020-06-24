using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider masterSFXSlider;
    public Toggle fsToggle;
    public Dropdown resDropdown;

    Resolution[] resolutions;

    public void Awake()
    {
        LoadPref("MasterVolume");
        LoadPref("MusicVolume");
        LoadPref("SFXMasterVolume");
        LoadPref("Fullscreen");
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> resOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resOptions.Add(option);
        }

        resDropdown.AddOptions(resOptions);
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
            if (value == 0) 
            { 
                Screen.fullScreen = false;
                fsToggle.SetIsOnWithoutNotify(false);
            }
            else
            {
                Screen.fullScreen = true;
                fsToggle.SetIsOnWithoutNotify(true);
            }
            return;
        }
        else if (PlayerPrefs.HasKey(key)) 
        {
            float value = PlayerPrefs.GetFloat(key);
            SetVolume(key, value);
            switch (key)
            {
                case "MasterVolume":
                    masterSlider.SetValueWithoutNotify(value);
                    break;
                case "MusicVolume":
                    musicSlider.SetValueWithoutNotify(value);
                    break;
                case "SFXMasterVolume":
                    masterSFXSlider.SetValueWithoutNotify(value);
                    break;
            }  
        }
    }
}
