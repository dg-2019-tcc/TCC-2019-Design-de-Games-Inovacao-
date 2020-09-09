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

    // valor só para a anim da moeda no canvas
    public int coinValue;

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
        coinValue = points;
        SetCoins();
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

        MoedaFeedbackLerp.instance.MoedaCanvasIsActive(true, coinValue);
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
