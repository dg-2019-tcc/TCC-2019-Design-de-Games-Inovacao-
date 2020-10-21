using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerAnimationsDB playerAnim;
    public NewPlayerMovent playerMovement;
    private DogController dogController;
    private ButtonA buttonA;
    private Controller2D controller;
    private InputController inputController;

    public AnimStateFrente nextAnimState01 = AnimStateFrente.None;
    public AnimStatePowerUp nextAnimState02 = AnimStatePowerUp.None;
    public AnimStateAction nextAnimState03 = AnimStateAction.None;
    public AnimStateMovement nextAnimState04 = AnimStateMovement.None;
    public AnimStateMovement oldAnimState04 = AnimStateMovement.None;

    public PlayerAnimInfo playerAnimInfo;
    public PlayerInfo playerInfo;

    [SerializeField] private float coolToNext;
    [HideInInspector] public bool dogButtonAnim;
    private bool callDogButtonAnim;
    public BoolVariable levouDogada;

    #region Unity Function
    private void Start()
    {
        playerAnim = GetComponent<PlayerAnimationsDB>();
        playerMovement = GetComponent<NewPlayerMovent>();
        dogController = GetComponent<DogController>();
        buttonA = GetComponent<ButtonA>();
        controller = GetComponent<Controller2D>();
        inputController = GetComponent<InputController>();
        playerAnimInfo = GetComponent<PlayerAnimInfo>();
    }

    private void Update()
    {
        ShouldUpdate();
        UpdateAnimation();
        coolToNext = 0;
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void ShouldUpdate()
    {
        Cooldown();
        if (coolToNext < 0.2f) { return; }
        if (GameManager.acabouFase) { WinLoseAnim(); }
        if (levouDogada.Value) { StunAnim(); return; }
    }

    void UpdateAnimation()
    {
        if (dogController.state.Equals(DogController.State.Carro) || dogController.state.Equals(DogController.State.Pipa))
        {
            PowerUpUpdate();
        }
        else
        {
            playerAnim.updateCar = false;
            playerAnim.statePowerUp = AnimStatePowerUp.None;
            playerAnim.StatePowerUpUpdate(false);

            if (dogButtonAnim) { DogButton(); }
            else
            {
                callDogButtonAnim = false;
                playerAnim.statePowerUp = AnimStatePowerUp.None;
                playerAnim.StateActionUpdate(false);
                CheckNormalMovement();
            }
        }
    }

    //State01 é WinLoseAnim() e StunAnim()

    void WinLoseAnim()
    {
        if (GameManager.ganhou) { nextAnimState01 = AnimStateFrente.Ganhou; }
        else { nextAnimState01 = AnimStateFrente.Perdeu; }

        if (GameManager.perdeu) { nextAnimState01 = AnimStateFrente.Perdeu; }

        playerAnim.stateFrente = nextAnimState01;
        playerAnim.StateFrenteUpdate();
    }
    void StunAnim() { nextAnimState01 = AnimStateFrente.Stun; playerAnim.StateFrenteUpdate(); }

    //State02
    void PowerUpUpdate()
    {
        if (dogController.state.Equals(DogController.State.Carro)) { CarroAnim(); }
        else { PipaAnim(); playerAnim.updateCar = false; }
        CallState02();
    }

    void CarroAnim()
    {
        playerAnim.updateCar = true;
        if (playerMovement.carroVelocity.y < -1 && controller.collisions.below == false) { nextAnimState02 = AnimStatePowerUp.CarroDown; }
        else if (playerMovement.carroVelocity.y > 1 && controller.collisions.below == false) { nextAnimState02 = AnimStatePowerUp.CarroUp; }
        else { nextAnimState02 = AnimStatePowerUp.CarroWalk; }

    }
    void PipaAnim()
    {
        nextAnimState02 = AnimStatePowerUp.Pipa;
    }
    void CallState02()
    {
        playerAnim.statePowerUp = nextAnimState02;
        playerAnim.StatePowerUpUpdate(true);
    }

    //State03
    void DogButton()
    {
        if (callDogButtonAnim) return;
        if (buttonA.state.Equals(ButtonA.State.Atirar))
        {
            nextAnimState03 = AnimStateAction.Arremesando;
        }

        else if (buttonA.state.Equals(ButtonA.State.Chutar))
        {
            nextAnimState03 = AnimStateAction.Chute;
        }
        callDogButtonAnim = true;
        CallState03();
    }
    void CallState03()
    {
        playerAnim.stateAction = nextAnimState03;
        playerAnim.StateActionUpdate(true);
        coolToNext = 0;
    }

    //State04
    void CheckNormalMovement()
    {
        AtualizaInfo();
        #region Com Animação de landing travando
        if (nextAnimState04 == AnimStateMovement.Falling)
        {

            while (playerInfo.isGrounded == false) { return; }
            nextAnimState04 = AnimStateMovement.Aterrisando;
        }
        else
        {
            if (playerMovement.velocity.y < 1f && controller.collisions.below == false) { nextAnimState04 = AnimStateMovement.Falling; nextAnimState01 = AnimStateFrente.None; }
            else if (playerMovement.jump || playerMovement.velocity.y > 0) { nextAnimState04 = AnimStateMovement.Rising; nextAnimState01 = AnimStateFrente.None; }
            else if (inputController.joyInput.x != 0 && controller.collisions.below) { nextAnimState04 = AnimStateMovement.Walk; nextAnimState01 = AnimStateFrente.None; }
            else { nextAnimState01 = AnimStateFrente.Idle; }
        }
        #endregion
        CallState04();
    }

    private void AtualizaInfo()
    {
        if (playerInfo.velocity != playerMovement.velocity) { playerInfo.velocity = playerMovement.velocity; }
        if (playerInfo.input != inputController.joyInput) { playerInfo.input = inputController.joyInput; }
        if (playerInfo.isGrounded != controller.collisions.below) { playerInfo.isGrounded = controller.collisions.below; }
        if (playerInfo.jump != playerMovement.jump) { playerInfo.jump = playerMovement.jump; }
    }
    void CallState04()
    {
        if (nextAnimState01 == AnimStateFrente.None)
        {
            playerAnim.stateMovement = nextAnimState04;
            playerAnim.StateMoveUpdate(true);
        }
        else
        {
            playerAnim.stateFrente = nextAnimState01;
            playerAnim.StateFrenteUpdate();
        }

        coolToNext = 0;
    }

    void Cooldown()
    {
        coolToNext += Time.deltaTime;
    }
    #endregion

    public struct PlayerInfo
    {
        public Vector2 velocity;
        public Vector2 input;
        public bool isGrounded;
        public bool jump;

    }
}
