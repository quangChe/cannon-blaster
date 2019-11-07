using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeController : MonoBehaviour
{
    public BluetoothManager bt;
    public Camera uiCam;
    public GameObject playButton, title, loadingSpinner, loadingPrompt;
    public Renderer playButtonRender;
    public Texture[] playButtonTexture;

    private string hitObjName;

    void Start()
    {
        Time.timeScale = 1;
        bt = FindObjectOfType<BluetoothManager>();
        IsLoading(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bt.connected)
        {
            IsLoading(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AnimateButton();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(Submit());
        }

    }

    private void IsLoading(bool loading)
    {
        if (loading)
        {
            title.SetActive(false);
            playButton.SetActive(false);
            loadingSpinner.SetActive(true);
            loadingPrompt.SetActive(true);
        }
        else
        {
            title.SetActive(true);
            playButton.SetActive(true);
            loadingSpinner.SetActive(false);
            loadingPrompt.SetActive(false);
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

        if (hitObjName == "Play Button")
        {
            playButtonRender.material.mainTexture = playButtonTexture[1];
        }
    }

    private IEnumerator Submit()
    {
        RaycastHit hitObject;
        Ray rayObj = uiCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayObj, out hitObject))
        {
            hitObjName = hitObject.collider.name;
        }

        if (hitObjName == "Play Button")
        {
            originalTextures();
            playButtonRender.material.mainTexture = playButtonTexture[0];
            iTween.MoveTo(playButton, new Vector3(-29, 0, 0), 2f);
            iTween.MoveTo(title, new Vector3(-29, 0, 0), 2f);
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("Levels");
        }
    }

    public void originalTextures()
    {
        playButtonRender.material.mainTexture = playButtonTexture[0];
    }
}
