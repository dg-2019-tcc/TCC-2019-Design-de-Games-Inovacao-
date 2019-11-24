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
    public GameObject wolf;
	private Rigidbody2D rb;
    public float followSpeed;
    public RaycastHit shot;
    Animator wolfAnim;


	private bool vitoria = false;
	private bool menuCustom;


	private Vector3 oldPosition;

    public Animator dogAC;



	void Start()
	{
		wolfAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		pv = GetComponent<PhotonView>();

		if (SceneManager.GetActiveScene().name == "TelaVitoria")
			vitoria = true;


		menuCustom = false;

		if (SceneManager.GetActiveScene().name == "MenuCustomizacao") menuCustom = true;


		if (vitoria && (int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
			transform.position = player.transform.position;

		if (!pv.IsMine && !menuCustom) rb.isKinematic = true;
	}


    // Update is called once per frame
    void Update()
    {
		if (!pv.IsMine && !menuCustom) return;
        
        if (player != null && Vector3.Distance(transform.position, player.transform.position) > allowedDistance)
        {
            followSpeed = 0.1f;
            //wolfAnim.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
            dogAC.SetBool("isWalking", true);




            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            else if (transform.position.x < player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

			if (rb.velocity.y == 0 && transform.position.y + allowedDistance < player.transform.position.y)
			{
				rb.AddForce(Vector2.up * 300);
			}
			


		}


        else
        {

            followSpeed = 0;
            //wolfAnim.SetBool("isWalking", false);

        }



		if (rb.velocity.y == 0 && vitoria)
		{
			rb.AddForce(Vector2.up * 100);
		} 

    }

}
