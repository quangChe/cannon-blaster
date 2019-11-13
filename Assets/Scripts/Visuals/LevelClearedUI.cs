using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClearedUI : MonoBehaviour
{
    public LevelDataController levelData;
    public GameObject star1, star2, star3;
    public GameObject praiseText;
    public AudioClip starSound, praiseSound;

    void Start()
    {
        StartCoroutine(AnimateAllStars());
    }

    private IEnumerator AnimateAllStars()
    {
        StartCoroutine(AnimateStars(star1));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(AnimateStars(star2));
        yield return new WaitForSeconds(.5f);
        //StartCoroutine(AnimatePraise());
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


}
