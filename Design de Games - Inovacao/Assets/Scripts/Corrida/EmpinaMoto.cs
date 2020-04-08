using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	[Header ("PhotonViews")]
	public PhotonView playerPV;
	public PhotonView motoPV;

	[Header ("ScriptableObjects")]
	public FloatVariable playerSpeed;
	public BoolVariable canJump;

	[Header ("Booleans (tá visível pra checar se ta dando certo, mas tem script vendo tbm)")]
	public bool isEmpinando;
	public bool isManobrandoNoAr;

	public static bool carregado;

	
	[Header("Velocidades")]
	public float baseSpeed;
	public float boostSpeed;
	private float originalSpeed;



	private void Start()
	{
		isEmpinando = false;
		isManobrandoNoAr = false;
		originalSpeed = playerSpeed.Value;
		playerSpeed.Value = baseSpeed;
		motoPV = GetComponent<PhotonView>();
		//motoPV.ViewID = playerPV.ViewID;
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
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, Mathf.Sin(Time.time) * (transform.localRotation.z + 45)), 0.5f);
			playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, boostSpeed, 0.5f);
		}
		else
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0), 0.5f);
			playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, baseSpeed, 0.5f);
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
				motoPV.RPC("daGrau", RpcTarget.All, 1);
			
			}

			if (!canJump.Value)
			{

				Debug.Log("manobrou no ar");
				isManobrandoNoAr = true;
				motoPV.RPC("daGrau", RpcTarget.All, 2);

			}
		

		
	}

	[PunRPC]
	public void daGrau(int modo)
	{
		switch (modo)
		{
			case 1:
				StartCoroutine("Empinando");
				isEmpinando = true;
				break;

			case 2:
				StartCoroutine("Empinando");
				isManobrandoNoAr = true;
				break;

			default:
				break;
				
		}

		baseSpeed = Mathf.Lerp(baseSpeed, boostSpeed, 0.1f);
	}

	public IEnumerator Empinando()
	{
		Debug.Log("moto");
		
		yield return new WaitForSeconds(2);
		isEmpinando = false;
		isManobrandoNoAr = false;
	}

	private void OnDestroy()
	{
		playerSpeed.Value = originalSpeed;
	}
}
