using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdMobManager : MonoBehaviour
{
	public static AdMobManager instance;
	
	public RewardedAd rewardedAd;
	public InterstitialAd interstitial;


	#region Unity Function
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
    {
		MobileAds.Initialize(initStatus => { });



		
		RequestRewarded();
		RequestInterstitial();
	}
	#endregion

	#region AdMob Functions
	//-----------------------------------------------------RewardedAd
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
	}

	public void HandleUserEarnedReward(object sender, Reward args)  // --------------------------------------------aqui o player recebe o prêmio, tem que passar pras moedas
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print(
			"HandleRewardedAdRewarded event received for "
						+ amount.ToString() + " " + type);
	}


	//-------------------------------------------------InterstitialAd

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
							+ args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		interstitial.Destroy();
		RequestInterstitial();
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}

	#endregion

	#region Private Functions
	private void RequestRewarded()
	{
		string adUnitId;
#if UNITY_ANDROID
		adUnitId = "ca-app-pub-3940256099942544/5224354917"; //Lembrar de mudar o ID
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

		this.rewardedAd = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
	}
	private void RequestInterstitial()
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //Lembrar de mudar o ID
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}
	#endregion

	#region Public Functions

	public void UserChoseToWatchAd() // ---------------------------------------chamar esse pra quando o player escolher ver o Ad
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
			Debug.Log("Ad is being shown");
		}
	}


	public void ShowInterstitialAd() // ----------------------------------------------------chamar esse em momentos que for pra rodar um Ad
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
	}

	#endregion
}
