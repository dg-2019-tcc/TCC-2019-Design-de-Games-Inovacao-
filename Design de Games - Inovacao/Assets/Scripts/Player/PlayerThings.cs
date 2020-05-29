﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class PlayerThings : MonoBehaviour
{
	
	private bool menuCustom;
    [SerializeField]
    private bool desativaCanvas;

    public GameObject canvasSelf;

    [HideInInspector]
    public PhotonView PV;

    public CameraManager cameraManager;
    public bool comecou;

    public bool shouldTurn;

    public static bool leftDir;
    public static bool rightDir;

    public GameObject player;
    public GameObject carro;
    public GameObject pipa;
    public GameObject dog;

	[Header ("Tiro")]
	public BoolVariable levou;
	public float StunTime;

	static public bool acabouPartida;

    [SerializeField]
    public Joystick joyStick;

    public GameObject playerParado;
    public GameObject playerAndando;

    private Controller2D controller;

    public float autoScroll;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        PV = GetComponent<PhotonView>();
        joyStick = FindObjectOfType<Joystick>();
		rightDir = true;
		leftDir = false;

        menuCustom = false;

        if (SceneManager.GetActiveScene().name == "HUB" || !PhotonNetwork.InRoom)
        {
            menuCustom = true;
        }

        if(SceneManager.GetActiveScene().name == "Customiza")
        {
            desativaCanvas = true;
        }


        if (desativaCanvas == true)
        {
            canvasSelf.SetActive(false);
        }

        if (menuCustom)
        {
            cameraManager.SendMessage("ActivateCamera", true);
        }

        else
        {
            canvasSelf.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }


    void Update()
    {
        if(desativaCanvas && canvasSelf.activeSelf)
        {
            canvasSelf.SetActive(false);
        }

        if (PV.IsMine && comecou)
        {
            cameraManager.SendMessage("ActivateCamera", true);
        }

        if (acabouPartida == true) return;

        if (!menuCustom && !PV.IsMine)
        {
            return;
        }

        if (joyStick != null)
        {
            if (shouldTurn)
            {
                if (joyStick.Horizontal > 0 || ThrowObject.dirRight && rightDir)
                {



                    rightDir = true;
                    leftDir = false;
                    ThrowObject.dirRight = false;



					GiraOn(rightDir);

				}
                else if (joyStick.Horizontal < 0 || ThrowObject.dirLeft == true && leftDir == false)
                {


                    leftDir = true;
                    rightDir = false;
                    ThrowObject.dirLeft = false;



					GiraOn(rightDir);
                }

                /*if (controller.collisions.climbingSlope)
                {
                    player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, controller.collisions.slopeAngle), 1f);
                    //carro.transform.localRotation = Quaternion.Slerp(carro.transform.localRotation, Quaternion.Euler(carro.transform.localRotation.x, player.transform.localRotation.y, controller.collisions.slopeAngle), 1f);
                }

                else if (controller.collisions.descendingSlope)
                {
                    player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, -controller.collisions.slopeAngle), 1f);
                    //carro.transform.localRotation = Quaternion.Slerp(carro.transform.localRotation, Quaternion.Euler(carro.transform.localRotation.x, carro.transform.localRotation.y, -controller.collisions.slopeAngle), 1f);
                }*/
            }

            float moveHorizontal = Mathf.Clamp(joyStick.Horizontal + Input.GetAxisRaw("Horizontal") + autoScroll, -2, 2);

            if (!PhotonNetwork.InRoom)
            {
                AtualizaPosicao(transform.position);
            }
            else
            {
                PV.RPC("AtualizaPosicao", RpcTarget.All, transform.position);
            }

            //PV.RPC("AtualizaPosicao", RpcTarget.All, transform.position);

            /*if (moveHorizontal > 0.1f || moveHorizontal < -0.1f|| controller.collisions.below == false)
            {
                //  playerAnimations.Walk(true);
                playerAndando.SetActive(true);
                playerParado.SetActive(false);
            }

            else
            {
                //playerAnimations.Walk(false);
                playerAndando.SetActive(false);
                playerParado.SetActive(true);
            }*/
        }


		if (PhotonNetwork.InRoom)
		{
			PV.RPC("GambiarraDosIndex", RpcTarget.All, (int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]);
		}

	}

	void GiraOn(bool dir)
	{
		int angle;
		if (dir)
		{
			angle = 0;
		}
		else
		{
			angle = 180;
		}

		if (PhotonNetwork.InRoom)
		{
			PV.RPC("NewGiraPlayer", RpcTarget.All, angle);
		}
		else
		{
			NewGiraPlayer(angle);
		}
	}

    [PunRPC]
    void NewGiraPlayer(int dir)
    {
		Quaternion direction = Quaternion.Euler(0, dir, 0);
		player.transform.rotation = direction;
		carro.transform.rotation = direction;
		pipa.transform.rotation = direction;
		dog.transform.rotation = direction;
	}

	public IEnumerator LevouDogada()
	{

		levou.Value = true;
		yield return new WaitForSeconds(StunTime);
		levou.Value = false;

	}

	[PunRPC]
	void AtualizaPosicao(Vector3 newPos)
	{
		if (!PV.IsMine)
		{
			transform.position = Vector3.Lerp(transform.position, newPos, 0.2f);
		}
	}


	[PunRPC]
	void GambiarraDosIndex(int index)
	{
		PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"] = index;
	}
}