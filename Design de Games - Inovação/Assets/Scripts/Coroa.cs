using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroa : MonoBehaviour
{
	public Transform ganhador;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if (ganhador != null)
		{
			transform.position = ganhador.position;
		}
    }
}
