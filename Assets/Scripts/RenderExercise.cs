using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderExercise : MonoBehaviour
{
    [SerializeField] GameObject exerciseObject;

    public void SetExerciseSprite(Sprite ex)
    {
        exerciseObject.GetComponent<SpriteRenderer>().sprite = ex;
    }
}
