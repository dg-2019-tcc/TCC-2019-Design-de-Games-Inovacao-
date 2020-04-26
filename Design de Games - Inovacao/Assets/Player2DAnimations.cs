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

    public State state = State.Walking;

    private Controller2D controller;
    [SerializeField]
    private UnityArmatureComponent player;

    public Armature armature;

    public bool dogButtonAnim;

    public int fase;


    //private DragonBones.AnimationState aimState = null;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        player.animation.Init(armature);
        state = State.Idle;
    }

    private void Update()
    {
        if(state == State.Idle)
        {
            frente.SetActive(true);
            lado.SetActive(false);
        }

        else
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }
    }



    public void ChangeMoveAnim(Vector2 moveAmount, Vector2 oldPos, Vector2 input, bool Jump, bool stopJump)
    {
        //frente.SetActive(false);
        //lado.SetActive(true);

        float moveX = Mathf.Abs(moveAmount.x);



        if(controller.collisions.below && input.x == 0 && input.y < 0 && dogButtonAnim == false)
        {
            Abaixar();
        }

        else if (oldPos.y < moveAmount.y /*&& controller.collisions.below == false */&& dogButtonAnim == false)
        {
            NoArUp();
        }

        else if (moveAmount.y < -1 /*&& controller.collisions.below == false */&& dogButtonAnim == false)
        { 
            Fall();
        }

        else if (moveAmount.y < -1 && controller.collisions.below == true && dogButtonAnim == false)
        {
            Aterrisando();
        }

        else if (input.x != 0 && controller.collisions.below && dogButtonAnim == false)
        {
            Walking();
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
                state = State.Idle;
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
                Arremesso();
            }

            else if (fase == 1)
            {
                Chute();
            }
        }
    }
    [PunRPC]
    public void Idle()
    {
        if(state != State.Idle)
        {
            state = State.Idle;
        }
    }

    [PunRPC]
    public void Walking()
    {
        if (state != State.Walking)
        {
            player.animation.FadeIn(walkAnimation, 0.2f,0);
            player.animation.timeScale = 1;
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
            player.animation.FadeIn(subindoJumpAnimation, 0.1f, 0);
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
           
            player.animation.FadeIn(descendoJumpAnimation, 0.1f,0);
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
            player.animation.FadeIn(aterrisandoAnimation, 0.1f, 1);
            player.animation.timeScale = 1;
            state = State.Aterrisando;
        }
    }

    [PunRPC]
    public void Chute()
    {
        if(state != State.Chutando)
        {
            player.animation.FadeIn(chuteAnimation, 0.1f, 1);
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
            player.animation.FadeIn(arremessoAnimation, 0f, 1);
            player.animation.timeScale = 1;
            state = State.Arremessando;
        }
    }

    [PunRPC]
    public void Abaixar()
    {
        if(state != State.Abaixando)
        {
            player.animation.FadeIn(abaixarAnimation, 0.2f, 1);
            player.animation.timeScale = 1;
            state = State.Abaixando;
        }
    }

    public void TransitionAir()
    {
        if(state != State.TransitionAir && state == State.Rising)
        {
            Debug.Log("Transition");
            player.animation.FadeIn(transitionJumpAnimation, 0.2f, 1);
            state = State.TransitionAir;
        }
    }

}
