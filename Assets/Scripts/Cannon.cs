using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject cannonball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newCannonball = Instantiate(cannonball, transform.position, Quaternion.identity);
            newCannonball.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }
    }
}
