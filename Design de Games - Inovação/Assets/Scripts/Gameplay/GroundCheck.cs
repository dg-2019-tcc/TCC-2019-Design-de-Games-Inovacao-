using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerMovement player;

    protected Joystick joystick;

    public Animator playerAC;

     void Start()
    {
        player = gameObject.GetComponentInParent<PlayerMovement>();

    }





    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Coletavel"))
        
        playerAC.SetTrigger("Fall");
        player.grounded = true;
    }

     void OnTriggerStay2D(Collider2D col)
    {
		if (!col.CompareTag("Coletavel"))
			player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
		if (!col.CompareTag("Coletavel"))
			player.grounded = false;
    }

    

    
}
