using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVolei : MonoBehaviour
{

    public float cooldownKick;

    public float kickSizeX;

    public float kickSizeY;

    public float kickForceX;

    public float kickForceY;

    private float forceVertical;

    private bool kicked;

    private bool rightDir;

    private Rigidbody2D ballrb;

    public Joystick joyStick;

    public GameObject hand;


    public void Start()
    {
        if (GetComponent<PhotonView>().IsMine || PhotonNetwork.InRoom)
        {
            joyStick = FindObjectOfType<Joystick>();
        }
    }


    public void Update()
    {

        if (joyStick != null)
        {
            if (joyStick.Horizontal > 0)
            {
                rightDir = true;
                gameObject.GetComponent<PhotonView>().RPC("GiraFoot", RpcTarget.All, rightDir);
            }

            else if (joyStick.Horizontal < 0)
            {
                rightDir = false;
                gameObject.GetComponent<PhotonView>().RPC("GiraFoot", RpcTarget.All, rightDir);
            }
        }
    }

    public void Corte()
    {
        if (joyStick.Vertical != 0)
        {
            forceVertical = kickForceY * joyStick.Vertical;
        }

        else
        {
            forceVertical = 5f;
        }
        //Debug.Log(forceVertical);
        gameObject.GetComponent<PhotonView>().RPC("KickedBall", RpcTarget.MasterClient, forceVertical);
    }

    [PunRPC]
    public void CortouBall(float force)
    {
        forceVertical = force;
        if (kicked == false)
        {
            StartCoroutine("CoolKick");
        }
    }

    IEnumerator CoolHand()
    {

        //hand.transform.position = handKickPos.transform.position;
        hand.SetActive(true);
        kicked = true;

        yield return new WaitForSeconds(cooldownKick);

        //hand.transform.position = handIncialPos.transform.position;
        hand.SetActive(false);
        kicked = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola") && kicked == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
        }
    }

    /*private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Bola") && kicked == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
        }
    }*/

    [PunRPC]
    public void CortaBola()
    {
        Debug.Log("KickBola");
        ballrb.AddForce(new Vector2(kickForceX, forceVertical), ForceMode2D.Impulse);
    }

    [PunRPC]
    void GiraFoot(bool dir)
    {
        if (dir)
        {
            kickSizeX = 0.5f;
            kickForceX = 5f;
        }

        else
        {
            kickSizeX = -0.5f;
            kickForceX = -5f;
        }
    }
}
