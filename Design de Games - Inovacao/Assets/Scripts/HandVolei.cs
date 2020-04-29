using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVolei : MonoBehaviour
{
    private PlayerThings player;

    public float cooldownKick;

    public float corteForceX;
    public float corteForceY;

    public float superForceX;
    public float superForceY;

    private float forceVertical;

    public static bool cortou;

    private bool rightDir;

    private Rigidbody2D ballrb;

    private GameObject bola;

    private BolaVolei bolaVolei;

    public Joystick joyStick;

    public TriggerCollisionsController triggerController;

    public Controller2D controller;

    public Player2DAnimations anim;


    public void Start()
    {
        if (GetComponent<PhotonView>().IsMine || PhotonNetwork.InRoom)
        {
            joyStick = FindObjectOfType<Joystick>();
        }

        bola = GameObject.FindWithTag("Volei");
        bolaVolei = bola.GetComponent<BolaVolei>();
        ballrb = bola.GetComponent<Rigidbody2D>();

        player = gameObject.GetComponentInParent<PlayerThings>();
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

        if(triggerController.collisions.tocouBola == true)
        {
            gameObject.GetComponent<PhotonView>().RPC("BateBola", RpcTarget.MasterClient);
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
        gameObject.GetComponent<PhotonView>().RPC("CortouBall", RpcTarget.MasterClient, corteForceY);
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
        anim.DogButtonAnim(cortou);

        yield return new WaitForSeconds(cooldownKick);
        cortou = false;
        anim.DogButtonAnim(cortou);
    }

    [PunRPC]
    public void BateBola()
    {
        ballrb.AddForce(new Vector2(corteForceX, 15), ForceMode2D.Impulse);
    }


    [PunRPC]
    public void CortaBola()
    { 
        bolaVolei.corte = true;
        //ballrb.velocity = new Vector2(0, 0);
        ballrb.AddForce(new Vector2(corteForceX, corteForceY), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperCortaBola()
    {

        Debug.Log("SuperCortaBola");
        /*if (joyStick.Vertical != 0)
        {
            forceVertical = corteForceY * joyStick.Vertical * 2;
            Debug.Log(forceVertical);
        }

        else
        {
            forceVertical = 50f;
        }*/
        bolaVolei.superCorte = true;
        ballrb.AddForce(new Vector2(superForceX, superForceY), ForceMode2D.Impulse);
    }

}
