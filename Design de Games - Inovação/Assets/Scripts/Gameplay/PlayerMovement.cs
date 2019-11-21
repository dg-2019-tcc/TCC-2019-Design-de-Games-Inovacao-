using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{

	[Header("Coletáveis")]

	[SerializeField]
	private float numeroDeColetaveis;
	[HideInInspector]
	public static float coletavel;



	[Header("Movimentação física")]

	public GameObject player;
    public GameObject carro;
    public GameObject pipa;
	private Rigidbody2D rb2d;
	public bool jump;
	public Transform groundCheck;
	public bool grounded;
	public bool levouDogada;
	public PlayerStat stats;
	public FloatVariable playerSpeed;
	public FloatVariable playerJump;
	private bool leftDir;
	private bool rightDir;
	public BoolVariable canJump;


	[Header("Canvas")]

	[SerializeField]
	protected Joystick joyStick;
	protected FixedButton fixedButton;
	public GameObject canvasSelf;
	public GameObject canvasPause;
	[SerializeField]
	private bool desativaCanvas;

	private Transform target;
	public float speedToTotem = 10.0f;
	public static bool acertouTotem;



	[Header("Photon")]

	[HideInInspector]
	public PhotonView PV;
	[SerializeField]
	private GameObject identificador;



	[Header("Cinemachine")]

	private CinemachineConfiner CC;
	private CinemachineVirtualCamera VC;




	[Header("Som")]

	public SimpleAudioEvent puloAudioEvent;
	public AudioSource puloSom;
	public GameObject walkSom;
	public AudioSource coleta;



	[Header("Animação")]

	public Animator playerAC;
    public Animator carroAC;
	private PlayerFaceAnimations playerFaceAnimations;



	[Header("SkillsState")]

	public BoolVariable carroState;
	public BoolVariable pipaState;
	public BoolVariable hitCarroToken;
	public BoolVariable hitPipaToken;
	public BoolVariable startPipa;
	public BoolVariable startCarro;

	public float oldPos;
	public float newPos;

	private bool menuCustom;



	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D>();
		joyStick = FindObjectOfType<Joystick>();
		fixedButton = FindObjectOfType<FixedButton>();
		PV = GetComponent<PhotonView>();
		stats.speed = playerSpeed;
		stats.jumpForce = playerJump;

		menuCustom = false;

		if (SceneManager.GetActiveScene().name == "MenuCustomizacao") menuCustom = true;


		if (desativaCanvas == true)
		{
			canvasSelf.SetActive(false);
		}

		//if (!PhotonNetwork.IsConnected) return;
		if (PV.IsMine || menuCustom)
		{
			identificador.SetActive(true);
			VC = gameObject.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
			VC.Priority = 15;

			if (joyStick.isActiveAndEnabled)
			{
				CC = gameObject.transform.GetChild(0).GetComponent<CinemachineConfiner>();
				CC.m_BoundingShape2D = GameObject.Find("CameraConfiner").GetComponent<PolygonCollider2D>();
				CC.InvalidatePathCache();
			}
			rb2d.gravityScale = 0.7f;
		}
		else
		{
			canvasSelf.SetActive(false);
			rb2d.isKinematic = true;
		}

		if (SceneManager.GetActiveScene().name == "TelaVitoria" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
			FindObjectOfType<Coroa>().ganhador = transform;
            playerAC.SetTrigger("Won");
            transform.position = new Vector3(0, 0, 0);
		}

        if (SceneManager.GetActiveScene().name == "TelaVitoria" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 0)
        {
            playerAC.SetBool("Lost", true);
            transform.position = new Vector3(0, 0, 0);
        }

        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
		gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);
		oldPos = transform.position.x;
		newPos = transform.position.x;
	}

    private void LateUpdate()
    {

        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {

        if((int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
        {
            playerAC.SetTrigger("Won");
        }

        if (PhotonNetwork.PlayerList.Length >= 1)
		{
			coletavel = PV.Owner.GetScore();

        }


		if (coletavel >= numeroDeColetaveis)
		{
			PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

			gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

			gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
			coletavel = 0;
		}

		if (!PV.IsMine && !menuCustom) return;


		if (joyStick != null)
		{
			if (joyStick.Horizontal > 0 && rightDir == false)
			{

				rightDir = true;
				leftDir = false;
				gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
				if (!PhotonNetwork.InRoom)
				{
					player.transform.rotation = Quaternion.Euler(0, 90, 0);
					carro.transform.rotation = Quaternion.Euler(0, 90, 0);
					pipa.transform.rotation = Quaternion.Euler(0, 90, 0);
				}

			}
			else if (joyStick.Horizontal < 0 && leftDir == false)
			{

				leftDir = true;
				rightDir = false;
				gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
				if (!PhotonNetwork.InRoom)
				{
					player.transform.rotation = Quaternion.Euler(0, -90, 0);
					carro.transform.rotation = Quaternion.Euler(0, -90, 0);
					pipa.transform.rotation = Quaternion.Euler(0, -90, 0);
				}
			}

			//Movimentação do player no joystick
			float moveHorizontal = joyStick.Horizontal + Input.GetAxisRaw("Horizontal");
			if (moveHorizontal != 0 && levouDogada == false)
			{

				rb2d.velocity = new Vector3(stats.speed.Value * moveHorizontal, rb2d.velocity.y, 0);
				playerAC.SetBool("isWalking", true);
                carroAC.SetBool("isWalking", true);
				//playerAC.SetBool("isWalking", true);
				//walkSom.SetActive(true);
			}

			else
			{
				playerAC.SetBool("isWalking", false);
                carroAC.SetBool("isWalking", false);
            }
		}

		/*if (grounded)
        {
            playerAC.SetBool("isGrounded", true);
        }
        else
        {
            playerAC.SetBool("isGrounded", false);

        }*/

		// Pulo
		if (grounded == true && jump == true && canJump.Value == true)
		{
			playerAC.SetTrigger("Jump");
			puloAudioEvent.Play(puloSom);
			rb2d.AddForce(new Vector2(0, stats.jumpForce.Value), ForceMode2D.Impulse);
			//Physics.IgnoreLayerCollision(10, 11, true);
			jump = false;

		}

	}





	[PunRPC]
	void TrocaSala()
	{
		PhotonNetwork.LoadLevel("TelaVitoria");
	}

	[PunRPC]
	void ZeraPontuacao()
	{
		PV.Owner.SetScore(0);
	}

	[PunRPC]
	void GiraPlayer(bool dir)
	{
        if (dir)
        {
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            carro.transform.rotation = Quaternion.Euler(0, 90, 0);
            pipa.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
            carro.transform.rotation = Quaternion.Euler(0, -90, 0);
            pipa.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

	/*[PunRPC]
    private void TransformaPet(bool isDog, string transformation)
    {
        Pet.SetActive(isDog);
    }*/


	//Função para o botão de pulo
	public void Jump()
	{
		playerAC.SetTrigger("Jump");
		jump = true;

	}

	
    IEnumerator LevouDogada()
    {
        playerAC.SetBool("Dogada", true);
        levouDogada = true;
        yield  return new WaitForSeconds(2f);
        playerAC.SetBool("Dogada", false);
        levouDogada = false;
    }
}
