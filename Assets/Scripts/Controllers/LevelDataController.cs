using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataController : MonoBehaviour 
{
    private BallData[] levelBalls;
    private TextAsset levelScript;


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
