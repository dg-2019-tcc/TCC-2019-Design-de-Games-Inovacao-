using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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



    public void InitializeBullet(Photon.Realtime.Player owner)
    {
        Owner = owner;

        shootDirection = ThrowObject.direction;

        rb.velocity = shootDirection;
        rb.position += rb.velocity;

		Owner.CustomProperties["atirou"] = true;
	}

    private void Update()
    {
        rb.velocity = shootDirection;
        rb.position += rb.velocity;

        timeDestroy += Time.deltaTime;
        if (timeDestroy >= 5f)
        {
			
            Owner.CustomProperties["atirou"] =  false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pipa"))
        {
			Owner.CustomProperties["dogPipa"] = true;
            //tokenSom.Play();
            totemTarget = collision.transform;
			Owner.CustomProperties["acertouTotem"] = true;
            timeDestroy = 4.9f;
        }

        if (collision.CompareTag("Carrinho"))
        {
			Owner.CustomProperties["dogCarro"] = true;
            //tokenSom.Play();
            totemTarget = collision.transform;
			Owner.CustomProperties["acertouTotem"] = true;
            timeDestroy = 4.9f;

        }

        if (collision.CompareTag("Player"))
        {
            PlayerMovement jogador = collision.GetComponent<PlayerMovement>();
            jogador.levouDogada = true;
            timeDestroy = 4.9f;
        }
    }
}
