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
    Wind wind;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        wind = FindObjectOfType<Wind>();
    }

    // Update is called once per frame
    void Update()
    {
        TravelParabolicTrajectory();
    }

    private void TravelParabolicTrajectory()
    {
        Animation += Time.deltaTime;
        Animation = Animation % 5;
        float previousHeight = transform.position.y;
        transform.position = ParabolaFormula(startPos, new Vector2(10f, 0), 4f, Animation / 5f);
        float newHeight = transform.position.y;

        if (previousHeight > newHeight && transform.position.y < 5f)
        {
            OpenParachute(transform.position, transform.rotation);
        }
    }

    private void OpenParachute(Vector3 p, Quaternion r)
    {
        wind.SetWind(parachuteLiftForce);
        Destroy(gameObject);
        GameObject parachutedBall = Instantiate(parachuteBall, p, r);
        parachutedBall.GetComponent<Parachute>().SetLiftForce(parachuteLiftForce);
    }


    private Vector2 ParabolaFormula(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector2.Lerp(start, end, t);
        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}
