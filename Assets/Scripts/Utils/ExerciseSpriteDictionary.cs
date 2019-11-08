using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSpriteDictionary : MonoBehaviour
{
    Dictionary<string, Sprite> imageDict = new Dictionary<string, Sprite>();

    void Awake()
    {
        BuildImageDictionary();
    }

    private void BuildImageDictionary()
    {
        imageDict.Add("LS", Resources.Load<Sprite>("sprites/lightswitch"));
        imageDict.Add("DK", Resources.Load<Sprite>("sprites/doorknob"));
        imageDict.Add("ZP", Resources.Load<Sprite>("sprites/zipper"));
        imageDict.Add("CP", Resources.Load<Sprite>("sprites/cups"));
    }

    public Sprite GetSprite(string key)
    {
        return imageDict[key];
    }
}
