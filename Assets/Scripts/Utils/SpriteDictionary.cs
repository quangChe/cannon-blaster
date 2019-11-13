using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDictionary : MonoBehaviour
{
    Dictionary<string, Sprite> imageDict = new Dictionary<string, Sprite>();

    private GameManager Game = GameManager.Instance;

    void Awake()
    {
        BuildImageDictionary();
    }

    private void BuildImageDictionary()
    {
        foreach (string exercise in Game.AllExercises)
        {
            imageDict.Add(exercise, Resources.Load<Sprite>("sprites/" + exercise));
        }
    }

    public Sprite GetSprite(string key)
    {
        return imageDict[key];
    }
}
