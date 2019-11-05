using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public Bluetooth bt;
    public Camera uiCam;
    public GameObject playButton, loadingSpinner;
    public Renderer playButtonRender;
    public Texture[] playButtonTexture;

    private string hitObjName;

    void Start()
    {
        Time.timeScale = 1;
        bt = FindObjectOfType<Bluetooth>();
        playButton.SetActive(false);
        loadingSpinner.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AnimateButton();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Submit();
        }

        if (bt._connected)
        {
            playButton.SetActive(true);
            loadingSpinner.SetActive(false);
        }
    }

    private void AnimateButton()
    {
        RaycastHit hitObject;
        Ray rayObj = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayObj, out hitObject))
        {
            hitObjName = hitObject.collider.name;
        }

        Debug.Log(hitObjName);

        if (hitObjName == "Play Button")
        {
            playButtonRender.material.mainTexture = playButtonTexture[1];
        }
    }

    private void Submit()
    {
        RaycastHit hitObject;
        Ray rayObj = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayObj, out hitObject))
        {
            hitObjName = hitObject.collider.name;
        }

        Debug.Log(hitObjName);

        if (hitObjName == "Play Button")
        {
            originalTextures();
            playButtonRender.material.mainTexture = playButtonTexture[0];
            //iTween.MoveTo(levels, new Vector3(0, 0, 0), 2f);
            iTween.MoveTo(gameObject, new Vector3(-29, 0, 0), 2f);
            SceneManager.LoadScene("Game");
        }
    }

    public void originalTextures()
    {
        playButtonRender.material.mainTexture = playButtonTexture[0];
    }
}
