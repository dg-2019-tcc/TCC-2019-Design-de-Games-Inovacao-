using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroa : MonoBehaviour
{
	public Transform ganhador;

    void Update()
    {
		if (ganhador != null)
		{
			transform.position = ganhador.position;
		}
    }
}
