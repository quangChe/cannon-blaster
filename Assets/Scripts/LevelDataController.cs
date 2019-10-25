using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataController : MonoBehaviour 
{
    private BallData[] levelBalls;
    private string levelDataFileName = "data.json";


    private void Awake()
    {
        LoadLevelData();
    }
    private void Start()
    {
    }

    private void Update()
    {
        
    }

    private void LoadLevelData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, levelDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
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
