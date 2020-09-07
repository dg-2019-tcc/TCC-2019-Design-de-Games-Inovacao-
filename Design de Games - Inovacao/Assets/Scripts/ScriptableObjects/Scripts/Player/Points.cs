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

    public void SetCoins()
    {
        Value = PlayerPrefs.GetInt("Coins");
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", Value);
    }

	public void Add(int points)
	{
        Value += points;
        SaveCoins();
        DisplayCoins();
		lastTimeThisRan = Time.time;

        Debug.Log("Setting " + this.name + " as " + Value + " on PlayerPrefs");
    }
    public void DisplayCoins()
    {
        if (MoedaFeedbackLerp.instance == null)
        {
            Instantiate(feedbackCanvas);
        }

        MoedaFeedbackLerp.instance.MoedaCanvasIsActive(true);
    }

    public void JustShowCoins()
    {
        SetCoins();
        if (MoedaFeedbackLerp.instance == null)
        {
            Instantiate(feedbackCanvas);
        }

        MoedaFeedbackLerp.instance.MoedaCanvasIsActive(true);
    }
	
	private bool stopAdding()
	{
		return Time.time - waitTimeToLetAddAgain <= lastTimeThisRan;
	}
}
