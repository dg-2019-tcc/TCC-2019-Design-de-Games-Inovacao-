using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class DragaoPlataform : MonoBehaviour
{
    private PlatformEffector2D effector;

    public Joystick joyStick;

    public bool turnPlataforma;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        joyStick = FindObjectOfType<Joystick>();

    }

    private void Update()
    {


        if (turnPlataforma == true)
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
        if (col.CompareTag("GroundCheck") && joyStick.Vertical <= -0.5 || Input.GetKeyDown(KeyCode.S))
        {
            turnPlataforma = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GroundCheck"))
        {
            collision.GetComponent<BoxCollider2D>().transform.parent.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GroundCheck"))
        {
            collision.GetComponent<BoxCollider2D>().transform.parent.SetParent(null);
        }
    }







    IEnumerator PlatDown()
    {
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(1f);
        effector.rotationalOffset = 0f;
        turnPlataforma = false;
    }
}
