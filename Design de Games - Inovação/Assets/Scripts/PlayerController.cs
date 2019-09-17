using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool grounded = false;
    private Rigidbody rb;

    protected Joystick joyStick;

    private Animator bodyAnim;
    private Animator hairAnim;
    private Animator torsoAnim;
    private Animator legsAnim;

    public Transform groundCheck;

    public bool pipa;
    public float pipaForce;
    public GameObject pipaObj;

	public bool carrinho;
	public float carrinhoSpeed;
	public GameObject carrinhoObj;

	public GameObject Pet;

	private bool podeTrocarEixo;
	private Transform eixoPosicao;
	private float outroAngulo;
	private bool gira;
	

    private Vector3 move;

	private PhotonView PV;
	private CinemachineVirtualCamera VC;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();

        joyStick = FindObjectOfType<Joystick>();

        bodyAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();

        hairAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();

        torsoAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();

        legsAnim = gameObject.transform.GetChild(3).GetComponent<Animator>();

		outroAngulo = -90;

		PV = GetComponent<PhotonView>();

		if (PV != null && !PV.IsMine)
		{
			VC = gameObject.transform.GetChild(4).GetComponent<CinemachineVirtualCamera>();
			VC.Priority = 15;
		}

    }

    // Update is called once per frame
    void FixedUpdate()
    {

		if (PV != null && !PV.IsMine) return;

            move = new Vector3(joyStick.Horizontal + Input.GetAxisRaw("Horizontal"), 0, 0/*joyStick.Vertical + Input.GetAxisRaw("Vertical")*/);

        


        //Vector3 jump = new Vector3(0, joyStick.Vertical,  0);

        //Debug.Log(move);

        //Debug.Log(jump);

        if (move != Vector3.zero)
        {
            torsoAnim.SetBool("iswalking", true);
            torsoAnim.SetFloat("input_x", move.x);
            torsoAnim.SetFloat("input_y", move.z);

            hairAnim.SetBool("iswalking", true);
            hairAnim.SetFloat("input_x", move.x);
            hairAnim.SetFloat("input_y", move.z);

            bodyAnim.SetBool("iswalking", true);
            bodyAnim.SetFloat("input_x", move.x);
            bodyAnim.SetFloat("input_y", move.z);

            legsAnim.SetBool("iswalking", true);
            legsAnim.SetFloat("input_x", move.x);
            legsAnim.SetFloat("input_y", move.z);
        }

        else
        {
            torsoAnim.SetBool("iswalking", false);
            hairAnim.SetBool("iswalking", false);
            bodyAnim.SetBool("iswalking", false);
            legsAnim.SetBool("iswalking", false);
        }

        rb.MovePosition(transform.rotation * ((rb.position + move * Time.deltaTime * speed) - transform.position) + transform.position); // agora com um sisteminha que vai fazer ele andar na direção local, e não mais global(da pra girar o player à vontade que ele vai funcionar de boas)


		if (Input.GetKeyDown(KeyCode.X) && podeTrocarEixo && !gira)
		{
			gira = true;

		}

		if (gira)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, outroAngulo, 0), 0.1f);
			transform.position = Vector3.Lerp(transform.position, eixoPosicao.position, 0.1f);

			if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, outroAngulo, 0)) < 10)
			{
				transform.rotation = Quaternion.Euler(0, outroAngulo, 0);
				transform.position = eixoPosicao.position;

				if (outroAngulo == -90)
				{
					outroAngulo = 0;
				}
				else
				{
					outroAngulo = -90;
				}

				gira = false;
			}
		}
		

        if (Input.GetKeyDown(KeyCode.Space))
        {
			if (pipa == false)
			{
				pipa = true;

			}
			else
			{
				pipa = false;
			}

        }

        if (pipa == true)
        {
            rb.AddForce(new Vector3(0, pipaForce, 0), ForceMode.Impulse);
            pipaObj.SetActive(true);
		}

        if (pipa == false && !Pet.activeSelf)
        {
            pipaObj.SetActive(false);
		}

		if (rb.velocity.y < 0 && grounded == true)
		{
			carrinho = true;
		}

		else
		{
			carrinho = false;
		}

		if (carrinho == true)
		{

			rb.AddForce(-Vector2.up * carrinhoSpeed);
		//	maxSpeed = carrinhoSpeed;
			carrinhoObj.SetActive(true);
			pipa = false;
		}

		else
		{
		//	maxSpeed = 8;
			carrinhoObj.SetActive(false);
		}

		if (carrinho == false && pipa == false)
		{
			TransformaPet(true, "carrinho");


		}

		else
		{
			TransformaPet(false, "carrinho");
			Pet.transform.position = transform.position;
		}


		/*RaycastHit hit;

        if (Physics.Raycast(groundCheck.transform.position, -Vector3.up, out hit))
        {

            if (joyStick.Vertical > 0.5 && hit.distance <= 0.3f)
            {
                rb.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);

            }

            Debug.Log(hit.distance);
        }*/
	}


	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("TrocaEixo"))
		{
			podeTrocarEixo = true;
			eixoPosicao = collision.gameObject.transform;
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.CompareTag("TrocaEixo"))
		{
			podeTrocarEixo = false;
		}
	}


	private void TransformaPet(bool isDog, string transformation)
	{
		Pet.SetActive(isDog);
	}

	

	
}
