using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public GameObject pauseMenu;

    private GameManager Game;

    void Start()
    {
        Game = GameManager.Instance;
        pauseMenu.SetActive(false);
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

    public void GoToMenu()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Home");
        Game.Unpause();
    }
}
