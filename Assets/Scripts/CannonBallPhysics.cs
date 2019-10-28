using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallPhysics : MonoBehaviour
{
    System.Random random = new System.Random();

    public GameObject parachuteBall;
    BallConfigurations ballConfigs;

    float parachuteLiftForce;
    protected float ParabolicPath;

    Vector2 startPos;
    float endPositionX;

    Wind wind;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        endPositionX = random.Next(5, 18);
        ballConfigs = GetComponent<BallConfigurations>();
        parachuteLiftForce = ballConfigs.GetParachuteLift();
        wind = FindObjectOfType<Wind>();
    }

    // Update is called once per frame
    void Update()
    {
        TravelParabolicTrajectory();
    }

    private void TravelParabolicTrajectory()
    {
        ParabolicPath += Time.deltaTime;
        ParabolicPath = ParabolicPath % 5;
        float previousHeight = transform.position.y;
        transform.position = ParabolaFormula(startPos, new Vector2(endPositionX, 0), 4f, ParabolicPath / 5f);
        float newHeight = transform.position.y;

        if (previousHeight > newHeight && transform.position.y < 5f)
        {
            OpenParachute(transform.position, transform.rotation);
        }
    }

    private void OpenParachute(Vector3 p, Quaternion r)
    {
        wind.SetWind();
        Sprite exerciseSprite = ballConfigs.GetExerciseSprite();
        Destroy(gameObject);
        GameObject parachutedBall = Instantiate(parachuteBall, p, r);
        parachutedBall.GetComponent<Parachute>().SetLiftForce(parachuteLiftForce);
        parachutedBall.GetComponent<RenderExercise>().SetExerciseSprite(exerciseSprite);
    }


    private Vector2 ParabolaFormula(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector2.Lerp(start, end, t);
        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}
