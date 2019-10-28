using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnController : MonoBehaviour
{
    public LevelDataController levelData;
    BallData[] levelGameBalls;

    // Start is called before the first frame update
    void Start()
    {
        levelGameBalls = CompileBalls();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private BallData[] CompileBalls()
    {
        BallData[] levelGameBalls = levelData.GetBalls();

    }

}
