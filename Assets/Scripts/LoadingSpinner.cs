using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSpinner : MonoBehaviour {
    public RectTransform _mainIcon;
    public float _timeStep = 0.05f;
    public float _oneStepAngle = 36;

    float _startTime;
	// Use this for initialization
	void Start () {
        _startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time - _startTime >= _timeStep)
        {
            Vector3 iconAngle = _mainIcon.localEulerAngles;
            iconAngle.z += _oneStepAngle;

            _mainIcon.localEulerAngles = iconAngle;

            _startTime = Time.time;
        }
	}
}
