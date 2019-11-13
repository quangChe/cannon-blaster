using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    private GameManager Game;

    public GameObject menuCanvas;
    public GameObject pauseMenu, confirmMenu, gameEndMenu;

    public enum States { Pause, Confirm, GameEnd }
    public States menuState;

    void Start()
    {
        //Game = GameManager.Instance;
        //menuCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        Game.Pause();
        menuCanvas.SetActive(true);
        menuState = States.Pause;
        RenderMenu();
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

    public void ToLevels()
    {
        menuCanvas.SetActive(false);
        SceneManager.LoadScene("Levels");
        Game.Unpause();
    }

    public void ToMenu()
    {
        menuState = States.Confirm;
        RenderMenu();
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

    private void RenderMenu()
    {
        switch(menuState)
        {
            case States.Confirm:
                pauseMenu.SetActive(false);
                gameEndMenu.SetActive(false);
                confirmMenu.SetActive(true);
                break;
            case States.Pause:
                pauseMenu.SetActive(true);
                gameEndMenu.SetActive(false);
                confirmMenu.SetActive(false);
                break;
            case States.GameEnd:
                pauseMenu.SetActive(false);
                gameEndMenu.SetActive(true);
                confirmMenu.SetActive(false);
                break;
        }
    }
}
