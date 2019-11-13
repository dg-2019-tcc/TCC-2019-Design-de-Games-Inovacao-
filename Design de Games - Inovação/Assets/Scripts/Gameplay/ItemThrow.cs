using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ItemThrow : MonoBehaviour
{

    public Photon.Realtime.Player Owner { get; private set; }

    public float speed = 20f;
    public Rigidbody2D rb;

    private Vector2 shootDirection;

    public float timeDestroy;

    public float speedPlayer = 1.0f;

    public static Transform totemTarget;

    public bool hit;

    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;
    public BoolVariable dog;


    public void InitializeBullet(Photon.Realtime.Player owner)
    {
        Owner = owner;

        shootDirection = ThrowObject.direction;

        rb.velocity = shootDirection;
        rb.position += rb.velocity;

		Owner.CustomProperties["atirou"] = true;
		Owner.CustomProperties["dogValue"] = false;
	}

    private void Update()
    {
        rb.velocity = shootDirection;
        rb.position += rb.velocity;

        timeDestroy += Time.deltaTime;
        if (timeDestroy >= 5f)
        {
			Owner.CustomProperties["dogValue"] = true;
            Owner.CustomProperties["atirou"] =  false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pipa"))
        {
            totemTarget = collision.transform;
            hitTotemPipa.Value = true;
			Owner.CustomProperties["dogPipa"] = true;
            //tokenSom.Play();
            
			Owner.CustomProperties["acertouTotem"] = true;
            timeDestroy = 4.9f;
        }

        if (collision.CompareTag("Carrinho"))
        {
            totemTarget = collision.transform;
            hitTotemCarro.Value = true;
            Owner.CustomProperties["dogCarro"] = true;
            //tokenSom.Play();
            
			Owner.CustomProperties["acertouTotem"] = true;
            timeDestroy = 4.9f;

        }

        if (collision.CompareTag("Player"))
        {
            PlayerMovement jogador = collision.GetComponent<PlayerMovement>();
            jogador.levouDogada = true;
            timeDestroy = 4.9f;
        }

		if (collision.CompareTag("Coletavel"))
		{
			collision.SendMessage("Coleta");
			Owner.AddScore(1);
		}
    }
}
