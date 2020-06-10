using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaVolei : MonoBehaviour
{
    private PhotonView PV;

    private SpriteRenderer bolaSprite;
    private Rigidbody2D rb2d;

    public bool normal;
    public bool corte;
    public bool superCorte;

    public float bolaTimer;

    public Vector3 bolaSpawnPoint;
    private float resetSpeed = 0;

    public float maxSpeed = 12f;
    Vector2 vel;

    Vector3 white;
    Vector3 red;
    Vector3 blue;
    Vector3 yellow;

    // Start is called before the first frame update  
    void Start()
    {
        white = new Vector3(1, 1, 1);
        red = new Vector3(1, 0, 0);
        blue = new Vector3(0, 0, 1);
        yellow = new Vector3(1, 0.92f, 0.016f);

        bolaSprite = GetComponent<SpriteRenderer>();

        rb2d = GetComponent<Rigidbody2D>();

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame  
    void Update()
    {
        vel = rb2d.velocity;

        if (vel.magnitude > maxSpeed)
        {
            PV.RPC("VelBola", RpcTarget.MasterClient);
        }


        if (normal)
        {
            PV.RPC("BolaVoleiAzul", RpcTarget.MasterClient, blue);
        }

        else if (corte && superCorte == false)
        {
            PV.RPC("BolaVoleiAmarela", RpcTarget.MasterClient, yellow);
        }

        else if (superCorte)
        {
            PV.RPC("BolaVoleiVermelha", RpcTarget.MasterClient, red);
        }

        else
        {
            PV.RPC("BolaVoleiBranca", RpcTarget.MasterClient, white);
        }

        if (bolaTimer >= 3f)
        {
            PV.RPC("BolaVoleiBranca", RpcTarget.MasterClient, white);
        }
    }

    [PunRPC]
    void ResetaBolaVolei(Vector3 bolaSpawn, float speed, bool isKin)
    {
        rb2d.velocity *= 0;
        rb2d.isKinematic = isKin;
        this.gameObject.transform.position = bolaSpawn;

    }

    public void FoiPonto(Vector3 bolaSpawn)
    {
        bolaSpawnPoint = bolaSpawn;
        StartCoroutine("BolaPos");
    }

    IEnumerator BolaPos()
    {
        Debug.Log("Reseta");
        PV.RPC("ResetaBolaVolei", RpcTarget.All,bolaSpawnPoint, resetSpeed, true);
        yield return new WaitForSeconds(0.8f);
        PV.RPC("ResetaBolaVolei", RpcTarget.All, this.gameObject.transform.position, resetSpeed, false);
        //PV.RPC("BolaVoleiBranca", RpcTarget.MasterClient, white);

        Debug.Log("Stop");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plataforma"))
        {
            gameObject.GetComponent<PhotonView>().RPC("SlowBola", RpcTarget.MasterClient);
        }
    }
    [PunRPC]
    void VelBola()
    {
        rb2d.velocity = vel.normalized * maxSpeed;
    }

    [PunRPC]
    void SlowBola()
    {
        rb2d.velocity /= 2;
    }

    [PunRPC]
    public void BolaVoleiBranca(Vector3 cor)
    {
        rb2d.gravityScale = 1;
        normal = false;
        corte = false;
        superCorte = false;
        bolaTimer = 0f;
        bolaSprite.color = new Color(cor.x, cor.y, cor.z);
    }

    [PunRPC]
    void BolaVoleiAzul(Vector3 cor)
    {
        bolaSprite.color = new Color(cor.x, cor.y, cor.z);

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVoleiAmarela(Vector3 cor)
    {
        rb2d.gravityScale = 1.5f;
        bolaSprite.color = new Color(cor.x, cor.y, cor.z);

        normal = false;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVoleiVermelha(Vector3 cor)
    {
        rb2d.gravityScale = 2;
        bolaSprite.color = new Color(cor.x, cor.y, cor.z);

        normal = false;
        corte = false;


        bolaTimer += Time.deltaTime;
    }
}
