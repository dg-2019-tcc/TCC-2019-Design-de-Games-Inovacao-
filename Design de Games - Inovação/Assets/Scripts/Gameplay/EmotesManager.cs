using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class EmotesManager : MonoBehaviour
{

	
	private GameObject stickers;


	private void OnEnable()
	{
		GetComponent<PressGesture>().Pressed += pressedhandler;
	//	GetComponent<ReleaseGesture>().Released += releasedHandler;
	}

	private void OnDisable()
	{
		GetComponent<PressGesture>().Pressed -= pressedhandler;
	//	GetComponent<ReleaseGesture>().Released -= releasedHandler;
	}



	private void pressedhandler(object sender, System.EventArgs e)
	{
		
			Debug.Log("tocou");
			stickers.SetActive(true);
		
	}

}
