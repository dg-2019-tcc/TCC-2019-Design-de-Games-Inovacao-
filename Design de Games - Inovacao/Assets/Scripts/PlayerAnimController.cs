using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class PlayerAnimController : MonoBehaviour
{
    private AnimDB animDB;
    public NewPlayerMovent playerMovement;
    private DogController dogController;
    private ButtonA buttonA;
    private Controller2D controller;
    private InputController inputController;

    public AnimState01 nextAnimState01 = AnimState01.None;
    public AnimState02 nextAnimState02 = AnimState02.None;
    public AnimState03 nextAnimState03 = AnimState03.None;
    public AnimState04 nextAnimState04 = AnimState04.None;

    public AnimState04 oldAnimState04 = AnimState04.None;


    public PlayerAnimInfo playerAnimInfo;

    public AnimInfo animInfo;
    public PlayerInfo playerInfo;

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
        playerAnimInfo = GetComponent<PlayerAnimInfo>();
    }

    private void FixedUpdate()
    {
        CheckNormalMovement();

        if (GameManager.acabouFase) { WinLoseAnim(); }
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
            }
        }
    }

    //State01 é WinLoseAnim() e StunAnim()
    void WinLoseAnim()
    {
        Debug.Log(GameManager.acabouFase);
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
        else  { nextAnimState02 = AnimState02.CarroWalk; }
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
        AtualizaInfo();

        if (playerAnimInfo.playerInfo.velocity.y < 1f && controller.collisions.below == false) { animInfo.anim04 = AnimState04.Falling; }
        else if (playerAnimInfo.playerInfo.jump || playerAnimInfo.playerInfo.velocity.y > 0) { animInfo.anim04 = AnimState04.Rising; }
        else if (playerAnimInfo.playerInfo.input.x != 0 && playerAnimInfo.playerInfo.isGrounded) { animInfo.anim04 = AnimState04.Walk; }
        else { animInfo.anim04 = AnimState04.Idle; }

        #region Com Animação de landing travando
        /*if (animInfo.oldAnim04 == AnimState04.Falling)
        {
            if (playerInfo.isGrounded == true) { return; }
            else { animInfo.anim04 = AnimState04.Aterrisando; }
        }
        else
        {
            if (playerMovement.velocity.y < 1f && controller.collisions.below == false) { animInfo.anim04 = AnimState04.Falling; }
            else if (playerMovement.jump || playerMovement.velocity.y > 0) { animInfo.anim04 = AnimState04.Rising;  }
            else if (inputController.joyInput.x != 0 && controller.collisions.below) { animInfo.anim04 = AnimState04.Walk;  }
            else if (animInfo.oldAnim04 == AnimState04.Walk || animInfo.oldAnim04 == AnimState04.None) { animInfo.anim04 = AnimState04.Idle; }
        }*/
        #endregion
        CallState04();
    }

    public void AtualizaInfo()
    {
        if (playerInfo.velocity != playerMovement.velocity) { playerInfo.velocity = playerMovement.velocity; }
        if (playerInfo.input != inputController.joyInput) { playerInfo.input = inputController.joyInput; }
        if (playerInfo.isGrounded != controller.collisions.below) { playerInfo.isGrounded = controller.collisions.below; }
        if (playerInfo.jump != playerMovement.jump) { playerInfo.jump = playerMovement.jump; }
    }
    void CallState04()
    {
        animDB.CallAnimState04(animInfo.anim04);
        animInfo.oldAnim04 = animInfo.anim04;
        coolToNext = 0;
    }

    public struct PlayerInfo
    {
        public Vector2 velocity;
        public Vector2 input;
        public bool isGrounded;
        public bool jump;

    }

    public struct AnimInfo
    {
        public AnimState01 anim01;
        public AnimState02 anim02;
        public AnimState03 anim03;
        public AnimState04 anim04;

        public AnimState04 oldAnim04;
    }


    void Cooldown()
    {
        coolToNext += Time.deltaTime;
    }
}
