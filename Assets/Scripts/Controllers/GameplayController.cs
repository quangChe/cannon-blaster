using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject confirmMenu;

    private GameManager Game;

    void Start()
    {
        Game = GameManager.Instance;
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Game.Pause();
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        Game.Unpause();
        pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Game");
        Game.Unpause();
    }

    public void GoHome()
    {
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(true);
    }

    public void ConfirmExit()
    {
        confirmMenu.SetActive(false);
        SceneManager.LoadScene("Home");
        Game.Unpause();
    }

    public void CancelExit()
    {
        confirmMenu.SetActive(false);
        Game.Unpause();
    }
}
