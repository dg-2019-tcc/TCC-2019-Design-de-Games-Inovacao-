using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private bool grounded = false;
    private Rigidbody rb;

    protected Joystick joyStick;

    private Animator bodyAnim;
    private Animator hairAnim;
    private Animator torsoAnim;
    private Animator legsAnim;

    public Transform groundCheck;

    public bool pipa;
    public float pipaForce;
    public GameObject pipaObj;

    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        joyStick = FindObjectOfType<Joystick>();

        bodyAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();

        hairAnim = gameObject.transform.GetChild(1).GetComponent<Animator>();

        torsoAnim = gameObject.transform.GetChild(2).GetComponent<Animator>();

        legsAnim = gameObject.transform.GetChild(3).GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

            move = new Vector3(joyStick.Horizontal + Input.GetAxisRaw("Horizontal"), 0, joyStick.Vertical + Input.GetAxisRaw("Vertical"));

        


        //Vector3 jump = new Vector3(0, joyStick.Vertical,  0);

        //Debug.Log(move);

        //Debug.Log(jump);

        if (move != Vector3.zero)
        {
            torsoAnim.SetBool("iswalking", true);
            torsoAnim.SetFloat("input_x", move.x);
            torsoAnim.SetFloat("input_y", move.z);

            hairAnim.SetBool("iswalking", true);
            hairAnim.SetFloat("input_x", move.x);
            hairAnim.SetFloat("input_y", move.z);

            bodyAnim.SetBool("iswalking", true);
            bodyAnim.SetFloat("input_x", move.x);
            bodyAnim.SetFloat("input_y", move.z);

            legsAnim.SetBool("iswalking", true);
            legsAnim.SetFloat("input_x", move.x);
            legsAnim.SetFloat("input_y", move.z);
        }

        else
        {
            torsoAnim.SetBool("iswalking", false);
            hairAnim.SetBool("iswalking", false);
            bodyAnim.SetBool("iswalking", false);
            legsAnim.SetBool("iswalking", false);
        }

        rb.MovePosition(rb.position + move * Time.deltaTime * speed);

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
            rb.AddForce(new Vector3(0, pipaForce, 0), ForceMode.Impulse);
            pipaObj.SetActive(true);
        }

        else
        {
            pipaObj.SetActive(false);
        }

        /*RaycastHit hit;

        if (Physics.Raycast(groundCheck.transform.position, -Vector3.up, out hit))
        {

            if (joyStick.Vertical > 0.5 && hit.distance <= 0.3f)
            {
                rb.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);

            }

            Debug.Log(hit.distance);
        }*/
    }
}
