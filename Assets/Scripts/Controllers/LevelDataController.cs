using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataController : MonoBehaviour 
{
    private BallObject[] levelBalls;
    private TextAsset levelScript;
    private GameManager Game = GameManager.Instance;

    public float[] successRate = { 0, 0 };
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
            LevelObject loadedData = JsonUtility.FromJson<LevelObject>(dataAsJson);
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
        foreach (BallObject data in levelBalls)
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

    public BallObject[] GetBalls()
    {
        return levelBalls;
    }

    public int StarsWon()
    {
        float percent = Mathf.Round((successRate[0] / successRate[1]) * 100);

        if (percent >= 30f && percent < 60f)
        {
            return 1;
        }
        if (percent >= 60f && percent < 95f)
        {
            return 2;
        }
        if (percent >= 95f)
        {
            return 3;
        }

        return 0;
    }

}
