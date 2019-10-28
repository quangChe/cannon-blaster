using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnController : MonoBehaviour
{
    public ExerciseSpriteDictionary exerciseSprites;
    public LevelDataController levelData;
    public FireCannon fireCannon;
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
        int last = ballData.Count - 1;
        Debug.Log(last);
        Sprite exercise = exerciseSprites.GetSprite(ballData[last].exercise);
        //ballData.RemoveAt(last);
        fireCannon.NewBall(ballData[last].fallDelay, exercise);
    }

}
