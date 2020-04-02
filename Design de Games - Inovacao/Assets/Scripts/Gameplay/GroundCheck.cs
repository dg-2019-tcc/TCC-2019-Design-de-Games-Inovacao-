using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GroundCheck : MonoBehaviour
{
    private PlayerMovement player;

    protected Joystick joystick;

    public Animator playerAC;
    public Rigidbody2D playerRB;

    public BoolVariable canJump;
	public BoolVariable jump;

    public AudioSource caiuSom;

    public float jumpCooldown;

    public GameObject jumpButton;

    public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;

    void Start()
    {
		if (PhotonNetwork.InRoom && !GetComponentInParent<PhotonView>().IsMine)
		{
			transform.gameObject.SetActive(false);
		}
        player = gameObject.GetComponentInParent<PlayerMovement>();
		jump = Resources.Load<BoolVariable>("Jump");
	}



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma") || col.CompareTag("Dragao"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Queda", GetComponent<Transform>().position);
            player.grounded = true;
            /*playerAC.SetBool("onFloor", true);
            playerAC.ResetTrigger("Jump");*/
            jumpButton.SetActive(true);
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if ((col.CompareTag("Plataforma") || col.CompareTag("Dragao")))
        {
            /*playerAC.SetBool("isGrounded", true);
            playerAC.SetBool("Falling", false);
            playerAC.SetBool("onFloor", true);*/
            player.grounded = true;
            jumpCooldown += Time.deltaTime;

            if (jumpCooldown >= 0.3f)
            {
                jumpButton.SetActive(true);
                canJump.Value = true;
                jump.Value = true;
            }
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma") || col.CompareTag("Dragao"))
        { 
            jumpCooldown = 0;
            player.grounded = false;
            /*playerAC.SetBool("isGrounded", false);
            playerAC.SetBool("onFloor", false);*/
            canJump.Value = false;
			//jump.Value = false;
        }
    }
}
