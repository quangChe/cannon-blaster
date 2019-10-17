using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D waterCollider;
    private float backgroundHorizontalLength;

    // Start is called before the first frame update
    void Start()
    {
        waterCollider = GetComponent<BoxCollider2D>();
        backgroundHorizontalLength = waterCollider.size.x - 0.83138f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -backgroundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(backgroundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
