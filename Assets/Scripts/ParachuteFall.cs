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
        fallingObj.velocity = new Vector2(0, dropSpeed);
        transform.Rotate(0f, 0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //FallDown();
        DragObject();
    }

    private void DragObject()
    {
        if (Math.Round(transform.rotation.z, 2) >= 0.18)
        {
            transform.Rotate(0f, 0f, -0.1f);
        }
        else if (Math.Round(transform.rotation.z, 2) <= -0.20)
        {
            transform.Rotate(0f, 0f, 0.1f);
        }
    }

    //private void FallDown()
    //{
    //    if (transform.position.x > 2)
    //    {
    //        fallingObj.velocity = new Vector2(-0.4f, dropSpeed);
    //    }
    //    else if (transform.position.y < -3)
    //    {
    //        fallingObj.velocity = new Vector2(0.4f, dropSpeed);
    //    }
    //}
}
