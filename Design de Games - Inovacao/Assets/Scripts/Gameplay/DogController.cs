using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{

    TriggerCollisionsController triggerCollisionsScript;
    public DogAnim dogAnim;

    public GameObject player;
    public GameObject playerModel;
    private Transform target;
    public float speedToTotem = 10f;

    private bool dogTransformed;
    private bool ativouDog;
    private bool desativouDog;

    [Header("Pet")]

    public GameObject Pet;
    public BoolVariable dogAtivo;
    public GameObject dogSpawn;


    [HideInInspector]
    private PhotonView PV;

    [Header("Skills")]

    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;

    public BoolVariable carroActive;
    public BoolVariable pipaActive;

    public BoolVariable dogBotao;
    public BoolVariable desativaPower;

    public float timePet;
    public bool dogState;


    public BoolVariableArray acabou01;
    public BoolVariableArray aiGanhou;

    bool sequestrado;


    void Start()
    {

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        }

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }

        if (acabou01.Value[4] == true && aiGanhou.Value[4] == false)
        {
            sequestrado = true;
            TransformaPet(false);
        }
        if((acabou01.Value[6] == true && aiGanhou.Value[7] == false))
        {
            sequestrado = false;
        }

        PV = gameObject.GetComponent<PhotonView>();
        pipaActive.Value = false;
        carroActive.Value = false;
        PV.Controller.CustomProperties["dogValue"] = true;
        dogAtivo.Value = true;

        triggerCollisionsScript = GetComponent<TriggerCollisionsController>();
    }


    void Update()
    {
        if (PV == null || PV.IsMine || !PhotonNetwork.InRoom)
        {
            if (desativaPower.Value == true)
            {
                if (!PhotonNetwork.InRoom)
                {
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

            if (sequestrado)
            {
                TransformaPet(false);
            }
            else
            {
                if (!dogAtivo.Value/* && desativouDog == false*/)
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

                //if(dogAtivo.Value && ativouDog == false)
                else
                {
                    //if (ativouDog == false)
                    //{
                    if (!PhotonNetwork.InRoom)
                    {
                        TransformaPet(true);
                    }
                    else
                    {
                        gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true);
                    }
                    // }
                }
            }

        }



    }


    [PunRPC]
    public void Carro()
    {
        if (carroActive.Value == false && pipaActive.Value == false && sequestrado == false)
        {

            hitTotemCarro.Value = false;
            carroActive.Value = true;

            dogAtivo.Value = false;
        }

    }



    [PunRPC]
    public void Pipa()
    {
        if (carroActive.Value == false && pipaActive.Value == false && sequestrado == false)
        {
            hitTotemPipa.Value = false;
            pipaActive.Value = true;

            dogAtivo.Value = false;
        }
    }

    [PunRPC]
    public void DesativaPowerUps()
    {
        dogAtivo.Value = true;

        pipaActive.Value = false;

        carroActive.Value = false;
        desativaPower.Value = false;

        triggerCollisionsScript.isDogNormal = true;
    }

    [PunRPC]
    private void TransformaPet(bool isDog)
    {
        Pet.SetActive(isDog);

        if (!isDog)
        {
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
        }
        /*timePet += Time.deltaTime;
        if (!isDog)
        {
            dogAnim.PetChange(isDog);
            //buttonPressed.Value = false;
            if (timePet >= .5f)
            {
                Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
                desativouDog = true;
                ativouDog = false;
                Pet.SetActive(isDog);
                timePet = 0;
                dogAnim.ativaDog = false;
            }
        }

        else
        {
            Pet.SetActive(isDog);
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
            dogAnim.PetChange(isDog);
            if (timePet >= .5f)
            {
                //Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
                desativouDog = false;
                ativouDog = true;
                //dogAnim.PetChange(isDog);
                timePet = 0;
                dogAnim.ativaDog = false;
            }
        }*/

    }

}
