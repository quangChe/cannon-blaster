using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D scrollObj;
    [SerializeField] float scrollSpeed = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        scrollObj = GetComponent<Rigidbody2D>();
        scrollObj.velocity = new Vector2(scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
