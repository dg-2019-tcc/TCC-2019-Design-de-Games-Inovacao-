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
    public float speed;
    public float jumpSpeed;
    public float maxSpeed = 8;
    private Rigidbody2D rb2d;
    public bool jump;
    public Transform groundCheck;
    public bool grounded;



    [Header("Canvas")]

    [SerializeField]
    protected Joystick joyStick;
    protected FixedButton fixedButton;
    public GameObject canvasSelf;
	public GameObject canvasPause;
    [SerializeField]
    private bool desativaCanvas;



    [Header("Pet")]

    public GameObject Pet;
    public GameObject dogSpawn;
    public float dogCount;
    private Transform target;
    public bool levouDogada;
    public static bool atirou;
    public float speedToTotem = 10.0f;
    public static bool acertouTotem;
    [HideInInspector]
    public bool desativaTransformacao;



    [Header("Pet pipa")]

    public bool pipa;
    public static bool dogPipa;
    public float pipaForce;
    public GameObject pipaObj;



    [Header("Pet carrinho")]
    
    public bool carrinho;
    public static bool dogCarro;
    public float carrinhoSpeed;
    public GameObject carrinhoObj;



    //[Header("Photon")]

    [HideInInspector]
    public PhotonView PV;



    //[Header("Cinemachine")]

    private CinemachineConfiner CC;
    private CinemachineVirtualCamera VC;
    



    [Header("Som")]

    public AudioSource puloSom;
    public GameObject walkSom;
    public AudioSource tokenSom;
    public AudioSource coleta;



    [Header("Animação")]

    public Animator playerAC;



    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        joyStick = FindObjectOfType<Joystick>();
        fixedButton = FindObjectOfType<FixedButton>();
        PV = GetComponent<PhotonView>();
		



		if (desativaCanvas == true)
        {
            canvasSelf.SetActive(false);
        }


        if (PV != null && PV.IsMine)
        {
            VC = gameObject.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            VC.Priority = 15;
			PV.Owner.CustomProperties["atirou"] = atirou;
			PV.Owner.CustomProperties["dogPipa"] = dogPipa;
			PV.Owner.CustomProperties["dogCarro"] = dogCarro;
			PV.Owner.CustomProperties["acertouTotem"] = acertouTotem;

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
			rb2d.isKinematic = true;
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

		atirou = (bool)PV.Owner.CustomProperties["atirou"];
		dogPipa = (bool)PV.Owner.CustomProperties["dogPipa"];
		dogCarro = (bool)PV.Owner.CustomProperties["dogCarro"];

		acertouTotem = (bool)PV.Owner.CustomProperties["acertouTotem"];

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

        if (grounded)
        {
            playerAC.SetBool("isGrounded", true);
        }
        else
        {
            playerAC.SetBool("isGrounded", false);

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
        if (desativaTransformacao == true)
        {
            pipa = false;
            dogPipa = false;
            carrinho = false;
            dogCarro = false;
            speed = 2.5f;
            dogCount = 0;
            desativaTransformacao = false;
        }

        //Colocando tempo limite das tranformações
        if (dogCount >= 5f)
        {
            desativaTransformacao = true;
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
			gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true, "carrinho");
        }

        else
        {
			gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false, "carrinho");
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



		PV.Owner.CustomProperties["atirou"] = atirou;

		PV.Owner.CustomProperties["dogPipa"] = dogPipa;

		PV.Owner.CustomProperties["dogCarro"] = dogCarro;

		PV.Owner.CustomProperties["acertouTotem"] = acertouTotem;
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

	[PunRPC]
    private void TransformaPet(bool isDog, string transformation)
    {
        Pet.SetActive(isDog);
    }


    //Função para o botão de pulo
    public void Jump()
    {
        playerAC.SetTrigger("Jump");
        jump = true;
        if (pipa == true || carrinho == true)
        {
            desativaTransformacao = true;
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
