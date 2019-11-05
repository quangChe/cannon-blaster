using UnityEngine;
using System.Collections;
using TMPro;
//using DG.Tweening;

public class Tweens : MonoBehaviour {

	Vector3 startPos;
	public  Tweenbuttons buttonTween;
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

			transform.Translate(20,0,0);
			iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));

			break;

		case Tweenbuttons.slideFromLeft:
			 
			transform.Translate(-20,0,0);
			iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));


			break;


		case Tweenbuttons.slideFromTop:
			transform.Translate(0,40,0);
			iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",2.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));
			


			break;
		case Tweenbuttons.slideFromBottom:
			transform.Translate(0,-40,0);
			iTween.MoveTo(gameObject,iTween.Hash("position",startPos,"time",1.0,"isLocal",true,"easetype",iTween.EaseType.easeInOutBack));
			
			
			
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
