using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteFall : MonoBehaviour
{
    Rigidbody2D fallingObj;
    [SerializeField] public float dropSpeed = -0.3f;
    float windSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fallingObj = GetComponent<Rigidbody2D>();
        fallingObj.velocity = new Vector2(0.4f, dropSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        DragObject();
        FallDown();
    }

    private void DragObject()
    {
        if (fallingObj.velocity.x > 0)
        {
            transform.Rotate(0f, 0f, 0.1f);
        } else if (fallingObj.velocity.x < 0)
        {
            transform.Rotate(0f, 0f, -0.1f);
        }
    }

    private void FallDown()
    {
        if (Math.Round(transform.rotation.z, 2) >= 0.20)
        {
            fallingObj.velocity = new Vector2(-0.4f, dropSpeed);
        }
        else if (Math.Round(transform.rotation.z, 2) <= -0.20)
        {
            fallingObj.velocity = new Vector2(0.4f, dropSpeed);
        }
    }
}
