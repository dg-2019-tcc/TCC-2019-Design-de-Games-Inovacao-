using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Player2DAnimations : MonoBehaviour
{
    public GameObject frente;
    public GameObject lado;

    public string idlePose = "SettingPose_SD";
    public string walkAnimation = "0_Corrida_V2";
    public string startJumpAnimation = "1_Pulo";
    public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
    public string transitionJumpAnimation = "1_NoAr(2_Transicao)";
    public string descendoJumpAnimation = "1_NoAr(3_Descendo)";
    public string aterrisandoAnimation = "1_Aterrisando";
    public string chuteAnimation = "3_Bicuda(SemPreparacao)";
    public string abaixarAnimation = "5_Abaixar(2_IdlePose)";
    public string arremessoAnimation = "6_Arremessar(3_Arremesso)";

    public enum State {Idle, Walking, Jumping, Rising, Falling, TransitionAir, Aterrisando, Chutando, Abaixando, Arremessando}

    public State state = State.Idle;

    private Controller2D controller;
    [SerializeField]
    private UnityArmatureComponent player;

    public Armature armature;

    public bool dogButtonAnim;

    public int fase;

    [HideInInspector]
    public PhotonView photonView;

    float moveX;
    float inputX;

    [SerializeField]
    private bool jaAterrisou;
    [SerializeField]
    private float coolToIdle;

    public BoolVariable pipaActive;
    public BoolVariable carroActive;

    public bool isCorrida;


    //private DragonBones.AnimationState aimState = null;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        controller = GetComponent<Controller2D>();
        player.animation.Init(armature);
        if (!isCorrida)
        {
            state = State.Idle;
        }
        else
        {
            state = State.Abaixando;
            frente.SetActive(false);
        }
    }


    [PunRPC]
    void ChangeArmature()
    {
        lado.SetActive(true);
        frente.SetActive(false);
    }



    public void ChangeMoveAnim(Vector2 moveAmount, Vector2 oldPos, Vector2 input, bool Jump, bool stopJump)
    {

        if (!PhotonNetwork.InRoom || photonView.IsMine)
        {
            if(carroActive.Value == true || pipaActive.Value == true)
            {
                Idle();
            }

            //frente.SetActive(false);
            //lado.SetActive(true);
            moveX = Mathf.Abs(moveAmount.x);

            if (state == State.Aterrisando || state == State.Walking || state == State.Abaixando)
            {
                if (state == State.Walking && input.x == 0)
                {
                    coolToIdle = 0.66f;
                }
                else
                {
                    coolToIdle += Time.deltaTime;
                }
            }

            if(moveAmount.y < -2 && jaAterrisou && state != State.Aterrisando && pipaActive.Value == false && carroActive.Value == false)
            {
                jaAterrisou = false;
            }

            if (oldPos.y < moveAmount.y && controller.collisions.below == false && dogButtonAnim == false && pipaActive.Value == false && carroActive.Value == false)
            {
                jaAterrisou = false;
                if (!PhotonNetwork.InRoom)
                {
                    NoArUp();
                }
                else
                {
                    photonView.RPC("NoArUp", RpcTarget.All);
                }
            }

            else if (moveAmount.y < 9 && moveAmount.y > 0 && controller.collisions.below == false && dogButtonAnim == false && pipaActive.Value == false && carroActive.Value == false)
            {

                if (!PhotonNetwork.InRoom)
                {
                    TransitionAir();
                }
                else
                {
                    photonView.RPC("TransitionAir", RpcTarget.All);
                }
            }

            else if (moveAmount.y <= 0 && controller.collisions.below == false && dogButtonAnim == false && jaAterrisou == false && pipaActive.Value == false && carroActive.Value == false)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Fall();
                }
                else
                {
                    photonView.RPC("Fall", RpcTarget.All);
                }
            }

            else if (moveAmount.y < -1f && input.x == 0 && input.y >=0 && controller.collisions.below == true && dogButtonAnim == false && jaAterrisou == false && pipaActive.Value == false && carroActive.Value == false)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Aterrisando();
                }
                else
                {
                    photonView.RPC("Aterrisando", RpcTarget.All);
                }
            }

            else if (controller.collisions.below && input.x == 0 && input.y < 0 && dogButtonAnim == false && pipaActive.Value == false && carroActive.Value == false)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Abaixar();
                }
                else
                {
                    photonView.RPC("Abaixar", RpcTarget.All);
                }
            }

            else if (input.x != 0 && controller.collisions.below && dogButtonAnim == false && pipaActive.Value == false && carroActive.Value == false)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Walking(inputX, oldPos, moveAmount);
                }
                else
                {
                    photonView.RPC("Walking", RpcTarget.All, moveX, oldPos, moveAmount);
                }

            }

            else 
            {
                if (Jump == false && dogButtonAnim == false && stopJump == false && jaAterrisou == true && coolToIdle >= 0.33f)
                {
                    if (!PhotonNetwork.InRoom)
                    {
                        Idle();
                    }
                    else
                    {
                        photonView.RPC("Idle", RpcTarget.All);
                    }
                }
            }
        }
    }

    public void DogButtonAnim(bool dogButtonOn)
    {
        dogButtonAnim = dogButtonOn;

        if (dogButtonOn == true)
        {

            if (fase == 0)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Arremesso();
                }
                else
                {
                    Debug.Log("Anim");
                    photonView.RPC("Arremesso", RpcTarget.All);
                }
            }

            else if (fase == 1)
            {
                if (!PhotonNetwork.InRoom)
                {
                    Chute();
                }
                else
                {
                    photonView.RPC("Chute", RpcTarget.All);
                }
            }
        }

        else
        {
            Idle();
        }
    }
    [PunRPC]
    public void Idle()
    {
        if(state != State.Idle)
        {
            coolToIdle = 0;
            frente.SetActive(true);
            lado.SetActive(false);
            state = State.Idle;
        }
    }

    [PunRPC]
    public void Walking(float animTime, Vector2 oldPos, Vector2 moveAmount)
    {
        if(state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }

        if (state != State.Walking )
        {
            player.animation.timeScale = 1.35f;
            player.animation.Play(walkAnimation);
            state = State.Walking;
        }
    }

    [PunRPC]
    public void StartPulo()
    {
        if (state != State.Jumping)
        {
            player.animation.FadeIn(startJumpAnimation,0f,1);
            player.animation.timeScale = 1;
            state = State.Jumping;
            //Debug.Log(state);
        }
    }

    [PunRPC]
    public void NoArUp()
    {
        if (state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }


        if (state != State.Rising)
        {
            player.animation.timeScale = 1;
            player.animation.Play(subindoJumpAnimation);
            state = State.Rising;
        }
    }

    [PunRPC]
    public void Fall()
    {
        if (state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }



        if (state != State.Falling)
        {
            player.animation.timeScale = 1;
            player.animation.Play(descendoJumpAnimation);
            state = State.Falling;
        }
    }

    [PunRPC]
    public void Aterrisando()
    {
        if (state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }


        if (state != State.Aterrisando)
        {
            player.animation.timeScale = 1.5f;
            player.animation.Play(aterrisandoAnimation);
            state = State.Aterrisando;
            jaAterrisou = true;
        }
    }

    [PunRPC]
    public void Chute()
    {
        if (state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }

        if (state != State.Chutando)
        {
            player.animation.timeScale = 0.5f;
            player.animation.Play(chuteAnimation);
            state = State.Chutando;
        }
    }

    [PunRPC]
    public void Arremesso()
    {

        if (state == State.Idle)
        {
                lado.SetActive(true);
                frente.SetActive(false);
        }

        if (state != State.Arremessando)
            {
                player.animation.timeScale = 1;
                player.animation.Play(arremessoAnimation);
                state = State.Arremessando;
            }
        }

    [PunRPC]
    public void Abaixar()
    {
        if (state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }


        if (state != State.Abaixando)
        {
            player.animation.timeScale = 1;
            player.animation.Play(abaixarAnimation);
            state = State.Abaixando;
        }
    }

    [PunRPC]
    public void TransitionAir()
    {
        if (state == State.Idle)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }


        if (state != State.TransitionAir)
        {
            player.animation.timeScale = 1.5f;
            player.animation.Play(transitionJumpAnimation);
            state = State.TransitionAir;
        }
    }

}
