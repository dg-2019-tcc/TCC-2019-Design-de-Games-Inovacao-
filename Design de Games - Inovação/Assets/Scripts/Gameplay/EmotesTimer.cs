using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotesTimer : MonoBehaviour
{
	public float segundos;

	private void OnEnable()
	{
		StartCoroutine("Timer", segundos);
	}

	private IEnumerator Timer(float tempo)
	{
		yield return new WaitForSeconds(tempo);
		gameObject.SetActive(false);
	}
}
