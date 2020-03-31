using Photon.Pun;
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

    private Rigidbody2D ballrb;

    public Joystick joyStick;

    public GameObject foot;

    public void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
    }


    public void Update()
    {
        if(PlayerMovement.leftDir == true)
        {
            kickSizeX = -0.5f;
            kickForceX = -5f;
        }

        else
        {
            kickSizeX = 0.5f;
            kickForceX = 5f;
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

        foot.transform.position = transform.position + new Vector3(kickSizeX, kickSizeY, 0);
        kicked = true;

        yield return new WaitForSeconds(cooldownKick);

        foot.transform.position = transform.position + new Vector3(-kickSizeX, -kickSizeY, 0);
        kicked = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bola") && kicked == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            KickBola();
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
}
