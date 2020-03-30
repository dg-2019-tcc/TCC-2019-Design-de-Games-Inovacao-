using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	public PlayerMovement script;

	public bool isEmpinando;

	public static bool carregado;




	private void Start()
	{
		isEmpinando = false;
	}

	private void Update()
	{
		if (isEmpinando)
		{
		 	transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 45), 0.5f);
		}
		else
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0), 0.5f);
		}


		if (carregado)
		{
			//brilha a moto
		}

	}

	public void buttonEmpina()
	{
		if (carregado)
		{
			isEmpinando = true;
			daGrau();
		}

		
	}

	[PunRPC]
	public void daGrau()
	{
		if (isEmpinando)
		{
			StartCoroutine("Empinando");
			isEmpinando = true;
		}
	}

	public IEnumerator Empinando()
	{
		Debug.Log("moto");
		
		yield return new WaitForSeconds(2);
		isEmpinando = false;
	}
}
