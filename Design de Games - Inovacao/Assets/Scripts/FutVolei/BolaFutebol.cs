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

    public float maxSpeed = 20f;

    public Transform bolaSpawnPoint;

	private PhotonView PV;

    public GameObject goool;

    // Start is called before the first frame update  
    void Start()
    {
        bolaSprite = GetComponent<SpriteRenderer>();

        rb2d = GetComponent <Rigidbody2D>();

		PV = GetComponent<PhotonView>();
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
            PV.RPC("BolaAzul", RpcTarget.All);
        }

        else if (kick)
        {
			PV.RPC("BolaAmarela", RpcTarget.All);
        }

        else if (superKick)
        {
			PV.RPC("BolaVermelha", RpcTarget.All);
        }

        else
        {
			PV.RPC("BolaBranca", RpcTarget.All);
        }

        if (bolaTimer >= 3f)
        {
			PV.RPC("BolaBranca", RpcTarget.All);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plataforma"))
        {
            PV.RPC("SlowBola", RpcTarget.All);
        }

        if (collision.CompareTag("ResetBall"))
        {
            transform.position = bolaSpawnPoint.transform.position;
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
