using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Points")]
public class Points : ScriptableObject
{
	public float Value;

	public GameObject feedbackCanvas;

	public float waitTimeToLetAddAgain;

	[Header ("Debug only")]
	public float lastTimeThisRan = 0;

	public void Add(float points)
	{
		if (stopAdding()) return;
		Value += points;
		PlayerPrefs.SetFloat(this.name, Value);
		Debug.Log("Setting " + this.name + " as " + Value + " on PlayerPrefs");
		Instantiate(feedbackCanvas);
		lastTimeThisRan = Time.time;
	}

	
	private bool stopAdding()
	{
		return Time.time - waitTimeToLetAddAgain <= lastTimeThisRan;
	}
}
