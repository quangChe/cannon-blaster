using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataController : MonoBehaviour 
{
    private BallData[] levelBalls;
    private TextAsset levelScript;
    private GameManager Game = GameManager.Instance;

    public int[] successRate = { 0, 0 };
    public Dictionary<string, int> successfulActivityRecord = new Dictionary<string, int>();

    private void Awake()
    {
        LoadLevelData();
        BuildActivityLibrary();
    }

    private void BuildActivityLibrary()
    {
        foreach (string exercise in Game.AllExercises)
        {
            successfulActivityRecord.Add(exercise, 0);
        }
    }

    private void LoadLevelData()
    {
        levelScript = GameManager.Instance.loadedLevel;
        if (levelScript)
        {
            string dataAsJson = levelScript.ToString();
            LevelData loadedData = JsonUtility.FromJson<LevelData>(dataAsJson);
            levelBalls = loadedData.balls;
            successRate[1] = levelBalls.Length;
        }
        else
        {
            Debug.LogError("Cannot load level data.");
        }
    }

    public BallData[] GetBalls()
    {
        return levelBalls;
    }
}
