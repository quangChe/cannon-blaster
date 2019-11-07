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

    public void GoToHome(int levelNumber)
    {
        SceneManager.LoadScene("Home");
    }

    public void GoToLevels()
    {
        SceneManager.LoadScene("Levels");
    }
}
