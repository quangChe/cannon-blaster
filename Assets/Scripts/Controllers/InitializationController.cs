using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InitializationController : MonoBehaviour
{
    public SceneController scene;
    public GameObject popup;

    void Update()
    {
        if (BluetoothManager.Instance.connected)
        {
            StartCoroutine(StartHomeScene());
        }

    }

    private IEnumerator StartHomeScene()
    {
        iTween.MoveTo(popup, new Vector3(0, -10, 0), 2f);
        yield return new WaitForSeconds(0.5f);
        scene.GoToHome();
    }
}
