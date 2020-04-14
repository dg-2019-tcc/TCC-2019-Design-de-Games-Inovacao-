using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WolfMovement : MonoBehaviour
{
	private PhotonView pv;
    public GameObject player;
    public float targetDistance;
    public float allowedDistance = 5;
	public float jumpPower;
    public GameObject wolf;
	private Rigidbody2D rb;
   // public float followSpeed;
    public RaycastHit shot;
    Animator wolfAnim;


	private bool vitoria = false;
	private bool menuCustom;
	public bool grounded;


	private Vector3 oldPosition;

	private int dogWalking;



	void Start()
	{
		wolfAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		pv = GetComponent<PhotonView>();

		if (SceneManager.GetActiveScene().name == "TelaVitoria")
			vitoria = true;


		menuCustom = false;

		if (SceneManager.GetActiveScene().name == "HUB") menuCustom = true;


		if (vitoria && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
			transform.position = player.transform.position;

		if (!pv.IsMine && !menuCustom) rb.isKinematic = true;

		//dogWalking = Animator.StringToHash("isWalking");
	}


    // Update is called once per frame
    void FixedUpdate()
    {
		if (!pv.IsMine && !menuCustom) return;

		if (grounded &&  transform.position.y + allowedDistance/2 < player.transform.position.y)
		{
			rb.AddForce(Vector2.up * jumpPower * Mathf.Lerp((player.transform.position.y - transform.position.y), 1, 0.1f));
		}

		if (Mathf.Abs(transform.position.x - player.transform.position.x) > allowedDistance)
        {
			//followSpeed = 0.1f;
			//wolfAnim.SetBool("isWalking", true);
			//  transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
			//rb.velocity += new Vector2(player.transform.position.x - transform.position.x, rb.velocity.y-Physics2D.gravity.magnitude*Time.deltaTime);//*followSpeed;
			rb.AddForce(Vector2.right * (player.transform.position.x - transform.position.x));




            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);

            }
            else if (transform.position.x < player.transform.position.x)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 0.5f);
            }

			
			


		}


        else
        {

			rb.velocity -= rb.velocity/20;

        }



		if (rb.velocity.y == 0 && vitoria)
		{
			rb.AddForce(Vector2.up * 100);
		} 

    }


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Plataforma") || collision.gameObject.CompareTag("Dragao"))
		{
			grounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Plataforma") || collision.gameObject.CompareTag("Dragao"))
		{
			grounded = false;
		}
	}


}
