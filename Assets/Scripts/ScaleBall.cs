﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.0025f, transform.localScale.y + 0.0025f);
        }
    }
}
