﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public string[] AllExercises = { "LS", "DK", "ZP", "CP" };
    public Object[] levels;
    public TextAsset loadedLevel;

    public bool paused = false;
    public bool doublePaused = false;

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
        if (paused) { doublePaused = true; }
        else {
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void Unpause()
    {
        if (doublePaused) { doublePaused = false; }
        else {
            paused = false;
            Time.timeScale = 1;
        }
    }
}
