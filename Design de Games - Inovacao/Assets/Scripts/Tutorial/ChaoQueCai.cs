using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoQueCai : MonoBehaviour
{
	private bool playerTocando;
	public GameObject coletavel;
    public GameObject sprite;

	public bool bulletTime;

	private void Update()
	{
		if (playerTocando && coletavel == null)
		{
            Destroy(sprite);
			Destroy(gameObject);
		}

		playerTocando = false;
	}

	public void ToAqui()
	{
		playerTocando = true;

	}

}
