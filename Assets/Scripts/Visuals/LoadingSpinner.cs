using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSpinner : MonoBehaviour {
    public RectTransform mainIcon;
    public float timeStep = 0.05f;
    public float oneStepAngle = 36;

    float startTime;

	void Start () {
        startTime = Time.unscaledTime;
	}
	
	void Update () {
        if(Time.unscaledTime - startTime >= timeStep)
        {
            Vector3 iconAngle = mainIcon.localEulerAngles;
            iconAngle.z += oneStepAngle;
            mainIcon.localEulerAngles = iconAngle;
            startTime = Time.unscaledTime;
        }
	}
}
