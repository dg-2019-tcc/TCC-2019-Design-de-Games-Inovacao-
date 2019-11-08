using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject player;
    private Transform target;
    public float speedToTotem = 10f;

    [Header("Pet")]

    public GameObject Pet;
    public GameObject dogSpawn;

    [Header("Pet pipa")]

    public GameObject pipaObj;

    [Header("Pet carrinho")]

    public GameObject carrinhoObj;


    [Header("Som")]

    public AudioSource tokenSom;
    public SimpleAudioEvent tokenAudioEvent;


    [HideInInspector]
    public PhotonView PV;

    [Header("Skills")]
    public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;
    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;
    public BoolVariable dog;



    // Start is called before the first frame update
    void Start()
    {
        efeitoCarro.ativa.Value = false;
        efeitoPipa.ativa.Value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV != null && !PV.IsMine) return;


        if (efeitoCarro.ativa.Value == false)
        {
            carrinhoObj.SetActive(false);
            Pet.SetActive(true);
        }

        if (efeitoPipa.ativa.Value == false)
        {
            pipaObj.SetActive(false);
            Pet.SetActive(true);
        }


        if (hitTotemCarro.Value == true || hitTotemPipa.Value == true)
        {
            target = ItemThrow.totemTarget;
            float step = speedToTotem * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        }

        if(dog.Value == false)
        {
            Pet.SetActive(false);
        }

        else
        {
            Pet.SetActive(true);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipa"))
        {
            if (efeitoCarro.ativa.Value == false)
            { 
            Pipa();
            }
        }

        if (collision.CompareTag("Carrinho"))
        {
            if (efeitoPipa.ativa.Value == false)
            {
                Carro();
            }
        }
    }

    public void Carro()
    {
        StartCoroutine(efeitoCarro.Enumerator(this));
        carrinhoObj.SetActive(true);
        tokenAudioEvent.Play(tokenSom);
        hitTotemCarro.Value = false;
    }

    public void Pipa()
    {
        StartCoroutine(efeitoPipa.Enumerator(this));
        pipaObj.SetActive(true);
        tokenAudioEvent.Play(tokenSom);
        hitTotemPipa.Value = false;
    }
    /*[PunRPC]
    private void TransformaPet(bool isDog, string transformation)
    {
        Pet.SetActive(isDog);
    }*/

}
