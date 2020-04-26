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
    public string walkAnimation = "0_Corrida";
    public string startJumpAnimation = "1_Pulo";
    public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
    public string transitionJumpAnimation = "1_NoAr(2_Transicao)";
    public string descendoJumpAnimation = "1_NoAr(3_Descendo)";
    public string aterrisandoAnimation = "1_Aterrisando";
    public string chuteAnimation = "3_Bicuda(SemPreparacao)";
    public string abaixarAnimation = "5_Abaixar(2_IdlePose)";
    public string arremessoAnimation = "6_Arremessar(3_Arremesso)";

    public Vector2 oldPos;

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


    //private DragonBones.AnimationState aimState = null;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        controller = GetComponent<Controller2D>();
        player.animation.Init(armature);
        state = State.Idle;
    }

    private void FixedUpdate()
    {
        if (!PhotonNetwork.InRoom || photonView.IsMine)
        {
            if (state != State.Idle)
            {
                ChangeArmature();
            }

        }

        else
        {
            photonView.RPC("ChangeArmature", RpcTarget.All);
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
            //frente.SetActive(false);
            //lado.SetActive(true);
            moveX = Mathf.Abs(moveAmount.x);


            if (oldPos.y < moveAmount.y && controller.collisions.below == false && dogButtonAnim == false)
            {
                if (!PhotonNetwork.InRoom)
                {
                    NoArUp();
                }
                else
                {
                    photonView.RPC("NoArUp", RpcTarget.All);
                }
            }

            else if (moveAmount.y < 9 && moveAmount.y > 0 && controller.collisions.below == false && dogButtonAnim == false)
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

            else if (moveAmount.y <= 0 && controller.collisions.below == false && dogButtonAnim == false)
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

            else if (moveAmount.y < -1 && controller.collisions.below == true && dogButtonAnim == false)
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

            else if (controller.collisions.below && input.x == 0 && input.y < 0 && dogButtonAnim == false)
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

            else if (input.x != 0 && controller.collisions.below && dogButtonAnim == false)
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

            /*else if (ThrowObject.shootAnim == true)
            {
                Arremesso();
            }

            else if (FutebolPlayer.kicked)
            {
                Chute();
            }*/

            else
            {
                if (Jump == false && dogButtonAnim == false && stopJump == false)
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
    }
    [PunRPC]
    public void Idle()
    {
        if(state != State.Idle)
        {
            frente.SetActive(true);
            lado.SetActive(false);
            state = State.Idle;
        }
    }

    [PunRPC]
    public void Walking(float animTime, Vector2 oldPos, Vector2 moveAmount)
    {
        if (state != State.Walking )
        {
            //player.animation.FadeIn(walkAnimation, 0.1f,0);
            player.animation.Play(walkAnimation);
            player.animation.timeScale = 1.75f;
            state = State.Walking;
            //Debug.Log(state);
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
        if (state != State.Rising)
        {
            //player.animation.FadeIn(subindoJumpAnimation, 0.01f, 0);
            player.animation.Play(subindoJumpAnimation);
            player.animation.timeScale = 1;
            state = State.Rising;
            //Debug.Log(state);
        }
    }

    [PunRPC]
    public void Fall()
    {
        if (state != State.Falling)
        {

            //player.animation.FadeIn(descendoJumpAnimation, 0.25f,0);
            player.animation.Play(descendoJumpAnimation);
            player.animation.timeScale = 1;
            state = State.Falling;
            //Debug.Log(state);
        }
    }

    [PunRPC]
    public void Aterrisando()
    {
        if(state != State.Aterrisando)
        {
            //player.animation.FadeIn(aterrisandoAnimation, 0.01f, 1);
            player.animation.Play(aterrisandoAnimation);
            player.animation.timeScale = 1;
            state = State.Aterrisando;
        }
    }

    [PunRPC]
    public void Chute()
    {
        if(state != State.Chutando)
        {
            //player.animation.FadeIn(chuteAnimation, 0.1f, 1);
            player.animation.Play(chuteAnimation);
            player.animation.timeScale = 1;
            state = State.Chutando;
            Debug.Log(state);
        }
    }

    [PunRPC]
        public void Arremesso()
        {
            if(state != State.Arremessando)
            {
                //player.animation.FadeIn(arremessoAnimation, 0f, 1);
                player.animation.Play(arremessoAnimation);
                player.animation.timeScale = 1;
                state = State.Arremessando;
            }
        }

    [PunRPC]
    public void Abaixar()
    {
        if(state != State.Abaixando)
        {
            //player.animation.FadeIn(abaixarAnimation, 0.01f, 1);
            player.animation.Play(abaixarAnimation);
            player.animation.timeScale = 1;
            state = State.Abaixando;
        }
    }

    [PunRPC]
    public void TransitionAir()
    {
        if(state != State.TransitionAir)
        {
            //player.animation.FadeIn(transitionJumpAnimation, 0.2f, 1);
            player.animation.Play(transitionJumpAnimation);
            state = State.TransitionAir;
        }
    }

}
