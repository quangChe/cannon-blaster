using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWaves : MonoBehaviour
{
    [SerializeField] float waveVelocity;
    [SerializeField] float startPosition = 0.4f;
    [SerializeField] float endPosition = -0.7f;
    private Rigidbody2D waves;

    // Start is called before the first frame update
    void Start()
    {
        waves = GetComponent<Rigidbody2D>();
        waves.velocity = new Vector2(0, waveVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= startPosition || transform.position.y <= endPosition)
        {
            waveVelocity = waveVelocity * -1;
        }
        waves.velocity = new Vector2(0, waveVelocity);
    }
}