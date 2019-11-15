using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewExercise : MonoBehaviour
{
    public GameObject previewImage;

    [Header("Referenced Scripts")]
    public LevelDataController levelData;
    public SpriteDictionary gameSprites;


    private BallObject[] ballData;

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
            previewObject.GetComponent<Image>().sprite = gameSprites.GetSprite(ballData[i].exercise);
            objDimensions.localScale = new Vector3(1, 1, 0);
            objDimensions.localPosition = new Vector2(0, positionY);
            positionY += 165f;
        }
    }

    IEnumerator SmoothScroll(RectTransform obj, Vector3 start, Vector3 end, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            obj.localPosition = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void UpdatePreview()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 start = rt.localPosition;
        Vector3 end = new Vector3(0, (rt.localPosition.y - 165f), 0);
        StartCoroutine(SmoothScroll(rt, start, end, 0.5f));
    }
    
}
