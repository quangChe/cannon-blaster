using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTop : MonoBehaviour
{
    AreaEffector2D windConfig;

    // Start is called before the first frame update
    void Start()
    {
        windConfig = GetComponent<AreaEffector2D>();
    }

    public void SetWind(float a, float sm)
    {
        this.windConfig.forceAngle = a;
        this.windConfig.forceMagnitude = m;
    }
}
