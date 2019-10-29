using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject shotBall;
    public GameObject explosion;

    public void NewBall(BallData d, Sprite exercise)
    {
        GameObject cannonBall = Instantiate(
                shotBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                transform.rotation
            );
        InputMapper.MountScript(cannonBall, explosion, d.exercise);
        BallConfigurations ballConfigs = cannonBall.GetComponent<BallConfigurations>();
        ballConfigs.data = d;
        ballConfigs.SetExercise(exercise);
        ballConfigs.SetParachuteLift(d.fallDelay);
    }
}


