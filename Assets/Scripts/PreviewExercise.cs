using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewExercise : MonoBehaviour
{
    public GameObject previewImage;

    LevelDataController levelControl;
    ExerciseSpriteDictionary exerciseSprites;
    BallData[] ballData;

    
    // Start is called before the first frame update
    void Start()
    {
        levelControl = FindObjectOfType<LevelDataController>();
        exerciseSprites = FindObjectOfType<ExerciseSpriteDictionary>();
        ballData = levelControl.GetBalls();
        RenderPreviewObjects();
    }

    private void RenderPreviewObjects()
    {
        float positionY = -170f;
        for (int i = 0; i < ballData.Length; i++)
        {
            GameObject previewObject = Instantiate(previewImage, new Vector3(0, 0, 0), Quaternion.identity);
            RectTransform objDimensions = previewObject.GetComponent<RectTransform>();
            objDimensions.SetParent(gameObject.transform);
            previewObject.GetComponent<Image>().sprite = exerciseSprites.GetSprite(ballData[i].exercise);
            objDimensions.localPosition = new Vector3(0, positionY, 0);
            objDimensions.localScale = new Vector3(1, 1, 0);
            positionY += 165f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //StartCoroutine(UpdatePreview());
            UpdatePreview();
        }
    }


    //public IEnumerator UpdatePreview()
    public void UpdatePreview()
    {
        //float scrollTime = 5f;
        //float currentTime = 0f;
        //float normalizedValue;
        //float scrollIncrementY = 0f;
        //int counter = 1;
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y - 165f, 0);
        //while (currentTime <= scrollTime)
        //{
        //    Debug.Log(counter);
        //    counter++;
        //    currentTime += Time.deltaTime;
        //    normalizedValue = currentTime / scrollTime;
        //    rt.localPosition = Vector3.Lerp(rt.localPosition, new Vector3(rt.localPosition.x, (rt.localPosition.y - 165f), 0), normalizedValue);
        //    yield return null;
        //}
    }
}
