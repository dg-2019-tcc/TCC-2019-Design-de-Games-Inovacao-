using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Player player;

    protected Joystick joystick;

     void Start()
    {
        player = gameObject.GetComponentInParent<Player>();

    }


    private void Update()
    {
        if (player.jump == false)
        {
            StopCoroutine("ResataPulo");    
        }
    }




    void OnTriggerEnter2D(Collider2D col)
    {
		if(!col.CompareTag("Coletavel"))
        player.grounded = true;
        StartCoroutine("ResetaPulo");

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

    IEnumerator ResetaPulo()
    {
        player.jump = true;
        yield return new WaitForSeconds(0.5f);
        player.jump = false;
    }

    
}
