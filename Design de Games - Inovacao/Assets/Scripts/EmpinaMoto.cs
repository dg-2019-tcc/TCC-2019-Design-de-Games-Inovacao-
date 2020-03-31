using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	public FloatVariable playerSpeed;

	public bool isEmpinando;

	public static bool carregado;

	private float originalSpeed;
	public float boostSpeed;



	private void Start()
	{
		isEmpinando = false;
		originalSpeed = playerSpeed.Value;
	}

	private void Update()
	{
		if (isEmpinando)
		{
		 	transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 45), 0.5f);
			playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, boostSpeed, 0.5f);
		}
		else
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0), 0.5f);
			playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, originalSpeed, 0.5f);
		}


		if (carregado)
		{
			Debug.Log("Vai filhão");
			//brilha a moto
		}

	}

	public void buttonEmpina()
	{
		if (carregado)
		{
			Debug.Log("empinou");
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
