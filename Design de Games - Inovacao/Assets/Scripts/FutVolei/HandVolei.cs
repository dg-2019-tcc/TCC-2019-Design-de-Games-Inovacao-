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

    [HideInInspector]
    public PhotonView photonView;


    public void Start()
    {
        if (GetComponent<PhotonView>().IsMine || PhotonNetwork.InRoom)
        {
            joyStick = FindObjectOfType<Joystick>();
        }


        photonView = gameObject.GetComponent<PhotonView>();

        bola = GameObject.FindWithTag("Volei");
        bolaVolei = bola.GetComponent<BolaVolei>();
        ballrb = bola.GetComponent<Rigidbody2D>();

        player = gameObject.GetComponentInParent<PlayerThings>();
        triggerController = GetComponent<TriggerCollisionsController>();
        controller = GetComponent<Controller2D>();
    }


    public void Update()
    {
        if (photonView.IsMine == true || !PhotonNetwork.InRoom)
        {


            if (joyStick != null)
            {
                if (joyStick.Horizontal > 0)
                {
                    rightDir = true;
                }

                else if (joyStick.Horizontal < 0)
                {
                    rightDir = false;
                }
            }

            if (triggerController.collisions.cortaBola == true || triggerController.collisions.cabecaBola == true || triggerController.collisions.tocouBola == true)
            {
                if (cortou == true)
                {
                    if (controller.collisions.below == false)
                    {
                        gameObject.GetComponent<PhotonView>().RPC("SuperCortaBola", RpcTarget.MasterClient);
                    }

                    else
                    {
                        gameObject.GetComponent<PhotonView>().RPC("CortaBola", RpcTarget.MasterClient);
                    }
                }

                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("BateBola", RpcTarget.MasterClient);
                }
            }
        }
    }

    public void Corte()
    {
        gameObject.GetComponent<PhotonView>().RPC("CortouBall", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void CortouBall()
    {
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
        ballrb.velocity = new Vector2(0, 0);

        ballrb.AddForce(new Vector2(10, 50), ForceMode2D.Impulse);
    }


    [PunRPC]
    public void CortaBola()
    {
        ballrb.velocity = new Vector2(0, 0);

        bolaVolei.corte = true;

        ballrb.AddForce(new Vector2(Random.Range(corteForceX - 10, 10 + corteForceX), corteForceY), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperCortaBola()
    {
        ballrb.velocity = new Vector2(0, 0);

        bolaVolei.superCorte = true;

        ballrb.AddForce(new Vector2(Random.Range(10 -superForceX,10 + superForceX), superForceY), ForceMode2D.Impulse);
    }

}
