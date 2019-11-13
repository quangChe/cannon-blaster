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

    public float[] successRate = { 0, 0 };
    public float successPercent;
    public Dictionary<string, int> successfulActivityRecord = new Dictionary<string, int>();

    private void Awake()
    {
        LoadLevelData();
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
            BuildExerciseDictionary();
        }
        else
        {
            Debug.LogError("Cannot load level data.");
        }
    }

    private void BuildExerciseDictionary()
    {
        foreach (BallData data in levelBalls)
        {
            if (!successfulActivityRecord.ContainsKey(data.exercise))
            {
                successfulActivityRecord.Add(data.exercise, 0);
            }
        }

        foreach (string key in successfulActivityRecord.Keys)
        {
            Debug.Log(key);
        }
    }

    public BallData[] GetBalls()
    {
        return levelBalls;
    }

    private void Update()
    {
        successPercent = Mathf.Round((successRate[0] / successRate[1]) * 100);
    }
}
