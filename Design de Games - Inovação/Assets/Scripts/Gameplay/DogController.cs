﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject player;
    private Transform target;
    public float speedToTotem = 10f;

    private bool dogTransformed;

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
    private PhotonView PV;

    [Header("Skills")]
    public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;
    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;
    //public BoolVariable dog;




    void Start()
    {
        PV = GetComponent<PhotonView>();
        efeitoCarro.ativa.Value = false;
        efeitoPipa.ativa.Value = false;
        PV.Controller.CustomProperties["dogValue"] = true;
    }


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

        if ((bool)PV.Controller.CustomProperties["dogValue"] == false)
        {
            gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
        }

        else
        {
            gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true);
        }



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipa"))
        {
            if (efeitoCarro.ativa.Value == false && efeitoPipa.ativa.Value == false)
            {
                gameObject.GetComponent<PhotonView>().RPC("Pipa", RpcTarget.All);
            }
        }

        if (collision.CompareTag("Carrinho"))
        {
            if (efeitoCarro.ativa.Value == false && efeitoPipa.ativa.Value == false)
            {
                gameObject.GetComponent<PhotonView>().RPC("Carro", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void Carro()
    {
        StartCoroutine(efeitoCarro.Enumerator(this));
        carrinhoObj.SetActive(true);
        tokenAudioEvent.Play(tokenSom);
        hitTotemCarro.Value = false;
    }



    [PunRPC]
    public void Pipa()
    {
        StartCoroutine(efeitoPipa.Enumerator(this));
        pipaObj.SetActive(true);
        tokenAudioEvent.Play(tokenSom);
        hitTotemPipa.Value = false;
    }



    [PunRPC]
    private void TransformaPet(bool isDog)
    {
        if (efeitoPipa.ativa.Value == false)
        {
            pipaObj.SetActive(false);
        }
        if(efeitoCarro.ativa.Value == false)
        {
            carrinhoObj.SetActive(false);
        }
        Pet.SetActive(isDog);
        if (!isDog)
        {
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
        }
    }
}
