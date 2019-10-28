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

    List<BallData> ballQueue = new List<BallData>();
    //List<ActiveGameBalls> gameBalls

    // Start is called before the first frame update
    void Start()
    {
        CompileBallData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SpawnBall();
        }
    }

    private void CompileBallData()
    {
        BallData[] data = levelData.GetBalls();
        for (int i = data.Length - 1; i > -1; i--)
        {
            ballQueue.Add(data[i]);
        }
    }

    private void SpawnBall()
    {
        if (ballQueue.Count > 0)
        {
            int last = ballQueue.Count - 1;
            Sprite exercise = exerciseSprites.GetSprite(ballQueue[last].exercise);
            fireCannon.NewBall(ballQueue[last].fallDelay, exercise);
            ballQueue.RemoveAt(last);
            previewPanel.UpdatePreview();
        }
        else
        {
            Debug.Log("DONE!");
        }
        
    }

}
