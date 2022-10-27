using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlowDebug : MonoBehaviour
{
	public TMP_Text acab;
	public TMP_Text aiganhou;

	public BoolVariableArray acabou01;
	public BoolVariableArray aiGanhou;
	
	/*void Start()
    {

		DontDestroyOnLoad(gameObject);
		acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
		aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");

	}
	
    void Update()
    {
		acab.text = "";
		aiganhou.text = "";
		for (int i = 0; i < acabou01.Value.Length; i++)
		{
			acab.text += acabou01.Value[i].ToString() + "\n";
		}
		for (int i = 0; i < aiGanhou.Value.Length; i++)
		{
			aiganhou.text += aiGanhou.Value[i].ToString() + "\n";
		}
		
    }*/
}
