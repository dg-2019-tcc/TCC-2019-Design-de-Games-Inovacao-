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

    private float normal;
    private float normalY = 50;
    private float corteForce;
    private float superCorteForce;


    private float forceVertical;

    public bool cortou;

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

            if (PlayerThings.rightDir)
            {
                normal = 10;
                corteForce = corteForceX;
                superCorteForce = superForceX;

                if (cortou)
                {
                    triggerController.rightRay = true;
                    triggerController.leftRay = false;
                }

                else
                {
                    triggerController.rightRay = false;
                    triggerController.leftRay = false;
                }
            }

            if (PlayerThings.leftDir)
            {
                normal = -10;
                corteForce = corteForceX  * -1;
                superCorteForce = superForceX * -1;

                if (cortou)
                {
                    triggerController.rightRay = true;
                    triggerController.leftRay = false;
                }

                else
                {
                    triggerController.rightRay = false;
                    triggerController.leftRay = false;
                }
            }

            if (triggerController.collisions.cortaBola == true || triggerController.collisions.cabecaBola == true || triggerController.collisions.tocouBola == true)
            {
                if (cortou == true)
                {
                    if (controller.collisions.below == false)
                    {
                        gameObject.GetComponent<PhotonView>().RPC("SuperCortaBola", RpcTarget.All, superCorteForce, superForceY);
                    }

                    else
                    {
                        gameObject.GetComponent<PhotonView>().RPC("CortaBola", RpcTarget.All, corteForce, corteForceY);
                    }
                }

                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("BateBola", RpcTarget.All, normal, normalY);
                }
            }
        
    }

    public void Corte()
    {
        gameObject.GetComponent<PhotonView>().RPC("CortouBall", RpcTarget.All);
    }

    [PunRPC]
    public void CortouBall()
    {
        if (photonView.IsMine == true || !PhotonNetwork.InRoom)
        {

            if (cortou == false)
            {
                StartCoroutine("CoolHand");
            }
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
    public void BateBola(float forceX, float forceY)
    {
        ballrb.velocity = new Vector2(0, 0);

        ballrb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }


    [PunRPC]
    public void CortaBola(float forceX, float forceY)
    {
        ballrb.velocity = new Vector2(0, 0);

        bolaVolei.corte = true;

        ballrb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperCortaBola(float forceX, float forceY)
    {
        ballrb.velocity = new Vector2(0, 0);

        bolaVolei.superCorte = true;

        ballrb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }

}
