using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;
using Photon.Pun;

public class EmpinaMoto : MonoBehaviour
{
	[Header ("PhotonViews")]
	public PhotonView playerPV;
	public PhotonView motoPV;

	[Header ("ScriptableObjects")]
	public FloatVariable playerSpeed;
	//spublic BoolVariable canJump;
	public FloatVariable jumpForce;
	private float originalJumpForce;
	public Controller2D controller;
    public TriggerCollisionsController triggerController;

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
	//public TMP_Text acelerometro;
	public Image brilhoDeBoost;
	public SpriteRenderer motoBrilho;

	[Header("GayTrail")]
	public GameObject particula;
	public ParticleSystem trail;

    [Header("GiraMoto")]
    public float rotationSpeed = 5f;
    public GameObject player;
    public Joystick joy;
    public PlayerThings playerThings;


    private void Start()
	{
        joy = FindObjectOfType<Joystick>();
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
        if (controller.collisions.climbingSlope)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, controller.collisions.slopeAngle + 30), 1f);
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, controller.collisions.slopeAngle), 1f);
        }

        else if (controller.collisions.descendingSlope)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, -controller.collisions.slopeAngle -45), 1f);
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, -controller.collisions.slopeAngle +30), 1f);
        }

        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0), 0.5f);
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, 0), 0.5f);
        }

        if (isManobrandoNoAr)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, Mathf.Sin(Time.time) * (transform.localRotation.z + 45)), 0.5f);
            playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, boostSpeed, 0.5f);
            //particula.SetActive(true);
            if (!trail.isPlaying)
            {
                trail.Play();
            }
            if (controller.collisions.below)
            {
                //Debug.Log("Caiu");
                playerThings.StartCoroutine("LevouDogada");
                //playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, 0, 0.5f);
                transform.localRotation = Random.rotation;
                //particula.SetActive(false);
                if (trail.isPlaying)
                {
                    trail.Stop();
                }
            }
        }

        else if (isEmpinando)
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 45), 0.5f);
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, 45), 0.5f);
            playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, boostSpeed, 0.5f);
			//particula.SetActive(true);
			if (!trail.isPlaying)
			{
				trail.Play();
			}

            /*if(triggerController.collisions.boostMoto == false)
            {
                isEmpinando = false;
            }*/
		}

        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0),0.5f);
            if (trail.isPlaying)
            {
                trail.Stop();
            }
        }

        if (controller.collisions.below)
        {
            isManobrandoNoAr = false;
        }



		if (motoPV.IsMine || !PhotonNetwork.InRoom)
		{
		//	acelerometro.text =  playerSpeed.Value.ToString();

			if (carregado || !controller.collisions.below)
			{
				//Debug.Log("Vai filhão");
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


		baseSpeed = Mathf.Lerp(baseSpeed, boostSpeed, 0.01f * Time.deltaTime);



	}

	public void buttonEmpina()
	{
		if (isManobrandoNoAr || isEmpinando) return;

		if (triggerController.collisions.boostMoto && controller.collisions.below)
		{
				//Debug.Log("empinou");
				isEmpinando = true;

            if (!PhotonNetwork.InRoom)
            {
                daGrau(1);
            }
            else
            {
                motoPV.RPC("daGrau", RpcTarget.All, 1);
            }
			
		}


        if (!controller.collisions.below)
		{

				//Debug.Log("manobrou no ar");
				isManobrandoNoAr = true;
            if (!PhotonNetwork.InRoom)
            {
                daGrau(1);
            }
            else
            {
                motoPV.RPC("daGrau", RpcTarget.All, 2);
            }

        }



    }


	public void stopManobra()
	{

		isManobrandoNoAr = false;
        if (!PhotonNetwork.InRoom)
        {
            daGrau(0);
        }
        else
        {
            motoPV.RPC("daGrau", RpcTarget.All, 0);
        };

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

	}

	public IEnumerator Empinando()
	{
		//Debug.Log("moto");
		
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
