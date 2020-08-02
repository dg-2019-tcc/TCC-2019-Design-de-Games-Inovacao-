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

    public GameObject[] falas;

    public GameFlowController flowController;

    void Start()
    {
        pointer = GetComponent<ItemLocatorOnScreen>();

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
            if (PlayerPrefsManager.Instance.prefsVariables.falasIndex == 2 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 4 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 5 || PlayerPrefsManager.Instance.prefsVariables.falasIndex == 7)
            {
                if (PlayerPrefsManager.Instance.prefsVariables.falasIndex == PlayerPrefsManager.Instance.prefsVariables.levelIndex)
                {
                    CoisasAtivas(GameManager.falaIndex, true);
                }
            }
            else
            {
                CoisasAtivas(GameManager.falaIndex, true);
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
