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

    private BolaVolei bola;

    public Joystick joyStick;

    public GameObject hand;

    public GameObject normalHand;

    public GameObject superHand;


    public void Start()
    {
        if (GetComponent<PhotonView>().IsMine || PhotonNetwork.InRoom)
        {
            joyStick = FindObjectOfType<Joystick>();
        }

        player = gameObject.GetComponentInParent<PlayerMovement>();
    }


    public void Update()
    {

        if (joyStick != null)
        {
            if (joyStick.Horizontal > 0)
            {
                rightDir = true;
                gameObject.GetComponent<PhotonView>().RPC("GiraHand", RpcTarget.All, rightDir);
            }

            else if (joyStick.Horizontal < 0)
            {
                rightDir = false;
                gameObject.GetComponent<PhotonView>().RPC("GiraHand", RpcTarget.All, rightDir);
            }
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola") && cortou == true && player.grounded == false)
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            bola = col.GetComponent<BolaVolei>();
            gameObject.GetComponent<PhotonView>().RPC("SuperCortaBola", RpcTarget.MasterClient);
        }

        if (col.CompareTag("Bola"))
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            bola = col.GetComponent<BolaVolei>();
            gameObject.GetComponent<PhotonView>().RPC("CortaBola", RpcTarget.MasterClient);
        }
    }



    [PunRPC]
    public void CortaBola()
    {
        Debug.Log("CortaBola");
        bola.corte = true;
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
        bola.superCorte = true;
        ballrb.AddForce(new Vector2(superForceX, forceVertical), ForceMode2D.Impulse);
    }

    [PunRPC]
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
    }
}
