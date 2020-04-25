using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Player2DAnimations : MonoBehaviour
{
    public GameObject frente;
    public GameObject lado;


    public UnityArmatureComponent player;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ChangeMoveAnim(Vector2 moveAmount, Vector2 input, bool jump)
    {
        if(input.x != 0 && jump == false)
        {
            lado.SetActive(true);
            frente.SetActive(false);
        }

        else
        {
            frente.SetActive(true);
            lado.SetActive(false);
        }


        if(moveAmount.x > 0 || moveAmount.x < 0 && moveAmount.y == 0)
        {
            Walking();
        }

        if (jump)
        {
            StartPulo();
        }

        if (moveAmount.y > 0)
        {
            NoArUp();
        }

        if (moveAmount.y < 0)
        {
            NoArDown();
        }
    }

    public void Walking()
    {
        Debug.Log("Walking");
        player.animation.Play("0_Corrida");
    }

    public void StartPulo()
    {
        Debug.Log("Pulo");
        player.animation.Play("1_Pulo");
    }

    public void NoArUp()
    {
        player.animation.Play("1_NoAr(1_Subindo)");

    }

    public void NoArDown()
    {
        player.animation.Play("1_NoAr(1_Descendo)");
    }

}
