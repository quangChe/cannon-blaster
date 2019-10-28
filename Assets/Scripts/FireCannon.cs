using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] GameObject shotBall;


    public void NewBall(float fallDelay, Sprite exercise)
    {
        GameObject cannonBall = Instantiate(
                shotBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                transform.rotation
            );
        Debug.Log(cannonBall.transform.position);
        BallConfigurations ballConfigs = cannonBall.GetComponent<BallConfigurations>();
        ballConfigs.SetExercise(exercise);
        ballConfigs.SetParachuteLift(fallDelay);
    }
}
