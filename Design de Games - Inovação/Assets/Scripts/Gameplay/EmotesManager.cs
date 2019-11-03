using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class EmotesManager : MonoBehaviour
{

	public GameObject pause;
	public GameObject[] emote;

	public void Sticker(int index)
	{
		pause.SetActive(false);
		emote[index].SetActive(true);

	}

}
