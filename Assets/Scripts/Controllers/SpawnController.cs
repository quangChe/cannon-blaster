﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private ExerciseSpriteDictionary exerciseSprites;

    public GameObject explosion;
    public AudioClip boom;

    [Header("Referenced Scripts")]
    public LevelDataController levelData;
    public CannonController CannonController;
    public PreviewExercise previewPanel;

    
    string[] AllExercises = {"LS", "DK", "ZP", "CP"};
    List<BallData> ballQueue = new List<BallData>();
    public Dictionary<string, List<ActiveBall>> activeQueue = new Dictionary<string, List<ActiveBall>>();

    private void Awake()
    {
        BluetoothManager.Instance.MountToLevel(this);
    }

    void Start()
    {
        CompileBallData();
        CompileActiveQueueLists();
        SpawnGameBalls();
    }

    private void SpawnGameBalls()
    {
        exerciseSprites = GetComponent<ExerciseSpriteDictionary>();
        StartCoroutine(SpawnBall(1f));
    }

    private void CompileActiveQueueLists()
    {
        foreach (string e in AllExercises)
        {
            List<ActiveBall> queue = new List<ActiveBall>();
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
            GameObject cannonBall = CannonController.FireProjectile(lastBall, exerSprite);
            ballQueue.RemoveAt(last);
            previewPanel.UpdatePreview();
            CreateActiveObject(cannonBall, lastBall);
            yield return new WaitForSeconds(lastBall.timeDelay);
        }

        // Do something here to end level
        Debug.Log("DONE!");
    }

    private void CreateActiveObject(GameObject gObj, BallData data)
    {
        ActiveBall newBall = new ActiveBall();
        newBall.data = data;
        newBall.gameObject = gObj;
        activeQueue[data.exercise].Add(newBall);
    }

    public void UpdateActiveObject(GameObject newObj, BallData d)
    {
        ActiveBall target = activeQueue[d.exercise].First(x => x.data.id == d.id);
        target.gameObject = newObj;
    }

    public void DestroyActiveObject(string exercise)
    {
        List<ActiveBall> q = activeQueue[exercise];
        //if (!queue.isFiring && queue.instances[0].data.id == d.id)
        if (q.Count > 0)
        {
            ActiveBall target = q[0];
            Rigidbody2D ball = target.gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();
            Instantiate(explosion, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            Destroy(target.gameObject);
            AudioSource.PlayClipAtPoint(boom, Camera.main.transform.position, 0.7f);
            q.RemoveAt(0);
        }
    }

    public void RemoveFromActive(BallData data)
    {
        List<ActiveBall> q = activeQueue[data.exercise];
        ActiveBall target = q.Single(t => t.data.id == data.id);
        q.Remove(target);
    }
}

public struct ActiveBallQueue
{
    public List<ActiveBall> instances;
}

public class ActiveBall
{
    public BallData data;
    public GameObject gameObject;
}