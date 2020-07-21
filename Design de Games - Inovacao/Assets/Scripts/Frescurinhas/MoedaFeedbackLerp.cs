using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoedaFeedbackLerp : MonoBehaviour
{
	public TMP_Text texto;
	public Points moedas;
	private float pontosDisplay;
	/*
    void Start()
    {
		texto = GetComponent<TMP_Text>();
    }
	*/

    void Update()
    {
		pontosDisplay = Mathf.Lerp(pontosDisplay, moedas.Value, 0.5f);
		texto.text = pontosDisplay.ToString();
    }
}
