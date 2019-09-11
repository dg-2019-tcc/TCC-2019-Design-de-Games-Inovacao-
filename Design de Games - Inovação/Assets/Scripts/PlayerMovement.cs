using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    Animator bodyAnim;

    Animator hairAnim;

    Animator torsoAnim;

    Animator legAnim;

    protected Joystick joyStick;

    public bool jump;

    public float playerSpeed;

    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bodyAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();

        hairAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();

        torsoAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();

        legAnim = gameObject.transform.GetChild(3).GetComponent<Animator>();

        joyStick = FindObjectOfType<Joystick>();

    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 move = new Vector2(joyStick.Horizontal  + Input.GetAxisRaw("Horizontal"), 0);

        Vector2 jumpMove = new Vector2(0, joyStick.Vertical + Input.GetAxisRaw("Vertical"));


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

        rb.MovePosition(rb.position + move * Time.deltaTime * 5);

        if (jumpMove != Vector2.zero)
        {
            jump = true;
        }

        if (jump)
        {
            rb.MovePosition(rb.position + jumpMove * 5);
        }

    }
}
