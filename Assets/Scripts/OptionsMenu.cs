using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMaster(float volume)
    {
        SetVolume("MasterVolume", volume);
    }

    public void SetMusic(float volume)
    {
        SetVolume("MusicVolume", volume);
    }

    public void SetSFX(float volume)
    {
        SetVolume("SFXMasterVolume", volume);
    }
    private void SetVolume(string value, float volume)
    {
        audioMixer.SetFloat(value, volume);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
