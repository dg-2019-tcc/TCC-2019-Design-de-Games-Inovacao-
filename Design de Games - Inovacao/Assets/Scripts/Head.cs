using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float headForceX;
    public float headForceY;
    public FloatVariable headForce;

    private bool rightDir;

    private Rigidbody2D ballrb;

    private BolaFutebol bola;

    public Joystick joyStick;


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
                gameObject.GetComponent <PhotonView>().RPC("GiraHead", RpcTarget.All, rightDir);
            }

            else if (joyStick.Horizontal < 0)
            {
                rightDir = false;
                gameObject.GetComponent<PhotonView>().RPC("GiraHead", RpcTarget.All, rightDir);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Bola"))
        {
            bola = col.GetComponent<BolaFutebol>();
            ballrb = col.GetComponent<Rigidbody2D>();

            gameObject.GetComponent<PhotonView>().RPC("HeadBall", RpcTarget.MasterClient);

        }
    }

    [PunRPC]
    void HeadBall()
    {
        bola.normal = true;  

        ballrb.AddForce(new Vector2(headForceX, headForceY), ForceMode2D.Impulse);
    }

    [PunRPC]
    void GiraHead(bool dir)
    {
        if (dir)
        {
            //kickSizeX = 0.5f;
            headForceX = headForce.Value;
        }

        else
        {
            //kickSizeX = -0.5f;
            headForceX = headForce.Value * -1;

        }
    }
}

