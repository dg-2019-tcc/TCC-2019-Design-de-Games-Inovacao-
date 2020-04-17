using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGroundCheck : MonoBehaviour
{

    public StateController controller;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma") || col.CompareTag("Dragao"))
        {
            
            controller.jumpCooldown += Time.deltaTime;

            if (controller.jumpCooldown >= 2f)
            {
                controller.canJump = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma") || col.CompareTag("Dragao"))
        {
            controller.jumpCooldown = 0f;
            controller.canJump = false;
        }
    }
}
