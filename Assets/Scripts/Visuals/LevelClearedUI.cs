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
        hiddenDetails.SetActive(false);
        StartCoroutine(AnimateAllStars());
    }

    private IEnumerator AnimateAllStars()
    {
        StartCoroutine(AnimateStars(star1));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(AnimateStars(star2));
        yield return new WaitForSeconds(.7f);
        //PraisePlayer();
        StartCoroutine(AnimatePraiseText("Yay. Wow."));
        yield return new WaitForSeconds(.7f);
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
            star.transform.localScale = new Vector3(starTransform.localScale.x - 0.04f, starTransform.localScale.y - 0.04f);
            yield return null;
        }

    }

    private void PraisePlayer()
    {
        float percent = levelData.successPercent;
        if (percent >= 30f && percent < 60f)
        {
            //praiseText.GetComponent<TextMeshProUGUI>.SetText("")
        }
        else if (percent >= 60f && percent < 95f)
        {

        } else if (percent >= 95f)
        {

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

   

}
