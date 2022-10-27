﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using Complete;

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

	[Header ("Tiro")]
	public BoolVariable levou;
	public float StunTime;

	static public bool acabouPartida;

    [SerializeField]
    public FloatingJoystick joyStick;
    public InputController inputController;

    public NewPlayerMovent playerMove;

    public GameObject playerParado;
    public GameObject playerAndando;

    private Controller2D controller;

    public float autoScroll;
    public bool isColeta;
    Vector2 input;

    public BoolVariable buildPC;

    #region Unity Function

    void Start()
    {
        controller = GetComponent<Controller2D>();
        PV = GetComponent<PhotonView>();
        playerMove = GetComponent<NewPlayerMovent>();
        inputController = GetComponent<InputController>();
        buildPC = Resources.Load<BoolVariable>("BuildPC");

        rightDir = true;
        leftDir = false;

        menuCustom = false;

        if (SceneManager.GetActiveScene().name == "HUB" || !PhotonNetwork.InRoom)
        {
            menuCustom = true;
        }

        if (SceneManager.GetActiveScene().name == "Customiza" || SceneManager.GetActiveScene().name == "Cabelo" || SceneManager.GetActiveScene().name == "Shirt" || SceneManager.GetActiveScene().name == "Tenis")
        {
            desativaCanvas = true;
        }

        CheckHUD();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }


    void Update()
    {
        ShouldUpdate();
        CheckHUD();

        if (PV.IsMine && comecou && (GameManager.sceneAtual == UnityCore.Scene.SceneType.Coleta || GameManager.sceneAtual == UnityCore.Scene.SceneType.Corrida))
        {
            cameraManager.SendMessage("ActivateCamera", true);
        }

        if (acabouPartida == true) return;

        if (!menuCustom && !PV.IsMine){ return;}

        JoystickUpdate();

        if (PhotonNetwork.InRoom && PV != null && isColeta)
        {
            PV.RPC("GambiarraDosIndex", RpcTarget.All, (int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]);
        }

    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void ShouldUpdate()
    {
        if (GameManager.pausaJogo == true || GameManager.isPaused) { return; }
    }

    void CheckHUD()
    {
        if ((desativaCanvas && canvasSelf.activeSelf) || buildPC.Value == true) { canvasSelf.SetActive(false); }
        else{ canvasSelf.SetActive(true);}
    }

    void JoystickUpdate()
    {
        if (joyStick != null || buildPC.Value)
        {
            if (shouldTurn)
            {
                input = inputController.joyInput;

                if (input.x > 0 && !rightDir)
                {
                    rightDir = true;
                    leftDir = false;
                    ThrowObject.dirRight = false;

                    GiraOn(rightDir);
                }
                else if (input.x < 0 && !leftDir)
                {
                    leftDir = true;
                    rightDir = false;
                    ThrowObject.dirLeft = false;

                    GiraOn(rightDir);
                }
            }

            if (!PhotonNetwork.InRoom)
            {
                AtualizaPosicao(transform.position);
            }
            else
            {
                PV.RPC("AtualizaPosicao", RpcTarget.All, transform.position);
            }

        }
        else
        {
            joyStick = FindObjectOfType<FloatingJoystick>();
        }
    }

    void GiraOn(bool dir)
    {
        int angle;
        if (dir){ angle = 0;}
        else{ angle = 180;}

        if (GameManager.inRoom){ PV.RPC("NewGiraPlayer", RpcTarget.All, angle);}
        else { NewGiraPlayer(angle);}
    }

    [PunRPC]
    void NewGiraPlayer(int dir)
    {
        Quaternion direction = Quaternion.Euler(0, dir, 0);
        player.transform.rotation = direction;
        carro.transform.rotation = direction;
        pipa.transform.rotation = direction;
    }

    public IEnumerator LevouDogada()
    {
        if (PV.Owner.IsLocal)
        {
            levou.Value = true;
        }
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
    #endregion
}
