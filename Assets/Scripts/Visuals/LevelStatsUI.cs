using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        int starsWon = levelData.StarsWon();

        if (!acquired["star1"] && starsWon == 1)
        {
            acquired["star1"] = true;
            StartCoroutine(AnimateAndAdd(star1));
        }
        if (!acquired["star2"] && starsWon == 2)
        {
            acquired["star2"] = true;
            StartCoroutine(AnimateAndAdd(star2));
        }
        if (!acquired["star3"] && starsWon == 3)
        {
            acquired["star3"] = true;
            StartCoroutine(AnimateAndAdd(star3));
        }
    }

    private IEnumerator AnimateAndAdd(GameObject star)
    {
        //while (star.transform.localScale.x <= 1.2f)
        //{
        //    star.transform.localScale = new Vector3(star.transform.localScale.x + 0.06f, star.transform.localScale.y + 0.04f);
        //    yield return null;
        //}

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / 0.2f;
            star.transform.localScale = Vector3.Lerp(star.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        star.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/fill_star");
        AudioSource.PlayClipAtPoint(gainStar, Camera.main.transform.position, 1f);
        //while (star.transform.localScale.x >= 0.9f)
        //{
        //    star.transform.localScale = new Vector3(star.transform.localScale.x - 0.06f, star.transform.localScale.y - 0.04f);
        //    yield return null;
        //}
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / 0.2f;
            star.transform.localScale = Vector3.Lerp(star.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

    }

    private void SetHitPoints()
    {
        string newHitRate = String.Format("{0}/{1}", hitPoint, levelData.successRate[1]);
        hitRate.GetComponent<TextMeshProUGUI>().SetText(newHitRate);
    }
}
