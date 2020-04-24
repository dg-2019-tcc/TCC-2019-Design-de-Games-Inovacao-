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

    private float forceVertical;

    private bool kicked;


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

        if (kicked == true && triggerController.collisions.chutouBola == true && controller.collisions.below == false)
        {
            Debug.Log("Super");
            gameObject.GetComponent<PhotonView>().RPC("SuperKickBola", RpcTarget.MasterClient);
            triggerController.collisions.chutouBola = false;
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
    public void KickBola()
    {
        bolaFutebol.kick = true;
        bolaFutebol.normal = false;
        bolaFutebol.superKick = false;
        bolaFutebol.velocity = new Vector2(kickForceX, forceVertical);
        Debug.Log("KickBola");
        //ballrb.AddForce(new Vector2(kickForceX, forceVertical *2), ForceMode2D.Impulse);
    }

    [PunRPC]
    public void SuperKickBola()
    {
        bolaFutebol.superKick = true;
        bolaFutebol.kick = false;
        bolaFutebol.normal = false;
        bolaFutebol.velocity = new Vector2(superKickForceX, forceVertical);
        Debug.Log("SuperKickBola");
        //ballrb.AddForce(new Vector2(superKickForceX, forceVertical *2), ForceMode2D.Impulse);
    }
}
