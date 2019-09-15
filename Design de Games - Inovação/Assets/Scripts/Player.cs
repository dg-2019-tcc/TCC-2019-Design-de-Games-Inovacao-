using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;

    public float jumpSpeed;

    public float maxSpeed = 3;

    private Rigidbody2D rb2d;

    Animator bodyAnim;

    Animator hairAnim;

    Animator torsoAnim;

    Animator legAnim;

    protected Joystick joyStick;


    public Transform groundCheck;

    public bool grounded;



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

        float moveHorizontal = joyStick.Horizontal + Input.GetAxisRaw("Horizontal");

        rb2d.AddForce((Vector2.right * speed) * moveHorizontal);

        if(rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        if (moveHorizontal != 0)
        {

            bodyAnim.SetBool("iswalking", true);
            bodyAnim.SetFloat("input_x", moveHorizontal);
            hairAnim.SetBool("iswalking", true);
            hairAnim.SetFloat("input_x", moveHorizontal);
            torsoAnim.SetBool("iswalking", true);
            torsoAnim.SetFloat("input_x", moveHorizontal);
            legAnim.SetBool("iswalking", true);
            legAnim.SetFloat("input_x", moveHorizontal);
            //bodyAnim.SetFloat("input_y", move.z);

        }

        else
        {
            bodyAnim.SetBool("iswalking", false);
            hairAnim.SetBool("iswalking", false);
            torsoAnim.SetBool("iswalking", false);
            legAnim.SetBool("iswalking", false);

        }



            if (joyStick.Vertical > 0.5  && grounded == true)
            {
                rb2d.AddForce(Vector2.up * jumpSpeed);
                //Physics.IgnoreLayerCollision(10, 11, true);

            }
    }
}

