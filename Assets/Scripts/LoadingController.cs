using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public Bluetooth bt;
    public GameObject playButton;

    void Start()
    {
        
    }

    void Update()
    {
        if (bt._connected)
        {
            GameObject button = Instantiate(playButton, transform.position, Quaternion.identity);
            RectTransform objDimensions = button.GetComponent<RectTransform>();
            objDimensions.SetParent(gameObject.transform.parent);
            objDimensions.anchoredPosition = new Vector2(0, transform.position.y - 100f);
            objDimensions.localScale = new Vector3(300, 180, 0);
            Destroy(gameObject);
        }
    }
}
