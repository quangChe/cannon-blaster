using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateText : MonoBehaviour
{
    public string[] transitionStates;
    public float timeDelay = 1f;

    private void Start()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        int stateCounter = 0;
        while(true)
        {
            GetComponent<TextMeshProUGUI>().SetText(transitionStates[stateCounter]);
            yield return new WaitForSecondsRealtime(timeDelay);
            stateCounter = (stateCounter == (transitionStates.Length - 1))
                ? 0 : stateCounter + 1;
        }
    }
}
