using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPath : MonoBehaviour
{
    [SerializeField] float shipVelocity = -0.3f;
    Rigidbody2D ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        ship.velocity = new Vector2(0, shipVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
