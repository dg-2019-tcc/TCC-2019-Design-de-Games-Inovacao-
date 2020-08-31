using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteAd : MonoBehaviour
{

	public void ShowAd()
	{
		AdMobManager.instance.UserChoseToWatchAd();
		Debug.Log("User chose to watch Ad");

	}
}
