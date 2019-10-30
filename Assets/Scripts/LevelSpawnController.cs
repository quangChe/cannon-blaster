using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnController : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip boom;

    [Header("Referenced Scripts")]
    public ExerciseSpriteDictionary exerciseSprites;
    public LevelDataController levelData;
    public FireCannon fireCannon;
    public PreviewExercise previewPanel;

    string[] AllExercises = {"LS", "DK", "ZP", "CP"};
    List<BallData> ballQueue = new List<BallData>();
    public Dictionary<string, ActiveBallQueue> activeQueue = new Dictionary<string, ActiveBallQueue>();

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
        StartCoroutine(SpawnBall(1f));
    }

    private void CompileActiveQueueLists()
    {
        foreach (string e in AllExercises)
        {
            ActiveBallQueue queue = new ActiveBallQueue();
            queue.isFiring = false;
            queue.instances = new List<ActiveBall>();
            activeQueue.Add(e, queue);
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
            Sprite exerSprite = exerciseSprites.GetSprite(lastBall.exercise);
            GameObject cannonBall = fireCannon.NewBall(lastBall, exerSprite);
            ballQueue.RemoveAt(last);
            previewPanel.UpdatePreview();
            SetActiveObject(cannonBall, lastBall);
            yield return new WaitForSeconds(lastBall.timeDelay);
        }

        // Do something here to end level
        Debug.Log("DONE!");
    }

    private void SetActiveObject(GameObject gObj, BallData data)
    {
        ActiveBall newBall = new ActiveBall();
        newBall.data = data;
        newBall.gameObject = gObj;
        activeQueue[data.exercise].instances.Add(newBall);
    }

    public void UpdateActiveObject(GameObject newObj, BallData d)
    {
        ActiveBall target = activeQueue[d.exercise].instances.First(x => x.data.id == d.id);
        Debug.Log(target);
        target.gameObject = newObj;
    }

    public void DestroyActiveObject(BallData d)
    {
        ActiveBallQueue queue = activeQueue[d.exercise];
        if (!queue.isFiring && queue.instances[0].data.id == d.id)
        {
            StartCoroutine(AnimateAndRemove(queue));
        }
    }

    private IEnumerator AnimateAndRemove(ActiveBallQueue q)
    {
        q.isFiring = true;
        ActiveBall target = q.instances[0];
        Rigidbody2D ball = target.gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();
        Instantiate(explosion, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
        Destroy(target.gameObject);
        AudioSource.PlayClipAtPoint(boom, Camera.main.transform.position, 0.7f);
        yield return new WaitForSeconds(0.05f);
        q.instances.RemoveAt(0);
        q.isFiring = false;
    }
}

public struct ActiveBallQueue
{
    public List<ActiveBall> instances;
    public bool isFiring;
}


public class ActiveBall
{
    public BallData data;
    public GameObject gameObject;
}