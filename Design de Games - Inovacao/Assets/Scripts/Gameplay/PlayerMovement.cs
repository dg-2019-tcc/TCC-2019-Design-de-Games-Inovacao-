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

    [Header("Movimentação física")]

	public GameObject player;
    public GameObject carro;
    public GameObject pipa;
	private Rigidbody2D rb2d;
	public BoolVariable jump;
	public Transform groundCheck;
	public bool grounded;
	public bool levouDogada;
	public PlayerStat stats;
	public FloatVariable playerSpeed;
	public FloatVariable playerJump;
	public static bool leftDir;
	public static bool rightDir;
	public BoolVariable canJump;
    static public bool acabouPartida;
    public bool canDoubleJump;
	public float autoScroll;

	public Quaternion normal;



    [Header("Canvas")]

	[SerializeField]
	public Joystick joyStick;
    public Button jumpButton;
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

	public PlayerAnimations playerAnimations;

    public SwipeDirection dir;

    public GameObject playerParado;
    public GameObject playerAndando;

    public BoolVariable turnPlat;

    void Start()
	{
        turnPlat = Resources.Load<BoolVariable>("TurnPlat");
        jumpImage = jumpButton.GetComponent<Image>();
        acabou = false;
        acabouPartida = false;
		rb2d = GetComponent<Rigidbody2D>();
		joyStick = FindObjectOfType<Joystick>();
		fixedButton = FindObjectOfType<FixedButton>();
		PV = GetComponent<PhotonView>();
		stats.speed = playerSpeed;
		stats.jumpForce = playerJump;

		//playerAnimations = GetComponent<PlayerAnimations>();
		//playerAnimations.rb2d = rb2d;

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
			rb2d.gravityScale = 0.7f;
		}
		else if (menuCustom)
		{
			cameraManager.SendMessage("ActivateCamera", true);
			rb2d.gravityScale = 0.7f;
		}

		else
		{
			canvasSelf.SetActive(false);
			rb2d.isKinematic = true;
		}


      //  PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
		
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


		


		if (!menuCustom && !PV.IsMine)
		{
			return;
		}

		if (joyStick != null)
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
					gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
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
					gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
				}
			}


			//Movimentação do player no joystick
			float moveHorizontal = Mathf.Clamp(joyStick.Horizontal + Input.GetAxisRaw("Horizontal") + autoScroll, -2, 2);
			if (levouDogada == false && acabou == false)
			{
				if (moveHorizontal != 0)
				{
					rb2d.velocity = normal * new Vector3(stats.speed.Value * moveHorizontal, rb2d.velocity.y, 0);
				}
				else
				{
					rb2d.velocity = normal * new Vector3(Mathf.Lerp(rb2d.velocity.x, 0, 0.1f), rb2d.velocity.y, 0);
				}
			}

            if(moveHorizontal >0.1f || moveHorizontal< -0.1f)
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
            }
			if (rb2d.velocity.y > 0)
			{
				canJump.Value = false;
                playerAndando.SetActive(true);
                playerParado.SetActive(false);
            }

            else if (rb2d.velocity.y < 0)
            {
                

                canJump.Value = false;
                playerAndando.SetActive(true);
                playerParado.SetActive(false);
            }
		}



        var tempColor = jumpImage.color;

        if (canJump.Value == true || efeitoCarro.ativa.Value == true || efeitoPipa.ativa.Value == true || canDoubleJump == true)
        {

            jumpButton.interactable = true;
            tempColor.a = 1f;
            jumpImage.color = tempColor;
        }

        else
        {
            jumpButton.interactable = false;
            tempColor.a = 0.5f;
            jumpImage.color = tempColor;
        }



        if (jump.Value == true && grounded == true && canJump.Value == true && acabou == false && (joyStick.Vertical > -0.5 && !Input.GetKey(KeyCode.S)))
		{
			//playerAnimations.playerAC.SetTrigger(playerAnimations.animatorJump);
			puloAudioEvent.Play(puloSom);


        }

        if (jump.Value == true && rb2d.velocity.y < 0)
        {
            canDoubleJump = true;
        }

    }

	private void OnCollisionStay2D(Collision2D collision)
	{
		
			foreach (ContactPoint2D contact in collision.contacts)
			{
				//print(contact.collider.name + " hit " + contact.otherCollider.name);
				normal = Quaternion.Euler(contact.normal); // Quaternion.Slerp(normal, Quaternion.Euler(contact.normal), 0.5f);
				Debug.DrawRay(contact.point, contact.normal, Color.white);
			}
		
	}






	[PunRPC]
	void GiraPlayer(bool dir)
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



    //Função para o botão de pulo
    public void Jump()
    {

        if (grounded == true && joyStick.Vertical > -0.8)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Pulo", GetComponent<Transform>().position);
            jump.Value = true;
            //DogController.poderEstaAtivo = false;
            rb2d.AddForce(new Vector2(0, stats.jumpForce.Value), ForceMode2D.Impulse);
            canDoubleJump = true;
        }



        else if (canDoubleJump == true && joyStick.Vertical > -0.8)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Pulo", GetComponent<Transform>().position);
            canDoubleJump = false;
            jump.Value = false;
            Vector2 v = rb2d.velocity;
            v.y = 0;
            rb2d.velocity = v;
            rb2d.AddForce(new Vector2(0, stats.jumpForce.Value), ForceMode2D.Impulse);
        }

        else if(joyStick.Vertical < -0.8)
        {
            turnPlat.Value = true;
            canDoubleJump = true;
            canJump.Value = false;
        }
    }

       
	
    IEnumerator LevouDogada()
    {
        levouDogadaSom.Play();
		//playerAnimations.playerAC.SetBool(playerAnimations.animatorDogada, true);
        levouDogada = true;
        yield  return new WaitForSeconds(2f);
		//playerAnimations.playerAC.SetBool(playerAnimations.animatorDogada, false);
        levouDogada = false;
    }

	


}
