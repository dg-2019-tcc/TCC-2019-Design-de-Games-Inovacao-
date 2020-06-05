﻿using System.Collections;
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
		for (int i = 0; i < acabou01.Value.Length; i++)
		{
			
			if (!acabou01.Value[i])
			{
				if (!aiGanhou.Value[i-1])
				{
					CoisasAtivas(i, true);
				}
				break;
			}

		}


		faloComTV = false;
    }

	
	public void CoisasAtivas(int index, bool ativar)
	{
		falas[index].SetActive(ativar);
		pointer.enabled = ativar;
		precisaFalar = ativar;
	}

    private void Update()
    {
        if(faloComTV == true)
        {
            pointer.enabled = false;
        }
    }

    void FalouComTV()
    {
        pointer.enabled = false;
        precisaFalar = false;
        faloComTV = true;
    }
}
