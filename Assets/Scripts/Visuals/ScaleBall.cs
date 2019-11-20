using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBall : MonoBehaviour
{
    private GameManager Game;

    void Start()
    {
        Game = GameManager.Instance;
        StartCoroutine(ScaleUpBall());
    }

    void Update()
    {
        if (!Game.paused)
        {
            //if (transform.localScale.x < 0.5f)
            //{
            //    transform.localScale = new Vector3(transform.localScale.x + 0.00417f, transform.localScale.y + 0.00417f);
            //}
        }
        
    }

    private IEnumerator ScaleUpBall()
    {
        float t = 0;
        while (t <= 1.0 && !Game.paused)
        {
            t += Time.deltaTime / 3f;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
