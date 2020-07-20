using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Points")]
public class Points : ScriptableObject
{
	public float Value;

	public void Add(float points)
	{
		Value += points;
		PlayerPrefs.SetFloat("Moedas", Value);
	}
}
