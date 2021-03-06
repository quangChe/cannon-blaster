﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallConfigurations : MonoBehaviour
{
    public GameObject ExerciseObject;
    public BallObject data;
    public Sprite targetExercise;

    float parachuteLiftForce;
    //float points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParachuteLift(float n)
    {
        parachuteLiftForce = n;
    }

    public float GetParachuteLift()
    {
        return parachuteLiftForce;
    }

    public void SetExercise(Sprite exercise)
    {
        targetExercise = exercise;
        ExerciseObject.GetComponent<SpriteRenderer>().sprite = exercise;
    }
}
