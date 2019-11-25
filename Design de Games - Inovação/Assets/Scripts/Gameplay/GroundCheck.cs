using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerMovement player;

    protected Joystick joystick;

    public Animator playerAC;
    public Rigidbody2D playerRB;

    public BoolVariable canJump;

    public AudioSource caiuSom;

    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerMovement>();
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma") || col.CompareTag("Dragao"))
        {
            player.grounded = true;
            playerAC.SetBool("onFloor", true);
            playerAC.ResetTrigger("Jump");
            caiuSom.Play();
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if ((col.CompareTag("Plataforma") || col.CompareTag("Dragao")) && playerRB.velocity.y <= 0)
        {
            playerAC.SetBool("isGrounded", true);
            playerAC.SetBool("Falling", false);
            playerAC.SetBool("onFloor", true);
            player.grounded = true;
            canJump.Value = true;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma") || col.CompareTag("Dragao"))
        {
            player.grounded = false;
            playerAC.SetBool("isGrounded", false);
            playerAC.SetBool("onFloor", false);
            canJump.Value = false;
        }
    }
}
