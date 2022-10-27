using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MotoActivateBoost : MonoBehaviour
{
   



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Moto")) //&& collision.GetComponent<PhotonView>().IsMine)
		{
			EmpinaMoto.carregado = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Moto")) //&& collision.GetComponent<PhotonView>().IsMine)
		{
			EmpinaMoto.carregado = false;
		}
	}
}
