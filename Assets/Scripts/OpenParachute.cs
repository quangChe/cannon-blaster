using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenParachute : MonoBehaviour
{
    Rigidbody2D fallingObj;
    [SerializeField] public float dropSpeed = -0.3f;
    float windSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fallingObj = GetComponent<Rigidbody2D>();
        fallingObj.velocity = new Vector2(0.2f, dropSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        FallDown();
       
    }

    private void FallDown()
    {
        if (transform.position.x > 2f)
        {
            fallingObj.velocity = new Vector2(-0.2f, dropSpeed);
        }
        else if (transform.position.x < -2f)
        {
            fallingObj.velocity = new Vector2(0.2f, dropSpeed);
        }
    }
}
