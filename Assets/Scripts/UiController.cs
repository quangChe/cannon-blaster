using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{

    public Camera uiCam;
    public string hitObjName;
    public GameObject playButton, title;
    public Renderer playButtonRender;
    public Texture[] playButtonTexture;

    void Start()
    {
        Time.timeScale = 1;
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
    }

    private void AnimateButton()
    {
        RaycastHit hitObject;
        Ray rayObj = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayObj, out hitObject))
        {
            hitObjName = hitObject.collider.name;
        }
        playButtonRender.material.mainTexture = playButtonTexture[1];
    }

    private void Submit()
    {
        RaycastHit hitObject;
        Ray rayObj = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayObj, out hitObject))
        {
            hitObjName = hitObject.collider.name;
        }

        originalTextures();
        playButtonRender.material.mainTexture = playButtonTexture[0];
        //iTween.MoveTo(levels, new Vector3(0, 0, 0), 2f);
        iTween.MoveTo(gameObject, new Vector3(-29, 0, 0), 2f);
        SceneManager.LoadScene("Game");
    }

    public void originalTextures()
    {
        playButtonRender.material.mainTexture = playButtonTexture[0];
    }
}
