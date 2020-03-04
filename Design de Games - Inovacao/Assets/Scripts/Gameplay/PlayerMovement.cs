using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

	[Header("Coletáveis")]

	
	
    public bool ganhouCorrida;
    public bool perdeuCorrida;
	[SerializeField]
	private float delayForWinScreen;



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
	public bool leftDir;
	public bool rightDir;
	public BoolVariable canJump;
    static bool acabouPartida;



	[Header("Canvas")]

	[SerializeField]
	public Joystick joyStick;
    public GameObject jumpButton;
    private Image jumpImage;
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

	[Header("Camera Manager")]
	public CameraManager cameraManager;



	[Header("Som")]

	public SimpleAudioEvent puloAudioEvent;
	public AudioSource puloSom;
	public GameObject walkSom;
    public AudioSource perdeuSom;
    public AudioSource levouDogadaSom;
    public AudioSource ganhouSom;


	[Header("SkillsState")]

    public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;
    public BoolVariable carroActive;
	public BoolVariable pipaActive;
	public BoolVariable hitCarroToken;
	public BoolVariable hitPipaToken;
	public BoolVariable startPipa;
	public BoolVariable startCarro;

	public float oldPos;
	public float newPos;

	private bool menuCustom;
    public static bool acabou = false;

	private PlayerAnimations playerAnimations;

    public SwipeDirection dir;

    void Start()
	{
        jumpImage = jumpButton.GetComponent<Image>();
        acabou = false;
        acabouPartida = false;
		rb2d = GetComponent<Rigidbody2D>();
		joyStick = FindObjectOfType<Joystick>();
		fixedButton = FindObjectOfType<FixedButton>();
		PV = GetComponent<PhotonView>();
		stats.speed = playerSpeed;
		stats.jumpForce = playerJump;
		playerAnimations = GetComponent<PlayerAnimations>();
		playerAnimations.rb2d = rb2d;

		menuCustom = false;

		if (SceneManager.GetActiveScene().name == "MenuCustomizacao") menuCustom = true;


		if (desativaCanvas == true)
		{
			canvasSelf.SetActive(false);
		}

		if (PV.IsMine || menuCustom)
		{
			cameraManager.SendMessage("ActivateCamera", true);
			PV.Owner.SetScore(0);
			rb2d.gravityScale = 0.7f;
		}

		else
		{
			canvasSelf.SetActive(false);
			rb2d.isKinematic = true;
		}

		/*if (SceneManager.GetActiveScene().name == "TelaVitoria" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
			FindObjectOfType<Coroa>().ganhador = transform;
            playerAnimations.playerAC.SetTrigger(playerAnimations.animatorWon);
            ganhouSom.Play();
            transform.position = new Vector3(0, 0, 0);
			PV.Owner.SetScore(-1);
		}

        else if (SceneManager.GetActiveScene().name == "TelaVitoria" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 0)
        {
			playerAnimations.playerAC.SetTrigger(playerAnimations.animatorLost);
            transform.position = new Vector3(0, 0, 0);
            perdeuSom.Play();
			PV.Owner.SetScore(-1);
		}*/

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


        if (acabouPartida == true) return;

        if(LinhaDeChegada.changeRoom == true)
        {
			StartCoroutine(Venceu());
		}

        if (ganhouCorrida)
        {
            GanhouCorrida();
        }

        if (perdeuCorrida)
        {
            PerdeuCorrida();
        }

        

        if (!menuCustom)
		{
			if (!PV.IsMine)
			{
				return;
			}
		}

		if (joyStick != null)
		{
			if (joyStick.Horizontal > 0 || ThrowObject.dirRight == true && rightDir == false)
			{
				rightDir = true;
				leftDir = false;
				ThrowObject.dirRight = false;

				gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
				if (!PhotonNetwork.InRoom)
				{
					Quaternion direction = Quaternion.Euler(0, 90, 0);
					player.transform.rotation = direction;
					carro.transform.rotation = direction;
					pipa.transform.rotation = direction;
				}

			}
			else if (joyStick.Horizontal < 0 || ThrowObject.dirLeft == true && leftDir == false)
			{

				leftDir = true;
				rightDir = false;
				ThrowObject.dirLeft = false;
				gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
				if (!PhotonNetwork.InRoom)
				{
					Quaternion direction = Quaternion.Euler(0, -90, 0);
					player.transform.rotation = direction;
					carro.transform.rotation = direction;
					pipa.transform.rotation = direction;
				}
			}


			//Movimentação do player no joystick
			float moveHorizontal = joyStick.Horizontal + Input.GetAxisRaw("Horizontal");
			if (moveHorizontal != 0 && levouDogada == false && acabou == false)
			{
				rb2d.velocity = new Vector3(stats.speed.Value * moveHorizontal, rb2d.velocity.y, 0);
				//playerAnimations.Walk(true);
			}

            if(moveHorizontal >0.1f || moveHorizontal< -0.1f)
            {
                playerAnimations.Walk(true);
            }

			else
			{
				playerAnimations.Walk(false);
			}
			if (rb2d.velocity.y > 0)
			{
				canJump.Value = false;
			}
		}



        // Pulo
        //float moveVertical = joyStick.Vertical;
        /*foreach (Touch touch in Input.touches)
        {

            if (touch.fingerId == 0 && touch.position.x > Screen.width / 2 && touch.phase != TouchPhase.Began)
            {
                Debug.Log("Pulo");
                // Finger 1 is touching! (remember, we count from 0)
                jump = true;
            }

        }*/
        var tempColor = jumpImage.color;

        if (canJump.Value == true || efeitoCarro.ativa.Value == true || efeitoPipa.ativa.Value == true)
        {

            //jumpButton.SetActive(true);
            tempColor.a = 1f;
            jumpImage.color = tempColor;
        }

        else
        {
            //jumpButton.SetActive(false);
            tempColor.a = 0.5f;
            jumpImage.color = tempColor;
        }



        if (jump == true && grounded == true && canJump.Value == true && acabou == false)
		{
			playerAnimations.playerAC.SetTrigger(playerAnimations.animatorJump);
			puloAudioEvent.Play(puloSom);
			rb2d.AddForce(new Vector2(0, stats.jumpForce.Value), ForceMode2D.Impulse);
			jump = false;


        }

        else
        {
            jump = false;
        }


   

    }


    [PunRPC]
    void GanhouCorrida()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
        ganhouSom.Play();
		playerAnimations.playerAC.SetTrigger(playerAnimations.animatorWon);

        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

        gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

        gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.All);
        ganhouCorrida = false;
        acabou = true;

    }

    [PunRPC]
    void PerdeuCorrida()
    {
        perdeuSom.Play();
        perdeuCorrida = true;
        acabou = true;
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
    }


	[PunRPC]
	void TrocaSala()
	{
        ganhouCorrida = false;
        perdeuCorrida = false;
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
			Quaternion direction = Quaternion.Euler(0, 90, 0);
			player.transform.rotation = direction;
            carro.transform.rotation = direction;
			pipa.transform.rotation = direction;
		}
        else
        {
			Quaternion direction = Quaternion.Euler(0, -90, 0);
			player.transform.rotation = direction;
            carro.transform.rotation = direction;
			pipa.transform.rotation = direction;
		}
    }



	//Função para o botão de pulo
	public void Jump()
	{
        foreach (Touch touch in Input.touches)
        {

            if (touch.pressure == 1f && touch.position.x > Screen.width / 2 && canJump.Value == true)
            {
                    // Finger 1 is touching! (remember, we count from 0)
                    jump = true;
                DogController.poderEstaAtivo = false;
            }

        }

#if UNITY_EDITOR
		// Finger 1 is touching! (remember, we count from 0)
		jump = true;
		DogController.poderEstaAtivo = false;
#endif

	}

    public void Terminou()
    {
        acabouPartida = true;
    }
	
    IEnumerator LevouDogada()
    {
        levouDogadaSom.Play();
		playerAnimations.playerAC.SetBool(playerAnimations.animatorDogada, true);
        levouDogada = true;
        yield  return new WaitForSeconds(2f);
		playerAnimations.playerAC.SetBool(playerAnimations.animatorDogada, false);
        levouDogada = false;
    }

	IEnumerator Venceu()
	{
		cameraManager.SendMessage("ActivateCamera", false);
		PV.RPC("Terminou", RpcTarget.All);
		for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
		{
			if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Ganhador"] == 1)
            {
                StopCoroutine(Venceu());
            }
		}
		PV.Owner.SetScore(-1);
        ganhouCorrida = false;
		yield return new WaitForSeconds(delayForWinScreen);
		PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

		gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

		gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.All);
		
	}


}
