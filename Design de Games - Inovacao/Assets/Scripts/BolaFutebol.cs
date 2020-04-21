using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFutebol : MonoBehaviour
{
    private SpriteRenderer bolaSprite;
    private Rigidbody2D rb2d;

    public bool normal;
    public bool kick;
    public bool superKick;

    public float bolaTimer;

    public float maxSpeed = 1f;

    public Transform bolaSpawnPoint;

    // Start is called before the first frame update  
    void Start()
    {
        bolaSprite = GetComponent<SpriteRenderer>();

        rb2d = GetComponent <Rigidbody2D>();
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
            gameObject.GetComponent<PhotonView>().RPC("BolaAzul", RpcTarget.MasterClient);
        }

        else if (kick)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaAmarela", RpcTarget.MasterClient);
        }

        else if (superKick)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaVermelha", RpcTarget.MasterClient);
        }

        else
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaBranca", RpcTarget.MasterClient);
        }

        if (bolaTimer >= 3f)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaBranca", RpcTarget.MasterClient);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plataforma"))
        {
            gameObject.GetComponent<PhotonView>().RPC("SlowBola", RpcTarget.MasterClient);
        }

        if (collision.CompareTag("ResetBall"))
        {
            gameObject.GetComponent<Transform>().position = bolaSpawnPoint.transform.position;
        }
    }

    [PunRPC]
    void SlowBola()
    {
        rb2d.velocity /= 2;
    }

    [PunRPC]
    void BolaBranca()
    {
        normal = false;
        kick = false;
        superKick = false;
        bolaTimer = 0f;
        bolaSprite.color = Color.white;
    }

    [PunRPC]
    void BolaAzul()
    {
        bolaSprite.color = Color.blue;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaAmarela()
    {
        bolaSprite.color = Color.yellow;

        normal = false;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVermelha()
    {
        bolaSprite.color = Color.red;

        normal = false;
        kick = false;

        bolaTimer += Time.deltaTime;
    }
}
