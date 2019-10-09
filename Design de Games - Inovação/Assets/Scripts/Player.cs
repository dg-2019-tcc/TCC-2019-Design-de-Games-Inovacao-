using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    Animator bodyAnim;
    Animator hairAnim;
    Animator torsoAnim;
    Animator legAnim;

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


	private PhotonView PV;
	private CinemachineVirtualCamera VC;

    public GameObject dogSpawn;
	





	void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        joyStick = FindObjectOfType<Joystick>();

        fixedButton = FindObjectOfType<FixedButton>();

       /* bodyAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();

        hairAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();

        torsoAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();

        legAnim = gameObject.transform.GetChild(3).GetComponent<Animator>();*/

		PV = GetComponent<PhotonView>();

		if (PV != null && PV.IsMine)
		{
			VC = gameObject.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
			VC.Priority = 15;

            rb2d.gravityScale = 1;
			
		}

		if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
			FindObjectOfType<Coroa>().ganhador = transform;
		}
	}

  
    void FixedUpdate()
    {

       

        if (joyStick.Horizontal > 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 90, 0);

        }
        else if(joyStick.Horizontal < 0)
        {
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
        }



        if (coletavel >= numeroDeColetaveis)
        {
			PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
			gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
			coletavel = 0;
			

		}


        if (PV != null && !PV.IsMine) return;

		//Vector2 move = new Vector2(joyStick.Horizontal + Input.GetAxisRaw("Horizontal"), 0);

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

        if (moveHorizontal != 0)
        {

           /* bodyAnim.SetBool("iswalking", true);
            bodyAnim.SetFloat("input_x", moveHorizontal);
            hairAnim.SetBool("iswalking", true);
            hairAnim.SetFloat("input_x", moveHorizontal);
            torsoAnim.SetBool("iswalking", true);
            torsoAnim.SetFloat("input_x", moveHorizontal);
            legAnim.SetBool("iswalking", true);
            legAnim.SetFloat("input_x", moveHorizontal);
            //bodyAnim.SetFloat("input_y", move.z);*/

        }

        else
        {
            /*bodyAnim.SetBool("iswalking", false);
            hairAnim.SetBool("iswalking", false);
            torsoAnim.SetBool("iswalking", false);
            legAnim.SetBool("iswalking", false);*/

        }



            if (joyStick.Vertical >= 0.7 && grounded == true && jump == false)
            {
                carrinho = false;
                rb2d.AddForce(Vector2.up * jumpSpeed);
            //Physics.IgnoreLayerCollision(10, 11, true);
                jump = true;

            }



        if (Input.GetKeyDown(KeyCode.Space) || fixedButton.Pressed)
        {
            pipa = true;
        }

        else
        {
            pipa = false;
        }

        if (pipa == true)
        {
            rb2d.AddForce(new Vector2(0, pipaForce), ForceMode2D.Impulse);
            pipaObj.SetActive(true);
			carrinho = false;
        }

		if (pipa == false && !Pet.activeSelf)
		{
            pipaObj.SetActive(false);
			Pet.transform.position = dogSpawn.transform.position;
		}

        if (rb2d.velocity.y < 0 && grounded == true )
        {
            carrinho = true;
			
		}

        else
        {
            carrinho = false;
			
		}

        if(carrinho == true && jump == false)
        {

            rb2d.AddForce(-Vector2.up * carrinhoSpeed);
            maxSpeed = carrinhoSpeed;
            carrinhoObj.SetActive(true);
			pipa = false;
        }

		if (carrinho == false && !Pet.activeSelf)
		{
            maxSpeed = 8;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            if (PV.IsMine == true)
            {
                coletavel++;
            }
        }
    }


    private void TransformaPet(bool isDog, string transformation)
	{
		Pet.SetActive(isDog);
	}
}

