using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefStorage<T> : MonoBehaviour, IPrefStorage<T>
{
    public void SetPref(string key, T value)
    {
        if (value is int) { PlayerPrefs.SetInt(key, (int)(object)value); }
        if (value is float) { PlayerPrefs.SetFloat(key, (int)(object)value); }
        PlayerPrefs.Save();
    }
}

public class PrefStorageInt : MonoBehaviour, IPrefStorage<int>
{
    public void SetPref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
}