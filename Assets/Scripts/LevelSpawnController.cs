using System;
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

    string[] AllExercises = {"LS", "DK", "ZP", "CP"};
    List<BallData> ballQueue = new List<BallData>();
    Dictionary<string, List<ActiveBallData>> activeQueue = new Dictionary<string, List<ActiveBallData>>();

    //List<ActiveGameBalls> gameBalls

    // Start is called before the first frame update
    void Start()
    {
        CompileBallData();
        CompileActiveQueueLists();
        SpawnGameBalls();
    }

    private void SpawnGameBalls()
    {
        StartCoroutine(SpawnBall(5f));
    }

    private void CompileActiveQueueLists()
    {
        foreach (string e in AllExercises)
        {
            activeQueue.Add(e, new List<ActiveBallData>());
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

    private IEnumerator SpawnBall(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (ballQueue.Count > 0)
        {
            int last = ballQueue.Count - 1;
            BallData lastBall = ballQueue[last];
            Debug.Log(lastBall.exercise);
            Sprite exerSprite = exerciseSprites.GetSprite(lastBall.exercise);
            GameObject cannonBall = fireCannon.NewBall(lastBall, exerSprite);
            ballQueue.RemoveAt(last);
            previewPanel.UpdatePreview();
            yield return new WaitForSeconds(lastBall.timeDelay);
           //SetActiveObject(cannonBall, ballQueue[last]);

        }
        
        Debug.Log("DONE!");
    }

    private void SetActiveObject(GameObject gObj, BallData data)
    {
        //if ()
    }
}

public class ActiveBallData
{

}