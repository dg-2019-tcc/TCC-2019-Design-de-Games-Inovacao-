using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	public PhotonView PV;

	public FloatVariable playerSpeed;
	public BoolVariable canJump;

	public bool isEmpinando;
	public bool isManobrandoNoAr;

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
		else if (isManobrandoNoAr)
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, Mathf.Sin(Time.time) * transform.localRotation.z), 0.5f);
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
		if (PV.IsMine)
		{
			if (carregado)
			{
				Debug.Log("empinou");
				isEmpinando = true;
				PV.RPC("daGrau", RpcTarget.All, 1);
			}

			if (!canJump)
			{

				Debug.Log("manobrou no ar");
				isManobrandoNoAr = true;
				PV.RPC("daGrau", RpcTarget.All, 2);

			}
		}

		
	}

	[PunRPC]
	public void daGrau(float modo)
	{
		switch (modo)
		{
			case 1:
				StartCoroutine("Empinando");
				isEmpinando = true;
				break;

			case 2:
				StartCoroutine("Manobrando");
				isManobrandoNoAr = true;
				break;

			default:
				break;
				
		}
		
		
	}

	public IEnumerator Empinando()
	{
		Debug.Log("moto");
		
		yield return new WaitForSeconds(2);
		isEmpinando = false;
		isManobrandoNoAr = false;
	}
}
