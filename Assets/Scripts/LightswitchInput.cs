using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightswitchInput : MonoBehaviour
{
    public GameObject explosion;
    public GameObject gameBall;

    public void SetGameObjects(GameObject g, GameObject e)
    {
        gameBall = g;
        explosion = e;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Rigidbody2D ball = gameBall.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();
            Instantiate(explosion, new Vector2(ball.transform.position.x, ball.transform.position.y), Quaternion.identity);
            Destroy(gameBall);
        }
    }
}
