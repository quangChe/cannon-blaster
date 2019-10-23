using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    [SerializeField] GameObject fastBall;
    [SerializeField] GameObject medBall;
    [SerializeField] GameObject slowBall;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(
                slowBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                Quaternion.identity
            );
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(
                medBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                Quaternion.identity
            );
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(
                fastBall,
                new Vector2(transform.position.x + 0.387f, transform.position.y + 1.748f),
                Quaternion.identity
            );
        }

    }
}
