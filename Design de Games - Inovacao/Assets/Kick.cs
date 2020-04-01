﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
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

    public GameObject foot;

    public void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
    }


    public void Update()
    {

        if (joyStick != null)
        {
            if (joyStick.Horizontal > 0)
            {
                rightDir = true;
                gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
            }

            else if (joyStick.Horizontal < 0)
            {
                rightDir = false;
                gameObject.GetComponent<PhotonView>().RPC("GiraPlayer", RpcTarget.All, rightDir);
            }
        }
    }

    public void Chute()
    {
        gameObject.GetComponent<PhotonView>().RPC("KickedBall", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void KickedBall()
    {
        if (kicked == false)
        {
            StartCoroutine("CoolKick");
        }
    }

    IEnumerator CoolKick()
    {

        foot.transform.position = foot.transform.position + new Vector3(kickSizeX, kickSizeY, 0);
        kicked = true;

        yield return new WaitForSeconds(cooldownKick);

        foot.transform.position = foot.transform.position + new Vector3(-kickSizeX, -kickSizeY, 0);
        kicked = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bola") && kicked == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<PhotonView>().RPC("KickBola", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    public void KickBola()
    {

        if (joyStick.Vertical != 0)
        {
            forceVertical = kickForceY * joyStick.Vertical;
        }

        else
        {
            forceVertical = 5f;
        }
        Debug.Log(forceVertical);

        ballrb.AddForce(new Vector2(kickForceX, forceVertical), ForceMode2D.Impulse);
    }

    [PunRPC]
    void GiraPlayer(bool dir)
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
