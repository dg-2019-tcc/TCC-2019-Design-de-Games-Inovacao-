using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemThrow : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    private Vector2 shootDirection;

    public float timeDestroy;

    public PhotonView PV;


    private void Awake()
    {
        timeDestroy += Time.deltaTime;
        shootDirection = ThrowObject.direction;

        rb.velocity = shootDirection * speed;

        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (timeDestroy >= 3f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            if (PV.IsMine == true)
            {
                PlayerMovement.coletavel++;
                //coleta.Play();
            }
        }

        if (collision.CompareTag("Pipa"))
        {
            PlayerMovement.dogPipa = true;
            //tokenSom.Play();
        }

        if (collision.CompareTag("Carrinho"))
        {
            PlayerMovement.dogCarro = true;
            //tokenSom.Play();
        }
    }
}
