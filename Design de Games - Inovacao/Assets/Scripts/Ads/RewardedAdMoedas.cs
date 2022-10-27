using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardedAdMoedas : MonoBehaviour
{

	private TMP_Text text;
	private Button button;


    private void Start()
    {
		button = GetComponent<Button>();
		text = GetComponentInChildren<TMP_Text>();
    }
	
    private void Update()
    {

		button.interactable = AdMobManager.instance.rewardedAd.IsLoaded();
	}


	public void ShowAd()
	{
		AdMobManager.instance.UserChoseToWatchAd();
	}
}
