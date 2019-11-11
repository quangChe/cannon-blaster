using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
