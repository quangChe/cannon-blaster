using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSpriteDictionary : MonoBehaviour
{
    Dictionary<string, Sprite> imageDict = new Dictionary<string, Sprite>();

    void Start()
    {
        BuildImageDictionary();
    }

    private void BuildImageDictionary()
    {
        imageDict.Add("LS", Resources.Load<Sprite>("lightswitch"));
        imageDict.Add("DK", Resources.Load<Sprite>("doorknob"));
        imageDict.Add("ZP", Resources.Load<Sprite>("zipper"));
        imageDict.Add("CP", Resources.Load<Sprite>("cups"));
    }

    public Sprite GetSprite(string key)
    {
        return imageDict[key];
    }
}
