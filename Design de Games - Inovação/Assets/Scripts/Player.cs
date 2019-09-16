using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;

    public float jumpSpeed;

    public float maxSpeed = 8;

    private Rigidbody2D rb2d;

    Animator bodyAnim;

    Animator hairAnim;

    Animator torsoAnim;

    Animator legAnim;

    protected Joystick joyStick;


    public Transform groundCheck;

    public bool grounded;


    public bool pipa;
    public float pipaForce;
    public GameObject pipaObj;

    public bool carrinho;
    public float carrinhoSpeed;
    public GameObject carrinhoObj;



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



            if (joyStick.Vertical > 0.5 || Input.GetKeyDown(KeyCode.W)  && grounded == true)
            {
                rb2d.AddForce(Vector2.up * jumpSpeed);
                //Physics.IgnoreLayerCollision(10, 11, true);

            }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pipa == false)
            {
                pipa = true;
                
            }

            else
            {
                pipa = false;
            }
        }

        if (pipa == true)
        {
            rb2d.AddForce(new Vector2(0, pipaForce), ForceMode2D.Impulse);
            pipaObj.SetActive(true);
            carrinho = false;
        }

        else
        {
            pipaObj.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(carrinho == false)
            {
                carrinho = true;
                
            }

            else
            {
                carrinho = false;
            }
        }

        if(carrinho == true)
        {
            maxSpeed = carrinhoSpeed;
            carrinhoObj.SetActive(true);
            pipa = false;
        }

        else
        {
            maxSpeed = 8;
            carrinhoObj.SetActive(false);
        }
    }
}

