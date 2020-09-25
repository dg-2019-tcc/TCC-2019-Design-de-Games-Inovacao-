using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class DogController : MonoBehaviour
    {

        TriggerCollisionsController triggerCollisionsScript;
        public DogAnim dogAnim;

        public GameObject player;
        public GameObject playerModel;
        private Transform target;
        public float speedToTotem = 10f;

        [HideInInspector]
        public bool ativouDog;
        [HideInInspector]
        public bool desativouDog;

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

        public enum State { Idle, Desativado, Carro, Pipa, Aviao }
        public State state = State.Idle;

        public bool sequestrado;

        public bool isCorrida;

        bool isOnline;

        private ButtonA buttonA;
        public bool isTutorial;

        public ParticleSystem puffOff;

        void Start()
        {
            if (GameManager.historiaMode == true)
            {
                GameManager.Instance.ChecaFase();
                if (GameManager.sequestrado == true || GameManager.sceneAtual == UnityCore.Scene.SceneType.Tutorial2|| GameManager.levelIndex == 5 || GameManager.levelIndex == 6)
                {
                    sequestrado = true;
                    ChangeState("DesativadoState");
                    //Debug.Log("Sequestrado: " + sequestrado);
                }
                else
                {
                    sequestrado = false;
                    ChangeState("IdleState");
                    //Debug.Log("Sequestrado: " + sequestrado);
                }
                
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
            //PV.Controller.CustomProperties["dogValue"] = true;
            //dogAtivo.Value = true;

            triggerCollisionsScript = GetComponent<TriggerCollisionsController>();

            if (puffOff.isPlaying)
            {
                puffOff.Stop();
            }
        }


        void Update()
        {
            if (PV == null || PV.IsMine || !PhotonNetwork.InRoom)
            {
                if (desativaPower.Value == true)
                {
                    ChangeState("IdleState");
                }

                if (hitTotemCarro.Value == true || hitTotemPipa.Value == true)
                {
                    target = ItemThrow.totemTarget;
                    float step = speedToTotem * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }

                //PV.Controller.CustomProperties["dogValue"] = dogAtivo.Value;


                if (GameManager.historiaMode == true)
                {
                    if (sequestrado)
                    {
                        ChangeState("DesativadoState");
                    }
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


        IEnumerator DogDisplay(bool isOn)
        {
            if (isOn)
            {
                Pet.SetActive(isOn);
                puffOff.Play();
            }
            else
            {
                puffOff.Play();
            }

            yield return new WaitForSeconds(.5f);

            if (isOn) puffOff.Stop();
            else
            {
                puffOff.Stop();
                Pet.SetActive(isOn);
            }
        }


        [PunRPC]
        public void DogState(string dogState)
        {
            switch (dogState)
            {
                case "CarroState":
                    if (state == State.Idle || state ==State.Aviao)
                    {
                        StartCoroutine("DogDisplay", false);

                        buttonA.state = ButtonA.State.PowerUp;

                        hitTotemCarro.Value = false;
                        carroActive.Value = true;
                        dogAtivo.Value = false;
                        state = State.Carro;
                    }
                    break;

                case "PipaState":
                    if (state == State.Idle || state == State.Aviao)
                    {
                        StartCoroutine("DogDisplay", false);

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
                        StartCoroutine("DogDisplay", false);

                        state = State.Aviao;
                    }
                    break;

                case "DesativadoState":
                    if (state != State.Desativado)
                    {
                        Pet.SetActive(false);

                        dogAtivo.Value = false;
                        pipaActive.Value = false;
                        carroActive.Value = false;

                        state = State.Desativado;
                    }
                    break;


                case "IdleState":
                    if (state != State.Idle)
                    {
                        StartCoroutine("DogDisplay", true);

                        ativouDog = true;
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
}
