using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float numeroDeColetaveis;

	[HideInInspector]
    public float coletavel;


    public float speed;
    public float jumpSpeed;
    public float maxSpeed = 8;
    private Rigidbody2D rb2d;
    public bool jump;


   // private Vector3 oldPosition;

    public GameObject player;


    protected Joystick joyStick;
    protected FixedButton fixedButton;


    public Transform groundCheck;
    public bool grounded;


	public GameObject Pet;


	public bool pipa;
    public float pipaForce;
    public GameObject pipaObj;


    public bool carrinho;
    public float carrinhoSpeed;
    public GameObject carrinhoObj;

    [HideInInspector]
	public PhotonView PV;
	private CinemachineConfiner CC;
	private CinemachineVirtualCamera VC;

    public GameObject dogSpawn;
    public float dogCount;

    public bool desativa;
	





	void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        joyStick = FindObjectOfType<Joystick>();

        fixedButton = FindObjectOfType<FixedButton>();

		PV = GetComponent<PhotonView>();

		if (PV != null && PV.IsMine)
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

		if (PhotonNetwork.IsConnected == true && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
			FindObjectOfType<Coroa>().ganhador = transform;
			transform.position = new Vector3(0, 0, 0);
		}

		
	}

  
    void FixedUpdate()
    {



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
		}



        if (coletavel >= numeroDeColetaveis)
        {
			PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

            gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

            gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
			coletavel = 0;
			

		}


        if (PV != null && !PV.IsMine) return;

		//Vector2 move = new Vector2(joyStick.Horizontal + Input.GetAxisRaw("Horizontal"), 0);


        //Movimentação do player no joystick
		float moveHorizontal = joyStick.Horizontal + Input.GetAxisRaw("Horizontal");

        rb2d.AddForce((Vector2.right * speed) * moveHorizontal);

        if(rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

       
        // Pulo
        if (grounded == true && jump == true)
            {
                carrinho = false;
                rb2d.AddForce(Vector2.up * jumpSpeed);
            //Physics.IgnoreLayerCollision(10, 11, true);
                jump = false;

        }

        // Desativando transformações
        if(desativa == true)
        {
            pipa = false;
            carrinho = false;
            maxSpeed = 8;
            dogCount = 0;
            desativa = false;
        }

        //Colocando tempo limite das tranformações
        if(dogCount >= 2f)
        {
            desativa = true;
        }

        //Transformação em pipa
        if (pipa == true)
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
        if(carrinho == true && jump == false)
        {
            dogCount += Time.deltaTime;
            rb2d.AddForce(-Vector2.up * carrinhoSpeed);
            maxSpeed = carrinhoSpeed;
            carrinhoObj.SetActive(true);
			pipa = false;
        }

		if (carrinho == false && !Pet.activeSelf)
		{
            carrinhoObj.SetActive(false);
			
		}


		if (carrinho == false && pipa == false)
		{
			TransformaPet(true, "carrinho");
			

		}

		else
		{
			TransformaPet(false, "carrinho");
			Pet.transform.position = dogSpawn.transform.position;
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
            }
        }

        if (collision.CompareTag("Pipa"))
        {
            pipa = true;
        }

        if (collision.CompareTag("Carrinho"))
        {
            carrinho = true;
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
}

