using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Points")]
public class Points : ScriptableObject
{
	public int Value;

	public GameObject feedbackCanvas;

	public float waitTimeToLetAddAgain;

	[Header ("Debug only")]
	public float lastTimeThisRan = 0;

	public void Add(int points)
	{
		//if (stopAdding()) return;
        Value += points;
        //PlayerPrefsManager.Instance.LoadPlayerPref("Coins");
        //PlayerPrefs.SetFloat(this.name, Value);
        PlayerPrefsManager.Instance.SavePlayerPrefs("Coins", Value);
		Debug.Log("Setting " + this.name + " as " + Value + " on PlayerPrefs");
        if (MoedaFeedbackLerp.instance == null)
        {
            Instantiate(feedbackCanvas);
            MoedaFeedbackLerp.instance.MoedaCanvasIsActive(true);
        }
        else
        {
            MoedaFeedbackLerp.instance.MoedaCanvasIsActive(true); 
        }
		lastTimeThisRan = Time.time;
	}

	
	private bool stopAdding()
	{
		return Time.time - waitTimeToLetAddAgain <= lastTimeThisRan;
	}
}
