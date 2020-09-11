using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class PlayerAnimController : MonoBehaviour
{
    private AnimDB animDB;
    private NewPlayerMovent playerMovement;
    private DogController dogController;
    private ButtonA buttonA;
    private Controller2D controller;
    private InputController inputController;

    public AnimState01 nextAnimState01 = AnimState01.None;
    public AnimState02 nextAnimState02 = AnimState02.None;
    public AnimState03 nextAnimState03 = AnimState03.None;
    public AnimState04 nextAnimState04 = AnimState04.None;

    public AnimState04 oldAnimState04 = AnimState04.None;

    [SerializeField] private float coolToNext;
    [HideInInspector] public bool dogButtonAnim;
    private bool callDogButtonAnim;
    public BoolVariable levouDogada;

    private void Start()
    {
        animDB = GetComponent<AnimDB>();
        playerMovement = GetComponent<NewPlayerMovent>();
        dogController = GetComponent<DogController>();
        buttonA = GetComponent<ButtonA>();
        controller = GetComponent<Controller2D>();
        inputController = GetComponent<InputController>();
    }

    private void FixedUpdate()
    {
        Cooldown();

        if (GameManager.acabouFase) { WinLoseAnim(); return; }
        if (levouDogada.Value) { StunAnim(); return; }

        if (dogController.state.Equals(DogController.State.Carro) || dogController.state.Equals(DogController.State.Pipa))
        {
            if (dogController.state.Equals(DogController.State.Carro)) { CarroAnim(); }
            else { PipaAnim(); }
            CallState02();
        }
        else
        {
            animDB.CallAnimState02(AnimState02.None);
            if (dogButtonAnim) { DogButton(); }
            else
            {
                callDogButtonAnim = false;
                animDB.CallAnimState03(AnimState03.None);
                CheckNormalMovement();
                //if (coolToNext >= 0.1f) { CallState04(); }
            }
        }
    }

    //State01 é WinLoseAnim() e StunAnim()
    void WinLoseAnim()
    {
        if (GameManager.ganhou) { nextAnimState01 = AnimState01.Ganhou; }
        else { nextAnimState01 = AnimState01.Perdeu; }

        if (GameManager.perdeu) { nextAnimState01 = AnimState01.Perdeu; }

        animDB.CallAnimState01(nextAnimState01);
    }
    void StunAnim() { nextAnimState01 = AnimState01.Stun; animDB.CallAnimState01(nextAnimState01); }

    //State02
    void CarroAnim()
    {
        if (playerMovement.carroVelocity.y < 0 && controller.collisions.below == false) { nextAnimState02 = AnimState02.CarroDown; }
        else if (playerMovement.jump) { nextAnimState02 = AnimState02.CarroUp; }
        else if (inputController.joyInput.x != 0 && controller.collisions.below) { nextAnimState02 = AnimState02.CarroWalk; }
    }
    void PipaAnim() => nextAnimState02 = AnimState02.Pipa;
    void CallState02()
    {
        animDB.CallAnimState02(nextAnimState02);
    }

    //State03
    void DogButton()
    {
        if (callDogButtonAnim) return;
        if (buttonA.state.Equals(ButtonA.State.Atirar))
        {
            nextAnimState03 = AnimState03.Arremesando;
        }

        else if (buttonA.state.Equals(ButtonA.State.Chutar))
        {
            nextAnimState03 = AnimState03.Chute;
        }
        callDogButtonAnim = true;
        CallState03();
        Debug.Log("[PlayerAnimController] DogButton()");
    }
    void CallState03()
    {
        animDB.CallAnimState03(nextAnimState03);
        coolToNext = 0;
    }

    //State04
    void CheckNormalMovement()
    {
        if(oldAnimState04 == AnimState04.Falling && controller.collisions.below == false) { return; }
        if (oldAnimState04 == AnimState04.Falling && controller.collisions.below == true) { nextAnimState04 = AnimState04.Aterrisando;}

        if (oldAnimState04 == AnimState04.Falling)
        {
            if (controller.collisions.below == false) { return; }
            else { nextAnimState04 = AnimState04.Aterrisando; }
        }
        else
        {
            if (playerMovement.velocity.y < 0 && controller.collisions.below == false) { nextAnimState04 = AnimState04.Falling; }
            else if (playerMovement.jump || playerMovement.velocity.y > 0) { nextAnimState04 = AnimState04.Rising; }
            else if (inputController.joyInput.x != 0 && controller.collisions.below) { nextAnimState04 = AnimState04.Walk; }
            else if (oldAnimState04 == AnimState04.Walk || oldAnimState04 == AnimState04.None) { nextAnimState04 = AnimState04.Idle; }
        }
        CallState04();
    }
    void CallState04()
    {
        animDB.CallAnimState04(nextAnimState04);
        oldAnimState04 = nextAnimState04;
        coolToNext = 0;
    }


    void Cooldown()
    {
        coolToNext += Time.deltaTime;
    }
}
