using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Button")]
public class ScriptableButtons : ScriptableObject
{
	[Header ("Colocar na ordem Português > Inglês")]
	public string[] text;
	public Sprite[] image;
}
