using Photon.Pun;
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

    public NewPlayerMovent playerMove;

    public GameObject playerParado;
    public GameObject playerAndando;

    private Controller2D controller;

    public float autoScroll;
    public bool isColeta;
    Vector2 input;

    public BoolVariable buildPC;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        PV = GetComponent<PhotonView>();
        playerMove = GetComponent<NewPlayerMovent>();
        buildPC = Resources.Load<BoolVariable>("BuildPC");


        joyStick = FindObjectOfType<FloatingJoystick>();
		if (PhotonNetwork.InRoom && !PV.Owner.IsLocal)
		{
			Destroy(joyStick);
		}
		rightDir = true;
		leftDir = false;

        menuCustom = false;

        if (SceneManager.GetActiveScene().name == "HUB" || !PhotonNetwork.InRoom)
        {
            menuCustom = true;
        }

        if(SceneManager.GetActiveScene().name == "Customiza" || SceneManager.GetActiveScene().name == "Cabelo" || SceneManager.GetActiveScene().name == "Shirt" || SceneManager.GetActiveScene().name == "Tenis")
        {
            desativaCanvas = true;
        }


		if (desativaCanvas == true || buildPC.Value == true)
		{
			canvasSelf.SetActive(false);
		}
		else
		{
			canvasSelf.SetActive(true);
		}

        if (menuCustom)
        {
            cameraManager.SendMessage("ActivateCamera", true);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }


    void Update()
    {
        if (GameManager.pausaJogo == true) { return; }
        if ((desativaCanvas && canvasSelf.activeSelf) || buildPC.Value == true)
        {
            canvasSelf.SetActive(false);
        }

        if (PV.IsMine && comecou &&(GameManager.Instance.fase.Equals(GameManager.Fase.Coleta) || GameManager.Instance.fase.Equals(GameManager.Fase.Corrida)))
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
                if (buildPC.Value)
                {
                    input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                }

                if (joyStick.Horizontal > 0 || input.x > 0 && !rightDir)
                {
                    rightDir = true;
                    leftDir = false;
                    ThrowObject.dirRight = false;

					GiraOn(rightDir);

				}
                else if (joyStick.Horizontal < 0 || input.x < 0 && !leftDir)
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

        else
        {
            joyStick = FindObjectOfType<FloatingJoystick>();
        }

		if (PhotonNetwork.InRoom && PV != null && isColeta)
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
}
