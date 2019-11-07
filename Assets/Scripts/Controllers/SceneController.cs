using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;

    public CanvasGroup faderCanvasGroup;
    public float fadeDuration = 1f;
    public string startingSceneName = "Loading";
    //public SaveData playerSaveData;

    private bool isFading;

    private IEnumerator Start()
    {
        faderCanvasGroup.alpha = 1f;

        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

        StartCoroutine(Fade(0f));
    }

    public void FadeAndLoadScene(string sceneName, bool withFade)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName, withFade));
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName, bool withFade)
    {
        if (withFade) {
            yield return StartCoroutine(Fade(1f));
        }

        BeforeSceneUnload?.Invoke();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        AfterSceneLoad?.Invoke();

        if (withFade)
        {
            yield return StartCoroutine(Fade(0f));
        }
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(
                faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }

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
