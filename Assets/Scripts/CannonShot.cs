using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField] GameObject parachuteBall;
    protected float Animation;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        TravelParabolicTrajectory();
    }

    private void RenderBallWithParachute(Vector3 p, Quaternion r)
    {
        //Debug.Log(p);
        //Debug.Log(r);
        Destroy(gameObject);
        Instantiate(parachuteBall, p, r);
        Destroy(GetComponent<CannonShot>());
    }


    private void TravelParabolicTrajectory()
    {
        Animation += Time.deltaTime;
        Animation = Animation % 5;
        float previousHeight = transform.position.y;
        transform.position = ParabolaFormula(startPos, new Vector2(10f, 0), 4f, Animation / 6f);
        float newHeight = transform.position.y;
        Debug.Log(transform.position);
        if (previousHeight > newHeight && transform.position.y < 5f)
        {
            Debug.Log("HELLO?");
            Debug.Log(transform.position);
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
