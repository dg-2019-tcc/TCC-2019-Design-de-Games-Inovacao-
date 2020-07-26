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
            if (GameManager.levelIndex == 2 || GameManager.levelIndex == 4 || GameManager.levelIndex == 5 || GameManager.levelIndex == 7)
            {
                if (GameManager.ganhouDoKley == 1)
                {
                    CoisasAtivas(GameManager.levelIndex, true);
                }
            }
            //Falas depois dos jogos, precisa ter ganhado para aparecer
            else
            {
                CoisasAtivas(GameManager.levelIndex, true);
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
            GameManager.Instance.LoadGame();
            falas[GameManager.levelIndex].SetActive(true);
        }
		pointer.enabled = ativar;
		precisaFalar = ativar;
        //FalouComTV();

    }

    public void FalouComTV()
    {
        pointer.enabled = false;
        precisaFalar = false;
        faloComTV = true;
        flowController.FlowHUB();
    }
}
