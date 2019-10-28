using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnController : MonoBehaviour
{
    [Header("Referenced Scripts")]
    public ExerciseSpriteDictionary exerciseSprites;
    public LevelDataController levelData;
    public FireCannon fireCannon;
    public PreviewExercise previewPanel;

    List<BallData> ballData = new List<BallData>();
    //List<ActiveGameBalls> gameBalls

    // Start is called before the first frame update
    void Start()
    {
        CompileBallData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    private void CompileBallData()
    {
        BallData[] data = levelData.GetBalls();
        for (int i = data.Length - 1; i > -1; i--)
        {
            ballData.Add(data[i]);
        }
    }

    private void SpawnBall()
    {
        if (ballData.Count > 0)
        {
            int last = ballData.Count - 1;
            Sprite exercise = exerciseSprites.GetSprite(ballData[last].exercise);
            fireCannon.NewBall(ballData[last].fallDelay, exercise);
            ballData.RemoveAt(last);
            previewPanel.UpdatePreview();
        }
        else
        {
            Debug.Log("DONE!");
        }
        
    }

}
