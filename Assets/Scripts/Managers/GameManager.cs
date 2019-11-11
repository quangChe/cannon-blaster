using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public Object[] levels;
    public TextAsset loadedLevel;

    public bool paused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            levels = Resources.LoadAll("levels", typeof(TextAsset));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int levelNumber)
    {
        loadedLevel = (TextAsset)levels[levelNumber - 1];
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
