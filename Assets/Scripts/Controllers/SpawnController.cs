using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject explosion;
    public AudioClip boom;

    [Header("Referenced Scripts")]
    public SpriteDictionary gameSprites;
    public CannonController CannonController;
    public PreviewExercise previewPanel;
    public GameplayController gamePlay;


    float timeout = 3f;
    List<BallObject> ballQueue = new List<BallObject>();
    Dictionary<string, List<ActiveBall>> activeQueue = new Dictionary<string, List<ActiveBall>>();
    LevelDataController levelData;
    GameManager Game = GameManager.Instance;

    private void Awake()
    {
        BluetoothManager.Instance.MountToLevel(this);
        levelData = GetComponent<LevelDataController>();
    }

    void Start()
    {
        CompileBallData();
        CompileActiveQueueLists();
    }

    void Update()
    {
        timeout -= Time.deltaTime;

        if (timeout <= 0f && ballQueue.Count > 0)
        {
            SpawnBall();
        }
    }

    private void CompileActiveQueueLists()
    {
        foreach (string e in Game.AllExercises)
        {
            List<ActiveBall> queue = new List<ActiveBall>();
            activeQueue.Add(e, queue);
        }
    }

    private void CompileBallData()
    {
        BallObject[] data = levelData.GetBalls();
        for (int i = data.Length - 1; i > -1; i--)
        {
            ballQueue.Add(data[i]);
        }
    }

    private void SpawnBall()
    {
        int last = ballQueue.Count - 1;
        BallObject lastBall = ballQueue[last];
        Sprite exerSprite = gameSprites.GetSprite(lastBall.exercise);
        GameObject cannonBall = CannonController.FireProjectile(lastBall, exerSprite);
        ballQueue.RemoveAt(last);
        previewPanel.UpdatePreview();
        CreateActiveObject(cannonBall, lastBall);
        timeout = lastBall.timeDelay;
    }

    private void CreateActiveObject(GameObject gObj, BallObject data)
    {
        ActiveBall newBall = new ActiveBall();
        newBall.data = data;
        newBall.gameObject = gObj;
        activeQueue[data.exercise].Add(newBall);
    }

    public void UpdateActiveObject(GameObject newObj, BallObject d)
    {
        ActiveBall target = activeQueue[d.exercise].First(x => x.data.id == d.id);
        target.gameObject = newObj;
    }

    public void DestroyActiveObject(string exercise)
    {
        List<ActiveBall> q = activeQueue[exercise];
       
        if (q.Count > 0)
        {
            // Animate explosion, destroy object, and play audio
            ActiveBall target = q[0];
            Rigidbody2D ball = target.gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();
            Instantiate(explosion, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            Destroy(target.gameObject);
            AudioSource.PlayClipAtPoint(boom, Camera.main.transform.position, 0.7f);

            // Remove from queue, update level data
            q.RemoveAt(0);
            levelData.successRate[0]++;
            levelData.successfulActivityRecord[exercise]++;

            // Spawn new ball if no active ball (to not keep player waiting)
            timeout = CheckActiveQueue() ? timeout : 0f;

            CheckLevelEnd();
        }
    }

    public void RemoveFromActive(BallObject data)
    {
        List<ActiveBall> q = activeQueue[data.exercise];
        ActiveBall target = q.Single(t => t.data.id == data.id);
        q.Remove(target);

        CheckLevelEnd();
    }

    private bool CheckActiveQueue()
    {
        bool queueing = false;

        foreach (List<ActiveBall> list in activeQueue.Values)
        {
            if (list.Count > 0) { queueing = true; }
        }

        return queueing;
    }

    private void CheckLevelEnd()
    {
        bool queueing = ballQueue.Count > 0;
        queueing = CheckActiveQueue() ? true : queueing;

        if (!queueing) {
            // Also need to store data here;
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1f);
        bool success = levelData.StarsWon() > 0;
        gamePlay.EndLevel(success);
    }
}

public struct ActiveBallQueue
{
    public List<ActiveBall> instances;
}

public class ActiveBall
{
    public BallObject data;
    public GameObject gameObject;
}