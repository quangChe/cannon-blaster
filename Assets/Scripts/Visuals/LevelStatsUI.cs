using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStatsUI : MonoBehaviour
{
    public LevelDataController levelData;
    public GameObject star1, star2, star3, hitRate;
    public AudioClip gainStar;

    private float hitPoint = 0f;
    private Dictionary<string, bool> acquired = new Dictionary<string, bool>()
        {
            {"star1", false},
            {"star2", false},
            {"star3", false}
        };

    private void Start()
    {
        SetHitPoints();
    }

    void Update()
    {
        if (levelData.successRate[0] > hitPoint)
        {
            hitPoint++;
            SetHitPoints();
        }

        CheckStars();
    }

    private void CheckStars()
    {
        float percent = levelData.successPercent;
        if (!acquired["star1"] && percent >= 30f && percent < 60f)
        {
            Debug.Log(percent);

            StartCoroutine(AnimateAndAdd(star1));
        }
        else if (!acquired["star2"] && percent >= 60f && percent < 95f)
        {
            Debug.Log(percent);

            StartCoroutine(AnimateAndAdd(star2));
        }
        else if (!acquired["star3"] && percent >= 95f)
        {
            Debug.Log(percent);

            StartCoroutine(AnimateAndAdd(star3));
        }
    }

    private IEnumerator AnimateAndAdd(GameObject star)
    {
        while (star.transform.localScale.x <= 1.2f)
        {
            star.transform.localScale = new Vector3(star.transform.localScale.x + 0.02f, star.transform.localScale.y + 0.02f);
            yield return null;
        }
        star.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/fill_star");
        AudioSource.PlayClipAtPoint(gainStar, Camera.main.transform.position, 1f);
        while (star.transform.localScale.x >= 0.9f)
        {
            star.transform.localScale = new Vector3(star.transform.localScale.x - 0.02f, star.transform.localScale.y - 0.02f);
            yield return null;
        }

    }

    private void SetHitPoints()
    {
        string newHitRate = String.Format("{0}/{1}", hitPoint, levelData.successRate[1]);
        hitRate.GetComponent<TextMeshProUGUI>().SetText(newHitRate);
    }
}
