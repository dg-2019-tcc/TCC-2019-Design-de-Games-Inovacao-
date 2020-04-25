using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Player2DAnimations : MonoBehaviour
{
    public GameObject frente;
    public GameObject lado;

    public string walkAnimation = "0_Corrida";
    public string startJumpAnimation = "1_Pulo";
    public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
    public string transitionJumpAnimation = "1_NoAr(2_Transicao)";
    public string descendoJumpAnimation = "1_NoAr(3_Descendo)";

    enum State { Walking, Jumping, Rising, Falling, TransitionAir}

    private State state = State.Walking;

    private Controller2D controller;

    public UnityArmatureComponent player;
    private DragonBones.AnimationState aimState = null;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
    }



    public void ChangeMoveAnim(Vector2 moveAmount, Vector2 input, bool jump)
    {
        //frente.SetActive(false);
        //lado.SetActive(true);

        if(moveAmount.x > 0 || moveAmount.x < 0 && controller.collisions.below)
        {
            Walking();
        }

        /*else if (moveAmount.x < 0 && controller.collisions.below)
        {
            Walking();
        }*/

        else if(moveAmount.y>-1 && moveAmount.y < 1)
        {
            TransitionAir();
        }

        else if(moveAmount.y < -1)
        {
            Fall();
        }



    }

    public void Walking()
    {
        if (state != State.Walking)
        {
            Debug.Log("Walking");
            player.animation.FadeIn(walkAnimation, 0.1f,-1);
            state = State.Walking;
        }
    }

    public void StartPulo()
    {
        if (state != State.Jumping)
        {
            Debug.Log("Pulo");
            player.animation.FadeIn(startJumpAnimation,0.1f,1);
            state = State.Jumping;
            NoArUp();
        }
    }

    public void NoArUp()
    {
        if (state != State.Rising)
        {
            player.animation.FadeIn(subindoJumpAnimation, 0.1f, 1);
            state = State.Rising;
        }
    }

    public void Fall()
    {
        if (state != State.Falling)
        {
            player.animation.FadeIn(descendoJumpAnimation, 0.1f,1);
            state = State.Falling;
        }
    }

    public void TransitionAir()
    {
        if(state != State.TransitionAir)
        {
            player.animation.FadeIn(transitionJumpAnimation, 0.1f, 1);
            state = State.TransitionAir;
        }
    }

}
