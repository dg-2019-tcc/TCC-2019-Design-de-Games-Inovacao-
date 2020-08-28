using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSetup : MonoBehaviour
{
	public BoolVariable buildPC;

	public Points moedas;

	public TMP_Text textoDebug;

	public TranslateVariable language;

	public BoolVariableMatrix blocked;
	public BoolVariableMatrix baseBlocked;

    void Start()
    {
		LoadCoins();
		SetVariables();
		CheckWhichBuild();
		CustomizationLocked();

		textoDebug.text = "Plataforma atual: " + plataforma + "\n Build pra pc: " + buildPC.Value.ToString();
	}

	void LoadCoins()
	{
		moedas.Value = PlayerPrefs.GetFloat("Moedas");
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
