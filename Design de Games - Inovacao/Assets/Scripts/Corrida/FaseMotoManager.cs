using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FaseMotoManager : MonoBehaviour
{

	public Vector3 speed;

	[HideInInspector]
	public static bool rodaFase;

    // Start is called before the first frame update
    void Start()
    {
		rodaFase = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (rodaFase)
		{
			transform.position += -speed;
		}
    }
	[PunRPC]
	public void comeca()
	{

		rodaFase = true;
	}
}
