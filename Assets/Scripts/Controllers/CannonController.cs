using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shotBall;
    public GameObject explosion;
    public AudioClip projectile;

    public GameObject FireProjectile(BallObject d, Sprite exercise)
    {
        GameObject cannonBall = Instantiate(
                shotBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                transform.rotation
            );
        AudioSource.PlayClipAtPoint(projectile, Camera.main.transform.position, 0.5f);
        BallConfigurations ballConfigs = cannonBall.GetComponent<BallConfigurations>();
        ballConfigs.data = d;
        ballConfigs.SetExercise(exercise);
        ballConfigs.SetParachuteLift(d.fallDelay);
        return cannonBall;
    }
}


