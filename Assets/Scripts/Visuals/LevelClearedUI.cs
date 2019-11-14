using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelClearedUI : MonoBehaviour
{
    public LevelDataController levelData;
    public GameObject star1, star2, star3;
    public GameObject praiseText;
    public GameObject hiddenDetails;
    public AudioClip starSound, praiseSound;

    void Start()
    {
        StartUIAnimation();
    }

    private void StartUIAnimation()
    {
        //float percent = levelData.successPercent;
        float percent = 95f;
        List<IEnumerator> starsToAnimate = new List<IEnumerator>();
        string praise = null;

        if (percent >= 30f && percent < 60f)
        {
            starsToAnimate.Add(AnimateStars(star1));
            praise = "Nice Job!";
        }
        else if (percent >= 60f && percent < 95f)
        {
            starsToAnimate.Add(AnimateStars(star1));
            starsToAnimate.Add(AnimateStars(star2));
            praise = "Excellent!";
        }

        else if (percent >= 95f)
        {
            starsToAnimate.Add(AnimateStars(star1));
            starsToAnimate.Add(AnimateStars(star2));
            starsToAnimate.Add(AnimateStars(star3));
            praise = "You're Amazing!";
        }

        StartCoroutine(AnimateUI(starsToAnimate, praise));
    }

    private IEnumerator AnimateUI(List<IEnumerator> starsToAnimate, string praise)
    {
        foreach (IEnumerator method in starsToAnimate)
        {
            StartCoroutine(method);
            yield return new WaitForSeconds(.5f);
        }
        StartCoroutine(AnimatePraiseText(praise));
        yield return new WaitForSeconds(.2f);
        StartCoroutine(AnimateHiddenDetails());
    }

 
    private IEnumerator AnimateStars(GameObject star)
    {
        RectTransform starTransform = star.GetComponent<RectTransform>();
        while (starTransform.localScale.x <= 1.6f)
        {
            starTransform.localScale = new Vector3(starTransform.localScale.x + 0.04f, starTransform.localScale.y + 0.04f);
            yield return null;
        }

        star.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/fill_star");
        AudioSource.PlayClipAtPoint(starSound, Camera.main.transform.position, 1f);

        while (starTransform.localScale.x >= 1.2f)
        {
            starTransform.localScale = new Vector3(starTransform.localScale.x - 0.04f, starTransform.localScale.y - 0.04f);
            yield return null;
        }

    }

    private IEnumerator AnimatePraiseText(string praise)
    {
        TextMeshProUGUI text = praiseText.GetComponent<TextMeshProUGUI>();
        text.fontSize = 1;
        text.SetText(praise);
        while(text.fontSize < 100f)
        {
            text.fontSize += 5f;
            yield return null;
        }

        AudioSource.PlayClipAtPoint(praiseSound, Camera.main.transform.position, 1f);
    }

    private IEnumerator AnimateHiddenDetails()
    {
        RectTransform detailTransform = hiddenDetails.GetComponent<RectTransform>();
        while (detailTransform.localScale.x < 1f)
        {
            detailTransform.localScale = new Vector3(
                detailTransform.localScale.x + 0.1f,
                detailTransform.localScale.y + 0.1f,
                detailTransform.localScale.z + 0.1f);
            yield return null;
        }
    }

}
