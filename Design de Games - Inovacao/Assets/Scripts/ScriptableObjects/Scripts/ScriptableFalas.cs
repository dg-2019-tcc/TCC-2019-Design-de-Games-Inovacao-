using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Fala")]

public class ScriptableFalas : ScriptableObject
{
	public Sprite imagemPT;
	public Sprite imagemEN;

	public string[] falasPT;
	public string[] falasEN;

	[HideInInspector]
	public string[] falas;


	public TranslateVariable language;
	public Sprite Imagem()
	{
		switch (GameManager.languageIndex)
		{
			default:
			case 0:
				falas = falasPT;
				return imagemPT;

			case 1:
				falas = falasEN;
				return imagemEN;
		}
	}
	
}
