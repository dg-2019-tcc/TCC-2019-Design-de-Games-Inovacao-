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

    public AnimState04 nextAnimState04 = AnimState04.None;

    [SerializeField]
    private float coolToNext;

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
        NormalMovement();
        if (coolToNext >= 0.2f) { CallNormalMovementAnim(); }
    }

    void NormalMovement()
    {
        //if(playerMovement.velocity.y < 0 && controller.collisions.below == true) {nextAnimState04 = AnimState04.Aterrisando; return; }
        //else if(playerMovement.velocity.y < 0 && controller.collisions.below == false){ nextAnimState04 = AnimState04.Falling; return; }
        /*else*/ if(playerMovement.jump) { nextAnimState04 = AnimState04.Rising;  }
        else if(inputController.joyInput.x != 0 && controller.collisions.below) { nextAnimState04 = AnimState04.Walk; return; }
        //else { nextAnimState04 = AnimState04.Idle; return; }
    }

    void Cooldown()
    {
        coolToNext += Time.deltaTime;
    }
    void CallNormalMovementAnim()
    {
        animDB.CallAnimState04(nextAnimState04);
        coolToNext = 0;
    }
}
