using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardedAdMoedas : MonoBehaviour
{

	private TMP_Text text;
	private Button button;


    void Start()
    {
		button = GetComponent<Button>();
		text = GetComponentInChildren<TMP_Text>();
    }
	
    void Update()
    {

		button.interactable = AdMobManager.instance.rewardedAd.IsLoaded();
	}


	void ShowAd()
	{
		AdMobManager.instance.UserChoseToWatchAd();
	}
}
