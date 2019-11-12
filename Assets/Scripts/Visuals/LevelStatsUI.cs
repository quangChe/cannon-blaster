using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStatsUI : MonoBehaviour
{
    public LevelDataController levelData;
    public GameObject star1, star2, star3, hitRate;

    private Dictionary<string, bool> acquired = new Dictionary<string, bool>();
    private float hitPoint = 0f;

    private void Awake()
    {
        SetHitPoints();
        for (int i = 0; i < 3; i++) { acquired.Add("star" + i, false); }
    }

    void Update()
    {
        if (levelData.successRate[0] > hitPoint)
        {
            hitPoint++;
            SetHitPoints();
        }

    }

    private void SetHitPoints()
    {
        string newHitRate = String.Format("{0}/{1}", hitPoint, levelData.successRate[1]);
        hitRate.GetComponent<TextMeshProUGUI>().SetText(newHitRate);
    }
}
