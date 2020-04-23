using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVolei : MonoBehaviour
{
    private PlayerMovement player;

    public float cooldownKick;

    public float corteForceX;
    public float corteForceY;

    public float superForceX;

    private float forceVertical;

    public bool cortou;

    private bool rightDir;

    private Rigidbody2D ballrb;

    private GameObject bola;

    private BolaVolei bolaVolei;

    public Joystick joyStick;

    public GameObject hand;

    public GameObject normalHand;

    public GameObject superHand;

    public TriggerCollisionsController triggerController;

    public Controller2D controller;


    public void Start()
    {
        if (GetComponent<PhotonView>().IsMine || PhotonNetwork.InRoom)
        {
            joyStick = FindObjectOfType<Joystick>();
        }

        bola = GameObject.FindWithTag("Volei");
        bolaVolei = bola.GetComponent<BolaVolei>();
        ballrb = bola.GetComponent<Rigidbody2D>();

        player = gameObject.GetComponentInParent<PlayerMovement>();
        triggerController = GetComponent<TriggerCollisionsController>();
        controller = GetComponent<Controller2D>();
    }


    public void Update()
    {

        if (joyStick != null)
        {
            if (joyStick.Horizontal > 0)
            {
                rightDir = true;
                //gameObject.GetComponent<PhotonView>().RPC("GiraHand", RpcTarget.All, rightDir);
            }

            else if (joyStick.Horizontal < 0)
            {
                rightDir = false;
                //gameObject.GetComponent<PhotonView>().RPC("GiraHand", RpcTarget.All, rightDir);
            }
        }

        if(triggerController.collisions.cortaBola == true && cortou == true && controller.collisions.above == false)
        {
            Debug.Log("SUPER");
            gameObject.GetComponent<PhotonView>().RPC("SuperCortaBola", RpcTarget.MasterClient);
        }

        if(triggerController.collisions.cortaBola == true)
        {
            Debug.Log("nORMAL");
            gameObject.GetComponent<PhotonView>().RPC("CortaBola", RpcTarget.MasterClient);
        }
    }

    public void Corte()
    {
        /*if (joyStick.Vertical != 0)
        {
            forceVertical = corteForceY * joyStick.Vertical * 2;
            Debug.Log(forceVertical);
        }

        else
        {
            forceVertical = 5f;
        }*/
        //Debug.Log(forceVertical);
        gameObject.GetComponent<PhotonView>().RPC("CortouBall", RpcTarget.MasterClient, forceVertical);
    }

    [PunRPC]
    public void CortouBall(float force)
    {
        forceVertical = force;
        if (cortou == false)
        {
            StartCoroutine("CoolHand");
        }
    }

    IEnumerator CoolHand()
    {
        cortou = true;

        yield return new WaitForSeconds(cooldownKick);
        superHand.SetActive(false);
        normalHand.SetActive(true);
        cortou = false;
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola") && cortou == true && controller.collisions.above == false)
        {
            Debug.Log("Super    CortaBola");
            gameObject.GetComponent<PhotonView>().RPC("SuperCortaBola", RpcTarget.MasterClient);
        }

        if (col.CompareTag("Bola"))
        {
            Debug.Log("CortaBola");
            ballrb = col.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<PhotonView>().RPC("CortaBola", RpcTarget.MasterClient);
        }
    }*/



    [PunRPC]
    public void CortaBola()
    { 
        bolaVolei.corte = true;
        ballrb.velocity = new Vector2(0, 0);
        ballrb.AddForce(new Vector2(corteForceX, forceVertical), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperCortaBola()
    {

        Debug.Log("SuperCortaBola");
        superHand.SetActive(true);
        normalHand.SetActive(false);
        if (joyStick.Vertical != 0)
        {
            forceVertical = corteForceY * joyStick.Vertical * 2;
            Debug.Log(forceVertical);
        }

        else
        {
            forceVertical = 5f;
        }
        bolaVolei.superCorte = true;
        ballrb.AddForce(new Vector2(superForceX, forceVertical), ForceMode2D.Impulse);
    }

    /*[PunRPC]
    void GiraHand(bool dir)
    {
        if (dir)
        {
            corteForceX = 5f;
        }

        else
        {
            corteForceX = -5f;
        }
    }*/
}
