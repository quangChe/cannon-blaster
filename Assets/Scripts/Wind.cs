using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] GameObject windA;
    [SerializeField] GameObject windB;
    [SerializeField] GameObject windC;

    AreaEffector2D windConfigA;
    AreaEffector2D windConfigB;
    AreaEffector2D windConfigC;

    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        windConfigA = windA.GetComponent<AreaEffector2D>();
        windConfigB = windB.GetComponent<AreaEffector2D>();
        windConfigC = windC.GetComponent<AreaEffector2D>();
    }

    public void SetWind(float m)
    {
        float direction = random.Next(0, 2) * 2 - 1;
        float velocity = direction * (m / 2);
        windConfigA.forceMagnitude = velocity;
        windConfigB.forceMagnitude = -velocity;
        windConfigC.forceMagnitude = velocity;

    }
}
