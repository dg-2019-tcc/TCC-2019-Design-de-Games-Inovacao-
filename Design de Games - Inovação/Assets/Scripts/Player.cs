using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;

    public float jumpSpeed;

    private Rigidbody2D rb2d;

    Animator bodyAnim;

    Animator hairAnim;

    Animator torsoAnim;

    Animator legAnim;

    protected Joystick joyStick;

    public bool jump;

    Vector2 jumpForce;

    public bool landed;

    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        joyStick = FindObjectOfType<Joystick>();

        bodyAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();

        hairAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();

        torsoAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();

        legAnim = gameObject.transform.GetChild(3).GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {

        //Vector2 move = new Vector2(joyStick.Horizontal + Input.GetAxisRaw("Horizontal"), 0);

        jumpForce = new Vector2(0, joyStick.Vertical + Input.GetAxisRaw("Vertical"));

        float moveHorizontal = joyStick.Horizontal + Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(moveHorizontal, 0);

        rb2d.MovePosition(rb2d.position + move * Time.deltaTime * 5);

        if (move != Vector2.zero)
        {

            bodyAnim.SetBool("iswalking", true);
            bodyAnim.SetFloat("input_x", move.x);
            hairAnim.SetBool("iswalking", true);
            hairAnim.SetFloat("input_x", move.x);
            torsoAnim.SetBool("iswalking", true);
            torsoAnim.SetFloat("input_x", move.x);
            legAnim.SetBool("iswalking", true);
            legAnim.SetFloat("input_x", move.x);
            //bodyAnim.SetFloat("input_y", move.z);

        }

        else
        {
            bodyAnim.SetBool("iswalking", false);
            hairAnim.SetBool("iswalking", false);
            torsoAnim.SetBool("iswalking", false);
            legAnim.SetBool("iswalking", false);

        }


        if (jumpForce != Vector2.zero && jump == false)
        {
            jump = true;
        }

        if (jump == true && landed == true)
        {
            jumpSpeed = 2;
            StartCoroutine("Jump");
        }


        if(jump == false)
        {
            jumpSpeed = 0;
            StopCoroutine("Jump");
        }





    }


    IEnumerator Jump()
    {
        rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        jump = false;
    }
}

