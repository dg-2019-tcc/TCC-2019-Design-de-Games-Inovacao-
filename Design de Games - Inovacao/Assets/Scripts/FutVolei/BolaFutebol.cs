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
    public float resetSpeed = 0;

    public Vector3 bolaSpawnPoint;

	private PhotonView PV;

    public GameObject goool;

    Vector3 white;
    Vector3 red;
    Vector3 blue;
    Vector3 yellow;

    // Start is called before the first frame update  
    void Start()
    {
        white = new Vector3(1,1,1);
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
        Vector2 vel = rb2d.velocity;

        if (vel.magnitude > maxSpeed)
        {
            rb2d.velocity = vel.normalized * maxSpeed;
        }


        if (normal)
        {
            PV.RPC("BolaAzul", RpcTarget.All, blue);
        }

        else if (kick)
        {
			PV.RPC("BolaAmarela", RpcTarget.All, yellow);
        }

        else if (superKick)
        {
			PV.RPC("BolaVermelha", RpcTarget.All, red);
        }

        else
        {
			PV.RPC("BolaBranca", RpcTarget.All,white);
        }

        if (bolaTimer >= 3f)
        {
			PV.RPC("BolaBranca", RpcTarget.All, white);
        }
    }

    public void FoiGol()
    {
        StartCoroutine("BolaPos");
    }

    [PunRPC]
    public void ResetaBola(bool ativaGoool,bool isKin, Vector3 bolaSpawn, float speed)
    {
        goool.SetActive(ativaGoool);
        //this.gameObject.SetActive(ativaBola);

        //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;
        rb2d.velocity *=0;
        rb2d.isKinematic = isKin;

        this.gameObject.transform.position = bolaSpawn;
    }

    IEnumerator BolaPos()
    {
        Debug.Log("Reseta");
        PV.RPC("ResetaBola", RpcTarget.All, true,true, bolaSpawnPoint, resetSpeed);
        yield return new WaitForSeconds(0.8f);
        PV.RPC("ResetaBola", RpcTarget.All, false,false, bolaSpawnPoint, resetSpeed);
        Debug.Log("Stop");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plataforma"))
        {
            PV.RPC("SlowBola", RpcTarget.All);
        }

        if (collision.CompareTag("ResetBall"))
        {
            transform.position = bolaSpawnPoint;
        }
    }


    [PunRPC]
    void SlowBola()
    {
        rb2d.velocity /= 2;
    }

    [PunRPC]
    void BolaBranca(Vector3 cor)
    {
        normal = false;
        kick = false;
        superKick = false;
        bolaTimer = 0f;
        bolaSprite.color = new Color(cor.x,cor.y,cor.z);
    }

    [PunRPC]
    void BolaAzul(Vector3 cor)
    {
        bolaSprite.color = new Color(cor.x, cor.y, cor.z); ;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaAmarela(Vector3 cor)
    {
        bolaSprite.color = new Color(cor.x, cor.y, cor.z); 

        normal = false;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVermelha(Vector3 cor)
    {
        bolaSprite.color = new Color(cor.x, cor.y, cor.z); 

        normal = false;
        kick = false;

        bolaTimer += Time.deltaTime;
    }
}
