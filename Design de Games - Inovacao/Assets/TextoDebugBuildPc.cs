using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoDebugBuildPc : MonoBehaviour
{
	public BoolVariable buildpc;
	private TMP_Text text;

	private void Start()
	{
		text = GetComponent<TMP_Text>();
	}
	void Update()
    {
		text.text = buildpc.Value.ToString();
    }
}
