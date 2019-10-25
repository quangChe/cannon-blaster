using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewExercise : MonoBehaviour
{
    public GameObject previewImage;

    LevelDataController levelControl;
    BallData[] ballData;

    Dictionary<string, Sprite> imageDict = new Dictionary<string, Sprite>();
    
    // Start is called before the first frame update
    void Start()
    {
        levelControl = FindObjectOfType<LevelDataController>();
        ballData = levelControl.GetBalls();
        BuildImageDictionary();
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
            previewObject.GetComponent<Image>().sprite = imageDict[ballData[i].exercise];
            objDimensions.localPosition = new Vector3(0, positionY, 0);
            objDimensions.localScale = new Vector3(1, 1, 0);
            positionY += 165f;
        }
    }

    private void BuildImageDictionary()
    {
        imageDict.Add("LS", Resources.Load<Sprite>("lightswitch"));
        imageDict.Add("DK", Resources.Load<Sprite>("doorknob"));
        imageDict.Add("ZP", Resources.Load<Sprite>("zipper"));
        imageDict.Add("CP", Resources.Load<Sprite>("cups"));
    }

    public void UpdatePreview()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(0, (rt.localPosition.y - 165f), 0);
    }
}
