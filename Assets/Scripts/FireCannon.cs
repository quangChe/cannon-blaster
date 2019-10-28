﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] GameObject shotBall;
    [SerializeField] Sprite exerciseSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BallConfigurations ballConfigs = InstantiateCannonBall();
            ballConfigs.SetParachuteLift(20);
        }
    }

    private BallConfigurations InstantiateCannonBall()
    {
        GameObject cannonBall = Instantiate(
                shotBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                Quaternion.identity
            );
        BallConfigurations ballConfigs = cannonBall.GetComponent<BallConfigurations>();

        // Need to set this
        ballConfigs.SetExercise(exerciseSprite);

        return ballConfigs;
    }
}
