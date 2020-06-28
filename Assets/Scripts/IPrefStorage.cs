using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrefStorage<T>
{
    void SetPref(string key, T value);
}
