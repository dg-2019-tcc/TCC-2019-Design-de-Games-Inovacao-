﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject player;
    public GameObject playerModel;
    private Transform target;
    public float speedToTotem = 10f;

    private bool dogTransformed;

    [Header("Pet")]

    public GameObject Pet;
    public GameObject dogSpawn;
    public static bool poderEstaAtivo;
    public static bool desativaPoder;

    [Header("Pet pipa")]

    public GameObject pipaObj;

    [Header("Pet carrinho")]

    public GameObject carrinhoObj;


    [Header("Som")]

    public AudioSource tokenSom;
    public AudioSource carroSom;
    public AudioSource pipaSom;
    public SimpleAudioEvent tokenAudioEvent;


    [HideInInspector]
    private PhotonView PV;

    [Header("Skills")]
    public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;
    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;
    public BoolVariable carroActive;
    public BoolVariable pipaActive;
    //public BoolVariable dog;

    public Animator playerAC;




    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        efeitoCarro.ativa.Value = false;
        efeitoPipa.ativa.Value = false;
        PV.Controller.CustomProperties["dogValue"] = true;
    }


    void Update()
    {
        if (PV != null && !PV.IsMine) return;

        if(PlayerMovement.acabou == true)
        {
            carrinhoObj.SetActive(false);
            pipaObj.SetActive(false);
            playerModel.SetActive(true);
        }


        if (poderEstaAtivo == false)//efeitoCarro.ativa.Value == false || efeitoPipa.ativa.Value == false)
        {

            pipaSom.Stop();
            carroSom.Stop();
            gameObject.GetComponent<PhotonView>().RPC("DesativaPowerUps", RpcTarget.All);
        }



        /*
        if (efeitoCarro.ativa.Value == false)
        {
            carrinhoObj.SetActive(false);
            Pet.SetActive(true);
        }

        if (efeitoPipa.ativa.Value == false)
        {
            pipaObj.SetActive(false);
            Pet.SetActive(true);
        }*/


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
        if (!PV.IsMine && PV != null) return;
		if (!(bool)PV.Controller.CustomProperties["dogValue"]) return;
        if (collision.CompareTag("Pipa"))
        {
            if (efeitoCarro.ativa.Value == false && efeitoPipa.ativa.Value == false)
            {
                StartCoroutine(efeitoPipa.Enumerator(this));
                gameObject.GetComponent<PhotonView>().RPC("Pipa", RpcTarget.All);
                hitTotemPipa.Value = false;
            }
        }

        if (collision.CompareTag("Carrinho"))
        {
            if (efeitoCarro.ativa.Value == false && efeitoPipa.ativa.Value == false)
            {
                StartCoroutine(efeitoCarro.Enumerator(this));
                gameObject.GetComponent<PhotonView>().RPC("Carro", RpcTarget.All);
                hitTotemCarro.Value = false;
            }
        }
    }

    [PunRPC]
    public void Carro()
    {
        carroActive.Value = true;
        carroSom.Play();
        PV.Controller.CustomProperties["dogValue"] = false;
        carrinhoObj.SetActive(true);
        playerModel.SetActive(false);
        poderEstaAtivo = true;
        tokenAudioEvent.Play(tokenSom);
        StartCoroutine(TempoParaDesativar(6f));
    }



    [PunRPC]
    public void Pipa()
    {
        pipaActive.Value = true;
        pipaSom.Play();
        PV.Controller.CustomProperties["dogValue"] = false;
        pipaObj.SetActive(true);
        playerModel.SetActive(false);
        poderEstaAtivo = true;
        tokenAudioEvent.Play(tokenSom);
        StartCoroutine(TempoParaDesativar(6f));
    }

    [PunRPC]
    public void DesativaPowerUps()
    {


        if (efeitoPipa.ativa.Value == false || efeitoPipa.ativa.Value == true)
        {
            pipaActive.Value = false;
            pipaObj.SetActive(false);
            playerModel.SetActive(true);
        }
        if (efeitoCarro.ativa.Value == false || efeitoCarro.ativa.Value == true)
        {
            carroActive.Value = false;
            carrinhoObj.SetActive(false);
            playerModel.SetActive(true);
        }
    }

    [PunRPC]
    private void TransformaPet(bool isDog)
    {
        Pet.SetActive(isDog);
        if (!isDog)
        {
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
        }
    }

    private IEnumerator TempoParaDesativar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        poderEstaAtivo = false;
        PV.Controller.CustomProperties["dogValue"] = true;
    }

	
}