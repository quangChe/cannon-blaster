using UnityEngine;
using System.Collections;
using TMPro;
//using DG.Tweening;

public class Tweens : MonoBehaviour {

	Vector3 startPos;
    public float timeDelay;
	public Tweenbuttons buttonTween;
	public enum Tweenbuttons{
	 
		inGameEndScroller,
		slideFromLeft,
		slideFromRight,
		slideFromTop,
		slideFromBottom,
	}

	void OnEnable () {


		startPos  = transform.localPosition;
		switch(buttonTween)
		{

		case Tweenbuttons.slideFromRight:

			transform.Translate(50,0,0);
			iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));

			break;

		case Tweenbuttons.slideFromLeft:
			 
			transform.Translate(-50,0,0);
			iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));


			break;


		case Tweenbuttons.slideFromTop:
			GetComponent<RectTransform>().transform.Translate(0, 500, -1);
            float t = (timeDelay > 0f) ? timeDelay : 1f;
            iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", timeDelay, "isLocal", true, "easetype", iTween.EaseType.easeInQuad));



                break;
		case Tweenbuttons.slideFromBottom:
			transform.Translate(0, -40, 0);
            iTween.MoveTo(gameObject, new Vector3(0, -1.5f, 0), 1.5f);
			//iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.linear));
			
			
			
			break;

		case Tweenbuttons.inGameEndScroller:
			iTween.MoveTo(gameObject,iTween.Hash("position",Vector3.zero,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));
			break;

		}

            
	
	}
	void OnDisable()
	{
		transform.localPosition = startPos;
	}
	
	 
}
