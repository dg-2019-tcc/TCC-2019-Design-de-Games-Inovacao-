using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSetup : MonoBehaviour
{
	public BoolVariable buildPC;

	public Points moedas;

	public TMP_Text textoDebug;


    void Start()
    {
		LoadCoins();
		SetVariables();
		CheckWhichBuild();
		textoDebug.text = "Plataforma atual: " + plataforma + "\n Build pra pc: " + buildPC.Value.ToString();
	}

	void LoadCoins()
	{
		moedas.Value = PlayerPrefs.GetFloat("Moedas");
	}

	private void SetVariables()
	{
		buildPC = Resources.Load<BoolVariable>("BuildPC");
	}


	private string plataforma;

	private void CheckWhichBuild()
	{
#if UNITY_EDITOR
		Debug.Log("Unity Editor");
		buildPC.Value = true;
		plataforma = "Unity Editor";
#elif UNITY_STANDALONE_WIN
		Debug.Log("Stand Alone Windows Build");
		buildPC.Value = true;
		plataforma = "Windows";
#elif UNITY_ANDROID
		Debug.Log("Android Build");
		buildPC.Value = false;
		plataforma = "Android";
#else
		Debug.Log("Unpredicted Platform, defaulting to android build");
		buildPC.Value = false;
		plataforma = "Não Especificada";
#endif
	}

}
