using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaVolei : MonoBehaviour
{
    private SpriteRenderer bolaSprite;
    private Rigidbody2D rb2d;

    public bool normal;
    public bool corte;
    public bool superCorte;

    public float bolaTimer;

    public float maxSpeed = 12f;

    // Start is called before the first frame update  
    void Start()
    {
        bolaSprite = GetComponent<SpriteRenderer>();

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame  
    void Update()
    {
        Vector2 vel = rb2d.velocity;

        if (vel.magnitude > maxSpeed)
        {
            rb2d.velocity = vel.normalized * maxSpeed;
        }


        if (normal)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaVoleiAzul", RpcTarget.MasterClient);
        }

        else if (corte && superCorte == false)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaVoleiAmarela", RpcTarget.MasterClient);
        }

        else if (superCorte)
        {
            normal = false;
            corte = false;  
            gameObject.GetComponent<PhotonView>().RPC("BolaVoleiVermelha", RpcTarget.MasterClient);
        }

        else
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaVoleiBranca", RpcTarget.MasterClient);
        }

        if (bolaTimer >= 3f)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaVoleiBranca", RpcTarget.MasterClient);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plataforma"))
        {
            gameObject.GetComponent<PhotonView>().RPC("SlowBola", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void SlowBola()
    {
        rb2d.velocity /= 2;
    }

    [PunRPC]
    void BolaVoleiBranca()
    {
        normal = false;
        corte = false;
        superCorte = false;
        bolaTimer = 0f;
        bolaSprite.color = Color.white;
    }

    [PunRPC]
    void BolaVoleiAzul()
    {
        bolaSprite.color = Color.blue;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVoleiAmarela()
    {
        bolaSprite.color = Color.yellow;

        normal = false;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVoleiVermelha()
    {
        bolaSprite.color = Color.red;

        normal = false;
        corte = false;

        bolaTimer += Time.deltaTime;
    }
}
