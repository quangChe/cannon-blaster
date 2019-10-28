using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewExercise : MonoBehaviour
{
    public GameObject previewImage;

    [Header("Referenced Scripts")]
    public LevelDataController levelData;
    public ExerciseSpriteDictionary exerciseSprites;

    BallData[] ballData;

    // Start is called before the first frame update
    void Start()
    {
        ballData = levelData.GetBalls();
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
            objDimensions.anchoredPosition = new Vector2(0, positionY);
            objDimensions.localScale = new Vector3(1, 1, 0);
            positionY += 165f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UpdatePreview();
        }
    }

    public void UpdatePreview()
    {
        RectTransform rt = GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, (rt.anchoredPosition.y - 165f));
    }
    
}
