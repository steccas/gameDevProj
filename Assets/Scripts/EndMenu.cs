using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : Menu
{
    public void LoadMenu()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex -2);
        StartCoroutine(Load(operation));
        audioManager.Play("Click");
        audioManager.Stop("Ending");
        audioManager.Play("MainTheme");
    }
}
