using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] GameObject cannonBall;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(
                cannonBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                Quaternion.identity
            );
        }
    }
}
