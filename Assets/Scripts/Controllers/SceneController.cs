using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

    //public void GoToLevels(int levelNumber)
    public void GoToLevels()
    {
        SceneManager.LoadScene("Levels");
    }
}
