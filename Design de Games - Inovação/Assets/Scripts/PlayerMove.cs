using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    Animator torsoAnim;
    Animator hairAnim;
    Animator bodyAnim;
    Animator legsAnim;

    protected Joystick joyStick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        torsoAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        hairAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();
        bodyAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();
        legsAnim = gameObject.transform.GetChild(3).GetComponent<Animator>();

        joyStick = FindObjectOfType<Joystick>(); 
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        if(move != Vector2.zero)
        {
            torsoAnim.SetBool("iswalking", true);
            torsoAnim.SetFloat("input_x", move.x);
            torsoAnim.SetFloat("input_y", move.y);

            hairAnim.SetBool("iswalking", true);
            hairAnim.SetFloat("input_x", move.x);
            hairAnim.SetFloat("input_y", move.y);

            bodyAnim.SetBool("iswalking", true);
            bodyAnim.SetFloat("input_x", move.x);
            bodyAnim.SetFloat("input_y", move.y);

            legsAnim.SetBool("iswalking", true);
            legsAnim.SetFloat("input_x", move.x);
            legsAnim.SetFloat("input_y", move.y);
        }

        else
        {
            torsoAnim.SetBool("iswalking", false);
            hairAnim.SetBool("iswalking", false);
            bodyAnim.SetBool("iswalking", false);
            legsAnim.SetBool("iswalking", false);
        }

        rb.MovePosition(rb.position + move * Time.deltaTime);
    }
}
