using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public bool faloComTV;
    public bool precisaFalar;

    public ItemLocatorOnScreen pointer;

    public BoolVariableArray acabou01;
    public BoolVariableArray aiGanhou;
    public BoolVariable demo;

    public GameObject[] falas;

    public GameFlowController flowController;

    void Start()
    {
        pointer = GetComponent<ItemLocatorOnScreen>();
        PlayerPrefsManager.Instance.LoadPlayerPref("FalasIndex");
        //Debug.Log("FalasIndex é: " + PlayerPrefsManager.Instance.prefsVariables.falasIndex);
        //Debug.Log("LevelIndex é: " + PlayerPrefsManager.Instance.prefsVariables.levelIndex);
        if (demo == null)
        {
            demo = Resources.Load<BoolVariable>("Demo");
        }

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        }

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }

		for (int i = 0; i < acabou01.Value.Length; i++)
		{
			CoisasAtivas(i, false);
		}

        if (GameManager.historiaMode)
        {
            Debug.Log(PlayerPrefsManager.Instance.prefsVariables.falasIndex);
            if (demo.Value == false)
            {
                if (PlayerPrefsManager.Instance.prefsVariables.falasIndex == 3 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 4 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 5 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 7)
                {
                    if (PlayerPrefsManager.Instance.prefsVariables.falasIndex == PlayerPrefsManager.Instance.prefsVariables.levelIndex)
                    {
                        CoisasAtivas(PlayerPrefsManager.Instance.prefsVariables.falasIndex, true);
                    }
                }
                else
                {
                    CoisasAtivas(PlayerPrefsManager.Instance.prefsVariables.falasIndex, true);
                }
            }
            else
            {
                if (PlayerPrefsManager.Instance.prefsVariables.falasIndex < 5)
                {
                    if (PlayerPrefsManager.Instance.prefsVariables.falasIndex == 3 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 4)
                    {
                        if (PlayerPrefsManager.Instance.prefsVariables.falasIndex == PlayerPrefsManager.Instance.prefsVariables.levelIndex)
                        {
                            CoisasAtivas(PlayerPrefsManager.Instance.prefsVariables.falasIndex, true);
                        }
                    }
                    else
                    {
                        CoisasAtivas(PlayerPrefsManager.Instance.prefsVariables.falasIndex, true);
                    }
                }
                else
                {
                    flowController.FlowHUB();
                }
            }

        }


		faloComTV = false;
    }

	
	public void CoisasAtivas(int index, bool ativar)
	{   
        if (!ativar)
        {
            falas[index].SetActive(ativar);
        }
        else
        {
            //GameManager.Instance.LoadGame();
            falas[PlayerPrefsManager.Instance.prefsVariables.falasIndex].SetActive(true);
        }
		pointer.enabled = ativar;
		precisaFalar = ativar;
        //FalouComTV();

    }

    public void FalouComTV()
    {
        PlayerPrefsManager.Instance.SavePlayerPrefs("FalasIndex", PlayerPrefsManager.Instance.prefsVariables.falasIndex + 1);
        Debug.Log("Falou com TV e levelIndex é: " + PlayerPrefsManager.Instance.prefsVariables.levelIndex);
        pointer.enabled = false;
        precisaFalar = false;
        faloComTV = true;
        if(PlayerPrefsManager.Instance.prefsVariables.falasIndex == 8)
        {
            PlayerPrefsManager.Instance.SavePlayerPrefs("LevelIndex", 8);
        }
        flowController.FlowHUB();
    }
}
