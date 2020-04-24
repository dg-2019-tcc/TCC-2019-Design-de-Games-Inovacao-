using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutebolPlayer : MonoBehaviour
{
    public Controller2D controller;
    public TriggerCollisionsController triggerController;

    private GameObject bola;
    private Rigidbody2D ballrb;
    private BolaFutebol bolaFutebol;

    public float cooldownKick;

    public FloatVariable kickForce;
    public float kickForceX;
    public float kickForceY;

    public FloatVariable superKickForce;
    public float superKickForceX;
    public float superKickForceY;

    private float forceVertical;

    public static bool kicked;

    public GameObject foot;


    void Awake()
    {
        controller = GetComponent<Controller2D>();
        triggerController = GetComponent<TriggerCollisionsController>();

        bola = GameObject.FindWithTag("Futebol");
        ballrb = bola.GetComponent<Rigidbody2D>();
        bolaFutebol = bola.GetComponent<BolaFutebol>();
    }


    void Update()
    {
        if (kicked)
        {
            foot.SetActive(true);
        }
        else
        {
            foot.SetActive(false);
        }

        if (PlayerThings.rightDir)
        {
            kickForceX = kickForce.Value;
            superKickForceX = superKickForce.Value;
        }

        if (PlayerThings.leftDir)
        {
            kickForceX = kickForce.Value * -1;
            superKickForceX = superKickForce.Value * -1;
        }

        if (kicked == true && triggerController.collisions.chutouBola == true && controller.collisions.below == true)
        {
            Debug.Log("Normal");
            gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
            triggerController.collisions.chutouBola = false;
        }


        if (kicked == false && triggerController.collisions.chutouBola == true)
        {
            gameObject.GetComponent<PhotonView>().RPC("TocouBola", RpcTarget.MasterClient);
            triggerController.collisions.chutouBola = false;
        }

        if (kicked == true && triggerController.collisions.chutouBola == true && controller.collisions.below == false)
        {
            Debug.Log("Super");
            gameObject.GetComponent<PhotonView>().RPC("SuperKickBola", RpcTarget.MasterClient);
            triggerController.collisions.chutouBola = false;
        }

        if(triggerController.collisions.cabecaBola == true)
        {
            Debug.Log("Tocou");
            gameObject.GetComponent<PhotonView>().RPC("TocouBola", RpcTarget.MasterClient);
        }
    }

    public void Chute()
    {
        //Debug.Log(forceVertical);
        gameObject.GetComponent<PhotonView>().RPC("KickedBall", RpcTarget.MasterClient, forceVertical);
    }

    [PunRPC]
    public void KickedBall(float force)
    {
        forceVertical = force;
        if (kicked == false)
        {
            StartCoroutine("CoolKick");
        }
    }

    IEnumerator CoolKick()
    {

        //foot.transform.position = footKickPos.transform.position;
        kicked = true;

        yield return new WaitForSeconds(cooldownKick);

        //foot.transform.position = footIncialPos.transform.position;
        kicked = false;
    }
    [PunRPC]
    public void TocouBola()
    {
        bolaFutebol.normal = true;
        ballrb.AddForce(new Vector2(kickForceX/10, 20), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void KickBola()
    {
        bolaFutebol.kick = true;
        bolaFutebol.normal = false;
        bolaFutebol.superKick = false;
        Debug.Log("KickBola");
        ballrb.AddForce(new Vector2(kickForceX, kickForceY), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperKickBola()
    {
        bolaFutebol.superKick = true;
        bolaFutebol.kick = false;
        bolaFutebol.normal = false;
        Debug.Log("SuperKickBola");
        ballrb.AddForce(new Vector2(superKickForceX, kickForceY), ForceMode2D.Impulse);
    }
}
