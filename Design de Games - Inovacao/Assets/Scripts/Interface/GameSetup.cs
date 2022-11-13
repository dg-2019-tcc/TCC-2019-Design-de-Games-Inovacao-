using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System.Diagnostics;

public class GameSetup : MonoBehaviour
{
	public BoolVariable buildPC;
    public BoolVariable buildMobile;
	public BoolVariable buildFinal;
	public BoolVariable resetaPlayerPrefs;
	public BoolVariable pularModoHistoria;
    public BoolVariable escolheFase;
    public FloatVariable faseEscolhida;

    public Points moedas;

	public TMP_Text textoDebug;

	public TranslateVariable language;

	public BoolVariableMatrix blocked;
	public BoolVariableMatrix baseBlocked;

    public bool isFirstTime;
    private int faseEsc;

    #region Unity Function

    private void Awake()
    {
        escolheFase = Resources.Load<BoolVariable>("EscolheFase");
        faseEscolhida = Resources.Load<FloatVariable>("FaseEscolhida");
		Debug.Log("faseescolhida é " + faseEscolhida.Value);

        if (resetaPlayerPrefs.Value == true)
        {
            escolheFase.Value = true;
            faseEscolhida.Value = 0;
            pularModoHistoria.Value = false;
            PlayerPrefs.DeleteAll();
            resetaPlayerPrefs.Value = false;
        }
        if (PlayerPrefs.GetInt("IsFirstTime") == 0) { isFirstTime = true; }
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
        if (escolheFase.Value == false)
        {
            if (isFirstTime) {
                Debug.Log("FIrstTime");
                FirstTime(); 
            }
            else {
                Debug.Log("AlreadyPlayed");
                AlreadyPlayed();
            }
        }
        else
        {
            EscolheFase();
        }

        textoDebug.text = "Plataforma atual: " + plataforma + "\n Build pra pc: " + buildPC.Value.ToString();

        PlayerPrefs.SetInt("IsFirstTime", 1);
    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions

    void FirstTime()
    {
        GameManager.historiaMode = true;
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
        CheckPointController.instance.LoadCheckPoint();
        if (CheckPointController.checkPointIndex >= 15) { GameManager.historiaMode = false; pularModoHistoria.Value = true; }
        else { GameManager.historiaMode = true; }
    }

    void EscolheFase()
    {
        if (pularModoHistoria.Value == false)
        {
            //PlayerPrefs.SetInt("Sequestrado", 0);
            PlayerPrefsManager.Instance.SavePlayerPrefs("Sequestrado", 0);
            faseEsc = Mathf.RoundToInt(faseEscolhida.Value);
            CheckPointController.instance.TalkCheckPoint(faseEsc);
            GameManager.historiaMode = true;
            //SaveGame(faseEsc, faseEsc);
            escolheFase.Value = false;
            faseEscolhida.Value = 0;
            Debug.Log("[GameManager] Escolheu Fase");
        }
        else
        {
            CheckPointController.instance.TalkCheckPoint(15);
            GameManager.historiaMode = false;
            //SaveGame(8,8);
            escolheFase.Value = false;
            faseEscolhida.Value = 0;
            pularModoHistoria.Value = false;
            Debug.Log("[GameManager] Escolheu Fase 2");
        }
    }

    void LoadCoins()
    {
        moedas.Value = PlayerPrefs.GetInt("Coins");
        Debug.Log("[Game Setup] Load Coins =" + moedas.Value + "  " + PlayerPrefs.GetInt("Coins"));
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
        if (!PlayerPrefs.HasKey("Blocked_0_0"))
        {
            blocked.rows = baseBlocked.rows;
            for (int x = 0; x < blocked.rows.Length; x++)
            {
                for (int y = 0; y < blocked.rows[x].row.Length; y++)
                {
                    PlayerPrefs.SetInt("Blocked_" + x + "_" + y, blocked.rows[x].row[y] ? 1 : 0);
                }
            }
        }
        else
        {
            GetCustomizationFromPlayerPrefs();

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

    private void GetCustomizationFromPlayerPrefs()
    {
        for (int x = 0; x < blocked.rows.Length; x++)
        {
            for (int y = 0; y < blocked.rows[x].row.Length; y++)
            {
                blocked.rows[x].row[y] = (PlayerPrefs.GetInt("Blocked_" + x + "_" + y) == 0 ? false : true);
            }
        }
    }

    #endregion

}
