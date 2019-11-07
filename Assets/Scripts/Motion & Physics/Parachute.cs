using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    public GameObject parachute;
    public Sprite blueParachute;
    public Sprite greenParachute;
    public Sprite redParachute;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetLiftForce(float n)
    {
        parachute.GetComponent<Rigidbody2D>().drag = n;
        ScaleParachute(n);
    }

    private void ScaleParachute(float n)
    {
        float parachuteScaleX;
        float parachuteScaleY;
        float parachutePosition;
        Sprite parachuteColor;
        if ( n < 59f)
        {
            parachuteScaleX = 0.3f;
            parachuteScaleY = 0.2f;
            parachutePosition = parachute.transform.position.y - 0.38f;
            parachuteColor = greenParachute;
        }
        else if (59f < n && n < 140)
        {
            parachuteScaleX = 0.5f;
            parachuteScaleY = 0.4f;
            parachutePosition = parachute.transform.position.y - 0.1f;
            parachuteColor = blueParachute;
        }
        else
        {
            parachuteScaleX = 0.7f;
            parachuteScaleY = 0.6f;
            parachutePosition = parachute.transform.position.y + 0.1f;
            parachuteColor = redParachute;
        }
        parachute.transform.localScale = new Vector3(parachuteScaleX, parachuteScaleY);
        parachute.transform.position = new Vector3(parachute.transform.position.x, parachutePosition, 1);
        parachute.GetComponent<SpriteRenderer>().sprite = parachuteColor;
    }
}
