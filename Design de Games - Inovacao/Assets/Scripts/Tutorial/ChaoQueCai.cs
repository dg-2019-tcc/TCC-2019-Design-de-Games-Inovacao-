using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoQueCai : MonoBehaviour
{
	private bool playerTocando;
	public GameObject coletavel;


	private void Update()
	{
		if (playerTocando && coletavel == null)
		{
			Destroy(gameObject);
		}

		playerTocando = false;
	}

	public void ToAqui()
	{
		playerTocando = true;

	}
}
