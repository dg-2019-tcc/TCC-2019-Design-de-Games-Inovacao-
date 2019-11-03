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
    [SerializeField]
    private float numeroDeColetaveis;

    [HideInInspector]
    public static float coletavel;


    public float speed;
    public float jumpSpeed;
    public float maxSpeed = 8;
    private Rigidbody2D rb2d;
    public bool jump;


    // private Vector3 oldPosition;

    public GameObject player;


    protected Joystick joyStick;
    protected FixedButton fixedButton;
    public GameObject canvasSelf;


    public Transform groundCheck;
    public bool grounded;


    public GameObject Pet;


    public bool pipa;
    public static bool dogPipa;
    public float pipaForce;
    public GameObject pipaObj;


    public bool carrinho;
    public static bool dogCarro;
    public float carrinhoSpeed;
    public GameObject carrinhoObj;

    [HideInInspector]
    public PhotonView PV;
    private CinemachineConfiner CC;
    private CinemachineVirtualCamera VC;

    public GameObject dogSpawn;
    public float dogCount;

    public bool desativa;

    public AudioSource puloSom;
    public GameObject walkSom;
    public AudioSource tokenSom;
    public AudioSource coleta;

    public Animator playerAC;


    public float speedToTotem = 10.0f;

    public static bool acertouTotem;

    private Transform target;

    public bool levouDogada;

    public static bool atirou;

    [SerializeField]
    private bool tutorial;


    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        joyStick = FindObjectOfType<Joystick>();

        fixedButton = FindObjectOfType<FixedButton>();

        PV = GetComponent<PhotonView>();

        if (PV != null && PV.IsMine || tutorial == true)
        {
            VC = gameObject.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            VC.Priority = 15;

            if (joyStick.isActiveAndEnabled)
            {
                CC = gameObject.transform.GetChild(0).GetComponent<CinemachineConfiner>();
                CC.m_BoundingShape2D = GameObject.Find("CameraConfiner").GetComponent<PolygonCollider2D>();
                CC.InvalidatePathCache();
            }

            rb2d.gravityScale = 1;

        }
        else
        {
            canvasSelf.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "TelaVitoria" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
        {
            FindObjectOfType<Coroa>().ganhador = transform;
            transform.position = new Vector3(0, 0, 0);
        }

        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;

    }


    void FixedUpdate()
    {

        if (coletavel >= numeroDeColetaveis)
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

            gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

            gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
            coletavel = 0;


        }


        if (PV != null && !PV.IsMine) return;

		//Vector2 move = new Vector2(joyStick.Horizontal + Input.GetAxisRaw("Horizontal"), 0);

		if (joyStick != null)
		{
			if (joyStick.Horizontal > 0)
			{
				player.transform.rotation = Quaternion.Euler(0, 90, 0);

			}
			else if (joyStick.Horizontal < 0)
			{
				player.transform.rotation = Quaternion.Euler(0, -90, 0);
			}



			//Movimentação do player no joystick
			float moveHorizontal = joyStick.Horizontal + Input.GetAxisRaw("Horizontal");


			if (moveHorizontal != 0 && levouDogada == false)
			{

				rb2d.velocity = new Vector3(speed * moveHorizontal, rb2d.velocity.y, 0);
				playerAC.SetBool("isWalking", true);

				//walkSom.SetActive(true);
			}

			else
			{
				playerAC.SetBool("isWalking", false);
			}

		}
        // Pulo
        if (grounded == true && jump == true)
        {
            carrinho = false;
            rb2d.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            //Physics.IgnoreLayerCollision(10, 11, true);
            jump = false;
            puloSom.Play();

        }

        // Desativando transformações
        if (desativa == true)
        {
            pipa = false;
            dogPipa = false;
            carrinho = false;
            dogCarro = false;
            speed = 2.5f;
            dogCount = 0;
            desativa = false;
        }

        //Colocando tempo limite das tranformações
        if (dogCount >= 2f)
        {
            desativa = true;
        }

        //Transformação em pipa
        if (pipa == true || dogPipa == true)
        {
            dogCount += Time.deltaTime;
            rb2d.AddForce(new Vector2(0, pipaForce), ForceMode2D.Impulse);
            pipaObj.SetActive(true);
            carrinho = false;
            maxSpeed = 4;
        }

        if (pipa == false && !Pet.activeSelf)
        {
            pipaObj.SetActive(false);
            Pet.transform.position = dogSpawn.transform.position;
        }

        // Transformação em carrinho
        if (carrinho == true && jump == false || dogCarro == true)
        {
            dogCount += Time.deltaTime;
            //rb2d.AddForce(-Vector2.up * carrinhoSpeed);
            speed = carrinhoSpeed;
            carrinhoObj.SetActive(true);
            pipa = false;
        }

        if (carrinho == false && !Pet.activeSelf)
        {
            carrinhoObj.SetActive(false);

        }


        if (carrinho == false && pipa == false && dogCarro == false && dogPipa == false && atirou == false)
        {
            TransformaPet(true, "carrinho");


        }

        else
        {
            TransformaPet(false, "carrinho");
            Pet.transform.position = dogSpawn.transform.position;
        }

        if (acertouTotem == true)
        {
            target = ItemThrow.totemTarget;

            float step = speedToTotem * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                acertouTotem = false;
            }
        }

        if (levouDogada)
        {
            StartCoroutine("LevouDogada");
        }

        else
        {
            StopCoroutine("LevouDogada");
        }




        //Debug.Log(rb2d.velocity);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            if (PV.IsMine == true)
            {
                coletavel++;
                coleta.Play();
            }
        }

        if (collision.CompareTag("Pipa"))
        {
            pipa = true;
            tokenSom.Play();
        }

        if (collision.CompareTag("Carrinho"))
        {
            carrinho = true;
            tokenSom.Play();
        }
    }


    private void TransformaPet(bool isDog, string transformation)
    {
        Pet.SetActive(isDog);
    }


    //Função para o botão de pulo
    public void Jump()
    {
        jump = true;
        if (pipa == true || carrinho == true)
        {
            desativa = true;
        }

    }

    IEnumerator LevouDogada()
    {
        playerAC.SetBool("Dogada", true);
        
        yield  return new WaitForSeconds(2f);
        playerAC.SetBool("Dogada", false);
        levouDogada = false;
    }
}
