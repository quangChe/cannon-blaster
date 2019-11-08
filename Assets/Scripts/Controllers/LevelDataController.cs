using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataController : MonoBehaviour 
{
    private BallData[] levelBalls;
    private string levelDataFileName = "levels/1";


    private void Awake()
    {
        LoadLevelData();
    }
    private void Start()
    {
    }

    private void LoadLevelData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(levelDataFileName);
        if (jsonFile)
        {
            string dataAsJson = jsonFile.ToString();
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
