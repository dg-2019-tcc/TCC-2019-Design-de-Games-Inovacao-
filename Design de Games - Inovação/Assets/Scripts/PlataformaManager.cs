using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaManager : MonoBehaviour
{
    private PlatformEffector2D effector;

    private Joystick joyStick;

    public bool turnPlataforma;

     void Start()
    {
        effector = GetComponent<PlatformEffector2D>();

        joyStick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        if(turnPlataforma == true)
        {
            StartCoroutine("PlatDown");
        }

        else
        {
            StopAllCoroutines();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("GroundCheck") && joyStick.Vertical <= -0.5)
        {
            turnPlataforma = true;
        }
    }

    /*void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("GroundCheck"))
        {
            turnPlataforma = true;
        }
    }*/


    IEnumerator PlatDown()
    {
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(1f);
        effector.rotationalOffset = 0f;
        turnPlataforma = false;


    }
}
