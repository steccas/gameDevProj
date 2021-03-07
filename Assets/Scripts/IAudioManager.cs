using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioManager
{
    void Play(string audio);
    void Stop(string audio);
    void FadeOut(string audio);
    void FadeIn(string audio);
}
