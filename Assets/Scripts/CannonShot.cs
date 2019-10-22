using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField] GameObject parachuteBall;
    [SerializeField] float parachuteLiftForce;

    protected float Animation;
    Vector2 startPos;

    System.Random random = new System.Random();
    
    WindTop wind1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        wind1 = FindObjectOfType<WindTop>();
    }

    // Update is called once per frame
    void Update()
    {
        TravelParabolicTrajectory();
    }

    private void RenderBallWithParachute(Vector3 p, Quaternion r)
    {
        ConfigureWindSpeeds(parachuteLiftForce);
        Destroy(gameObject);
        GameObject parachutedBall = Instantiate(parachuteBall, p, r);
        parachutedBall.GetComponent<Parachute>().SetLiftForce(parachuteLiftForce);
    }

    private void ConfigureWindSpeeds(float speed)
    {
        
        float direction = random.Next(0, 2) * 2 - 1;
        float velocity = direction * (speed);
        wind1.SetWind(velocity);
    }


    private void TravelParabolicTrajectory()
    {
        Animation += Time.deltaTime;
        Animation = Animation % 5;
        float previousHeight = transform.position.y;
        transform.position = ParabolaFormula(startPos, new Vector2(10f, 0), 4f, Animation / 2f);
        float newHeight = transform.position.y;
        if (previousHeight > newHeight && transform.position.y < 5f)
        {
            RenderBallWithParachute(transform.position, transform.rotation);
        }
    }


    private Vector2 ParabolaFormula(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector2.Lerp(start, end, t);
        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}
