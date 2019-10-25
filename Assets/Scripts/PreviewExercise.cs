using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewExercise : MonoBehaviour
{
    public GameObject previewImage;

    LevelDataController levelControl;
    ExerciseSprites exerciseSprites;
    BallData[] ballData;

    
    // Start is called before the first frame update
    void Start()
    {
        levelControl = FindObjectOfType<LevelDataController>();
        exerciseSprites = FindObjectOfType<ExerciseSprites>();
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

    

    public void UpdatePreview()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(0, (rt.localPosition.y - 165f), 0);
    }
}
