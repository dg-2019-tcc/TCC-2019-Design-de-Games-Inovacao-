using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFases : MonoBehaviour
{
	public bool shouldFade = false;


	private Image cor;
	private void Start()
	{
		if(shouldFade)
		cor = GetComponent<Image>();
	}
	private void Update()
	{
		if(shouldFade)
		cor.color = Color.Lerp(cor.color, new Color(cor.color.r, cor.color.g, cor.color.b, 0), 0.1f);
	}
}
