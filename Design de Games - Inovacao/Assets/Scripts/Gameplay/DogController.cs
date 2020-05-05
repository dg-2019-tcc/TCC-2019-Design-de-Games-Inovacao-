using Photon.Pun;
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
    public BoolVariable dogAtivo;
    public GameObject dogSpawn;


    [Header("Pet pipa")]

    public GameObject pipaObj;

    [Header("Pet carrinho")]

    public GameObject carrinhoObj;


    [HideInInspector]
    private PhotonView PV;

    [Header("Skills")]

    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;

    public BoolVariable carroActive;
    public BoolVariable pipaActive;

    public BoolVariable dogBotao;
    public BoolVariable desativaPower;



    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        pipaActive.Value = false;
        carroActive.Value = false;
        PV.Controller.CustomProperties["dogValue"] = true;
        dogAtivo.Value = true;
    }


    void Update()
    {
        if (PV == null || PV.IsMine || !PhotonNetwork.InRoom)
        {
            if (desativaPower.Value == true)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Debug.Log("Desativa");
                    DesativaPowerUps();
                }
                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("DesativaPowerUps", RpcTarget.All);
                }
            }

            if (hitTotemCarro.Value == true || hitTotemPipa.Value == true)
            {
                target = ItemThrow.totemTarget;
                float step = speedToTotem * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }

            PV.Controller.CustomProperties["dogValue"] = dogAtivo.Value;

            if (!dogAtivo.Value)
            {
                if (!PhotonNetwork.InRoom)
                {
                    TransformaPet(false);
                }
                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
                }
            }

            else
            {
                if (!PhotonNetwork.InRoom)
                {
                    TransformaPet(true);
                }
                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true);
                }
            }

        }



    }


    [PunRPC]
    public void Carro()
    {
        if (carroActive.Value == false && pipaActive.Value == false)
        {

            hitTotemCarro.Value = false;
            carroActive.Value = true;

            dogAtivo.Value = false;
            carrinhoObj.SetActive(true);
        }

    }



    [PunRPC]
    public void Pipa()
    {
        if (carroActive.Value == false && pipaActive.Value == false)
        {
            hitTotemPipa.Value = false;
            pipaActive.Value = true;

            dogAtivo.Value = false;
            pipaObj.SetActive(true);
        }
    }

    [PunRPC]
    public void DesativaPowerUps()
    {
        dogAtivo.Value = true;

        pipaActive.Value = false;
        pipaObj.SetActive(false);


        carroActive.Value = false;
        carrinhoObj.SetActive(false);
        desativaPower.Value = false;

    }

    [PunRPC]
    private void TransformaPet(bool isDog)
    {
        Pet.SetActive(isDog);
        if (!isDog)
        {
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
            //buttonPressed.Value = false;

        }
    }



}
