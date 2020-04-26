using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class PlayerThings : MonoBehaviour
{
	public FloatVariable moveSpeed;
	private bool menuCustom;
    [SerializeField]
    private bool desativaCanvas;

    public GameObject canvasSelf;

    [HideInInspector]
    public PhotonView PV;

    public CameraManager cameraManager;

    public bool shouldTurn;

    public static bool leftDir;
    public static bool rightDir;

    public GameObject player;
    public GameObject carro;
    public GameObject pipa;

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


        if (desativaCanvas == true)
        {
            canvasSelf.SetActive(false);
        }

        if (PV.IsMine)
        {
            cameraManager.SendMessage("ActivateCamera", true);
        }
        else if (menuCustom)
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
        if (acabouPartida == true) return;

        if (!menuCustom && !PV.IsMine)
        {
            return;
        }

        if (joyStick != null)
        {
            if (shouldTurn)
            {
                if (joyStick.Horizontal > 0 || ThrowObject.dirRight == true && rightDir == false)
                {



                    rightDir = true;
                    leftDir = false;
                    ThrowObject.dirRight = false;



                    if (!PhotonNetwork.InRoom)
                    {
                        Quaternion direction = Quaternion.Euler(0, 0, 0);
                        player.transform.rotation = direction;
                        carro.transform.rotation = direction;
                        pipa.transform.rotation = direction;
                    }
                    else
                    {
                        gameObject.GetComponent<PhotonView>().RPC("NewGiraPlayer", RpcTarget.All, rightDir);
                    }

                }
                else if (joyStick.Horizontal < 0 || ThrowObject.dirLeft == true && leftDir == false)
                {


                    leftDir = true;
                    rightDir = false;
                    ThrowObject.dirLeft = false;



                    if (!PhotonNetwork.InRoom)
                    {
                        Quaternion direction = Quaternion.Euler(0, 180, 0);
                        player.transform.rotation = direction;
                        carro.transform.rotation = direction;
                        pipa.transform.rotation = direction;
                    }
                    else
                    {
                        gameObject.GetComponent<PhotonView>().RPC("NewGiraPlayer", RpcTarget.All, rightDir);
                    }
                }
            }

            float moveHorizontal = Mathf.Clamp(joyStick.Horizontal + Input.GetAxisRaw("Horizontal") + autoScroll, -2, 2);

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
    }



    [PunRPC]
    void NewGiraPlayer(bool dir)
    {
        if (dir)
        {
            Quaternion direction = Quaternion.Euler(0, 0, 0);
            player.transform.rotation = direction;
            carro.transform.rotation = direction;
            pipa.transform.rotation = direction;
        }
        else
        {
            Quaternion direction = Quaternion.Euler(0, 180, 0);
            player.transform.rotation = direction;
            carro.transform.rotation = direction;
            pipa.transform.rotation = direction;
        }
    }
}
