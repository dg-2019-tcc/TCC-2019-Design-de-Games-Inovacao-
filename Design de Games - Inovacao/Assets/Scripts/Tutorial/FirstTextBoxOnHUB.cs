﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTextBoxOnHUB : MonoBehaviour
{

	public BoolVariableArray acabou01;
	public BoolVariableArray aiGanhou;

	public TV tv;


	void Start()
    {
		if (aiGanhou == null)
		{
			aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
		}

		if (acabou01 == null)
		{
			acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
		}

		tv = FindObjectOfType<TV>();
	}

	private void OnDestroy()
	{
		aiGanhou.Value[0] = true;
		tv.CoisasAtivas(1, true);
	}
}
