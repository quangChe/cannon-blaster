using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField] GameObject parachuteBall;
    protected float Animation;
    bool openParachute = false;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (openParachute)
        {
            Destroy(gameObject);
            Instantiate(parachuteBall, transform.position, transform.rotation);
            Destroy(GetComponent<CannonShot>());
        }
        else
        {
            RunMovementPhysics();
        }
    }

    private void RunMovementPhysics()
    {
        Animation += Time.deltaTime;
        Animation = Animation % 5;
        float previousHeight = transform.position.y;
        transform.position = ParabolaFormula(startPos, new Vector2(2f, 0), 8f, Animation / 3f);
        transform.localScale = new Vector3(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f);
        Debug.Log(transform.localScale);
        float newHeight = transform.position.y;
        if (previousHeight > newHeight && transform.position.y < 4f)
        {
            openParachute = true;
        }
    }

    public static Vector2 ParabolaFormula(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}
