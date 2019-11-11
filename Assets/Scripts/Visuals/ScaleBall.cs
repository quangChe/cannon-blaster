using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBall : MonoBehaviour
{
    private GameManager Game;

    void Start()
    {
        Game = GameManager.Instance;
    }

    void Update()
    {
        if (!Game.paused)
        {
            if (transform.localScale.x < 0.5f)
            {
                transform.localScale = new Vector3(transform.localScale.x + 0.00417f, transform.localScale.y + 0.00417f);
            }
        }
        
    }
}
