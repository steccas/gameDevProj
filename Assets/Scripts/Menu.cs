using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public Slider loadingBar;

    protected AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.GetInstance();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    protected IEnumerator Load(AsyncOperation operation)
    {
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
