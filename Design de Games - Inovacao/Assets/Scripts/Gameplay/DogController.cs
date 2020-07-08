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

    public enum State{Idle,Desativado, Carro, Pipa, Aviao}

    public State state = State.Idle;

    bool sequestrado;

    public bool isCorrida;

    bool isOnline;

    private ButtonA buttonA;


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

        if (acabou01.Value[4] == true && aiGanhou.Value[5] == false && acabou01.Value[8] == false)
        {
            Debug.Log("Desativa");
            sequestrado = true;
            ChangeState("DesativadoState");

            //TransformaPet(false);
        }
        else if(((acabou01.Value[6] == true && aiGanhou.Value[7] == false) && isCorrida == false) || acabou01.Value[8] == true)
        {
            Debug.Log("Ativa");
            sequestrado = false;
            ChangeState("IdleState");
        }

        if (PhotonNetwork.InRoom)
        {
            isOnline = true;
        }
        else
        {
            isOnline = false;
        }

        PV = gameObject.GetComponent<PhotonView>();
        buttonA = GetComponent<ButtonA>();
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
                Debug.Log("dESATIVA");
                ChangeState("IdleState");
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
                ChangeState("DesativadoState");
            }
        }
    }

    [PunRPC]
    public void ChangeState(string changeState)
    {
        if (isOnline)
        {
            PV.RPC("DogState", RpcTarget.All, changeState);
        }
        else
        {
            DogState(changeState);
        }
    }

    [PunRPC]
    public void DogState(string dogState)
    {
        switch (dogState)
        {
            case "CarroState":
                if (state == State.Idle)
                {
                    Pet.SetActive(false);

                    buttonA.state = ButtonA.State.PowerUp;

                    hitTotemCarro.Value = false;
                    carroActive.Value = true;
                    dogAtivo.Value = false;
                    state = State.Carro;
                }
                break;

            case "PipaState":
                if (state == State.Idle)
                {
                    Pet.SetActive(false);

                    buttonA.state = ButtonA.State.PowerUp;

                    hitTotemPipa.Value = false;
                    pipaActive.Value = true;
                    dogAtivo.Value = false;
                    state = State.Pipa;
                }
                break;

            case "TiroState":
                if (state == State.Idle)
                {
                    Pet.SetActive(false);
                    state = State.Aviao;
                }
                break;

            case "DesativadoState":
                if (state == State.Idle)
                {
                    Pet.SetActive(false);

                    dogAtivo.Value = false;
                    pipaActive.Value = false;
                    carroActive.Value = false;
                }
                break;

            case "IdleState":
                if (state != State.Idle)
                {
                    Pet.SetActive(true);
                    Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);

                    dogAtivo.Value = true;
                    pipaActive.Value = false;
                    carroActive.Value = false;
                    desativaPower.Value = false;
                    triggerCollisionsScript.isDogNormal = true;

                    state = State.Idle;
                }
                break;
        }
    } 

}
