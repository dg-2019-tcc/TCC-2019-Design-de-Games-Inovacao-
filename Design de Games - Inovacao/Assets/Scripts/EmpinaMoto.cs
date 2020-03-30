using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	public MotoFlip script;

	public bool isEmpinando;

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

	}

	public void buttonEmpina()
	{
		isEmpinando = true;
		daGrau();

		
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
