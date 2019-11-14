using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    private GameManager Game;

    public GameObject menuCanvas;
    public GameObject pauseMenu, confirmMenu, gameOverMenu, levelClearedMenu;

    public enum States { Pause, Confirm, GameOver, LevelCleared }
    public States menuState;

    void Start()
    {
        Game = GameManager.Instance;
        menuCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        Game.Pause();
        menuState = States.Pause;
        OpenMenu();
    }

    public void UnpauseGame()
    {
        Game.Unpause();
        menuCanvas.SetActive(false);
    }

    public void RestartLevel()
    {
        menuCanvas.SetActive(false);
        SceneManager.LoadScene("Game");
        Game.Unpause();
    }

    public void ToLevelMenu()
    {
        menuCanvas.SetActive(false);
        SceneManager.LoadScene("Levels");
        Game.Unpause();
    }

    public void ToMainMenu()
    {
        menuState = States.Confirm;
        OpenMenu();
    }

    public void ConfirmExit()
    {
        menuCanvas.SetActive(false);
        SceneManager.LoadScene("Home");
        Game.Unpause();
    }

    public void CancelExit()
    {
        menuCanvas.SetActive(false);
        Game.Unpause();
    } 

    public void EndLevel(bool success)
    {
        //Game.Pause();
        menuState = (success) ? States.LevelCleared : States.GameOver;
        OpenMenu();
    }

    private void OpenMenu()
    {
        menuCanvas.SetActive(true);

        switch (menuState)
        {
            case States.Confirm:
                confirmMenu.SetActive(true);
                pauseMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                levelClearedMenu.SetActive(false);
                break;
            case States.Pause:
                pauseMenu.SetActive(true);
                gameOverMenu.SetActive(false);
                confirmMenu.SetActive(false);
                levelClearedMenu.SetActive(false);
                break;
            case States.GameOver:
                gameOverMenu.SetActive(true);
                pauseMenu.SetActive(false);
                confirmMenu.SetActive(false);
                levelClearedMenu.SetActive(false);
                break;
            case States.LevelCleared:
                levelClearedMenu.SetActive(true);
                gameOverMenu.SetActive(false);
                pauseMenu.SetActive(false);
                confirmMenu.SetActive(false);
                levelClearedMenu.GetComponent<LevelClearedUI>().StartUIAnimation();
                break;
        }
    }
}
