using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	[Header ("PhotonViews")]
	public PhotonView playerPV;
	public PhotonView motoPV;

	[Header ("ScriptableObjects")]
	public FloatVariable playerSpeed;
	public BoolVariable canJump;
	public FloatVariable jumpForce;
	private float originalJumpForce;


	[Header ("Booleans (tá visível pra checar se ta dando certo, mas tem script vendo tbm)")]
	public bool isEmpinando;
	public bool isManobrandoNoAr;

	public static bool carregado;

	
	[Header("Velocidades")]
	public float baseSpeed;
	public float boostSpeed;
	private float originalSpeed;
	public float jumpMoto;


	[Header("Feedbacks")]
	public Image acelerometro;
	public Image brilhoDeBoost;
	public SpriteRenderer motoBrilho;



	private void Start()
	{
		isEmpinando = false;
		isManobrandoNoAr = false;
		originalSpeed = playerSpeed.Value;
		playerSpeed.Value = baseSpeed;
		motoPV = GetComponent<PhotonView>();
		brilhoDeBoost.gameObject.SetActive(false);
		motoBrilho.gameObject.SetActive(false);
		originalJumpForce = jumpForce.Value;
		jumpForce.Value = jumpMoto;
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
			if (canJump.Value)
			{
				playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, 0, 0.5f);
				transform.localRotation = Random.rotation;
			}
		}
		else
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0), 0.5f);
			playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, baseSpeed, 0.5f);
		}



		if (motoPV.IsMine || !PhotonNetwork.InRoom)
		{
			acelerometro.fillAmount = playerSpeed.Value / boostSpeed;

			if (carregado || !canJump.Value)
			{
				Debug.Log("Vai filhão");
				brilhoDeBoost.gameObject.SetActive(true);
				motoBrilho.gameObject.SetActive(true);
				motoBrilho.color = Color.Lerp(motoBrilho.color, Random.ColorHSV(0, 1), 0.1f);
			}
			else
			{
				brilhoDeBoost.gameObject.SetActive(false);
				motoBrilho.gameObject.SetActive(false);
			}
		}
				

	}

	public void buttonEmpina()
	{
		if (isManobrandoNoAr || isEmpinando) return;

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


	public void stopManobra()
	{

		isManobrandoNoAr = false;
		motoPV.RPC("daGrau", RpcTarget.All, 0);

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
				//StartCoroutine("Empinando");
				isManobrandoNoAr = true;
				break;

			default:
				isManobrandoNoAr = false;
				break;
				
		}

		baseSpeed = Mathf.Lerp(baseSpeed, boostSpeed, 0.1f);
	}

	public IEnumerator Empinando()
	{
		Debug.Log("moto");
		
		yield return new WaitForSeconds(2);
		isEmpinando = false;
		//isManobrandoNoAr = false;
	}

	private void OnDestroy()
	{
		playerSpeed.Value = originalSpeed;
		jumpForce.Value = originalJumpForce;
	}
}
