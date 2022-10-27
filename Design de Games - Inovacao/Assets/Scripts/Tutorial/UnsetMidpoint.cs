using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsetMidpoint : MonoBehaviour
{
	CamLookaheadAim script;


    void Update()
    {
		if (script == null)
		{
			script = FindObjectOfType<CamLookaheadAim>();
			script.deltaAltura = 0f;
		}
    }

}
