using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    [SerializeField] GameObject parachute;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetLiftForce(float n)
    {
        parachute.GetComponent<Rigidbody2D>().drag = n;
    }
}
