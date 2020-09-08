﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSetup : MonoBehaviour
{
	public BoolVariable buildPC;
    public BoolVariable buildMobile;
	public BoolVariable buildFinal;
	public BoolVariable resetaPlayerPrefs;
	public BoolVariable pularModoHistoria;

	public Points moedas;

	public TMP_Text textoDebug;

	public TranslateVariable language;

	public BoolVariableMatrix blocked;
	public BoolVariableMatrix baseBlocked;

    public bool isFirstTime;

    private void Awake()
    {
        if(resetaPlayerPrefs.Value == true)
        {
            pularModoHistoria.Value = false;
            PlayerPrefs.DeleteAll();
            resetaPlayerPrefs.Value = false;
        }
        if(PlayerPrefs.GetInt("IsFirstTime") == 0) { isFirstTime = true; }
        else { isFirstTime = false; }
    }

    void Start()
    {
        SetVariables();
        CustomizationLocked();

#if UNITY_EDITOR
        if (buildMobile.Value == true)
        {
            buildPC.Value = false;
            GameManager.buildPC = false;
        }
        else
        {
            CheckWhichBuild();
        }
#else
        CheckWhichBuild();
#endif

        if (isFirstTime) { FirstTime(); }
        else { AlreadyPlayed(); }

		textoDebug.text = "Plataforma atual: " + plataforma + "\n Build pra pc: " + buildPC.Value.ToString();

        PlayerPrefs.SetInt("IsFirstTime", 1);
	}

    void FirstTime()
    {
        pularModoHistoria.Value = false;
        PlayerPrefsManager.Instance.DefaultCustom();
        PlayerPrefs.SetInt("Coins", 100);
        moedas.Value = PlayerPrefs.GetInt("Coins");
        Debug.Log("[Game Setup] FirstTime Coins =" + moedas.Value + "  " + PlayerPrefs.GetInt("Coins"));
    }

    void AlreadyPlayed()
    {
        LoadCoins();
        PlayerPrefsManager.Instance.LoadPlayerPref("All");
        if(PlayerPrefsManager.Instance.prefsVariables.levelIndex > 8) { GameManager.historiaMode = false; pularModoHistoria.Value = true; }
    }

	void LoadCoins()
	{
		moedas.Value = PlayerPrefs.GetInt("Coins");
        Debug.Log("[Game Setup] Load Coins =" + moedas.Value+ "  " + PlayerPrefs.GetInt("Coins"));
	}

	void LoadLanguage()
	{
		language.UpdateLanguage(PlayerPrefs.GetString("Language"));
		PlayerPrefs.SetString("Language", language.language);
	}

	private void SetVariables()
	{
		buildPC = Resources.Load<BoolVariable>("BuildPC");
	}

	private void CustomizationLocked()
	{
		if (!PlayerPrefs.HasKey("BlockedX"))
		{
			blocked.rows = baseBlocked.rows;
		}
		else
		{
			//pegar do playerprefs, mas o playerprefs n aceita array por hora
		}
	}


	private string plataforma;

	private void CheckWhichBuild()
	{
#if UNITY_EDITOR
		Debug.Log("Unity Editor");
		buildPC.Value = true;
        GameManager.buildPC = true;
		plataforma = "Unity Editor";
#elif UNITY_STANDALONE_WIN
		Debug.Log("Stand Alone Windows Build");
		buildPC.Value = true;
                GameManager.buildPC = true;
		plataforma = "Windows";
#elif UNITY_ANDROID
		Debug.Log("Android Build");
		buildPC.Value = false;
                GameManager.buildPC = false;
		plataforma = "Android";
#else
		Debug.Log("Unpredicted Platform, defaulting to android build");
		buildPC.Value = false;
		plataforma = "Não Especificada";
#endif
    }

}
