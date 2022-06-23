using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadAbout()
    {
        SceneManager.LoadScene(6);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
