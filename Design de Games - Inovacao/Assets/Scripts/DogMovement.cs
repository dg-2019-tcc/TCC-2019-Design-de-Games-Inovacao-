using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DogMovement : MonoBehaviour
{
    public float speed;
    float velocityXSmoothing;
    float targetVelocityX;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float maxJumpVelocity;
    float minJumpVelocity;
    float gravity;
    public float maxJumpHeight;
    public float minJumpHeight;
    public float timeToJumpApex;

    Vector3 velocity;
    Vector2 input;
    Vector2 joyInput;

    [SerializeField]
    public FloatingJoystick joyStick;

    AIController2D dogController;

    public NewPlayerMovent playerMove;
    public TriggerCollisionsController playerTriggerController;

    public GameObject player;
    public GameObject dog;

    public UnityEngine.Transform dogPosInicial;

    public float delay =0.1f;

    bool isMoving;
    bool isJumping;

    float jumpTimes;

    bool isDown;

    public bool trick;

    private Animator anim;

    public UnityArmatureComponent dogArmature;

    public enum State { Idle, Aviao, Carro, Pipa, Moto }

    public State state = State.Idle;

    public int trickIndex;


    void Start()
    {
        anim = GetComponent<Animator>();

        dogController = GetComponent<AIController2D>();

        joyStick = FindObjectOfType<FloatingJoystick>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }




    void LateUpdate()
    {
        if (trick) return;
        joyInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
        //joyInput = playerMove.joyInput;
        if (joyInput.x > 0.3f || joyInput.x < -0.3f)
        {
            input.x = joyInput.x;
        }

        else
        {
            input.x = 0;
        }

        targetVelocityX = input.x * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (dogController.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        if (playerMove.slowFall == false)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -3.5f;
        }

        if (dogController.collisions.below && playerMove.jump && isJumping == false)
        {
            //DogJump();
            jumpTimes = 0;
            isJumping = true;
            Invoke("DogJump", delay);
        }

        if (dogController.collisions.above || dogController.collisions.below)
        {
            isJumping = false;
            velocity.y = 0;
        }

        if (transform.position.x > player.transform.position.x)
        {
            dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, Quaternion.Euler(0, 180, 0), 0.5f);

        }
        else if (transform.position.x < player.transform.position.x)
        {
            dog.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
        }

        if(transform.position.y - player.transform.position.y > 1)
        {
            input.y = -1;
        }

        else
        {
            input.y = joyInput.y;
        }

        if(transform.position.y - player.transform.position.y < -2 && isJumping == false)
        {
            DogJump();
            jumpTimes++;

            if(transform.position.x - player.transform.position.x > 0)
            {
                velocity.x = -1 * Mathf.Abs(transform.position.x - player.transform.position.x) - 1;
            }

            if (transform.position.x - player.transform.position.x < 0)
            {
                velocity.x = 1 * Mathf.Abs(transform.position.x - player.transform.position.x) + 1;
            }
        }

        if(transform.position.y - player.transform.position.y >= 0)
        {
            jumpTimes = 0;
        }

        if (transform.position.x < player.transform.position.x && joyInput.x < 0f ||transform.position.x > player.transform.position.x && joyInput.x > 0f || playerTriggerController.collisions.hitDog)
        {
            velocity.x = 0;
        }

        if (/*controller.collisions.below &&*/ playerTriggerController.collisions.caixaDagua)
        {
            velocity.y = maxJumpHeight * 1.8f;
        }

        dogController.Move(velocity * Time.deltaTime, input);
    }



    public void DogJump()
    {
        isJumping = true;

        if (jumpTimes > 0)
        {
            velocity.y = maxJumpHeight + (jumpTimes * 5f);
        }
        else
        {
            velocity.y = maxJumpHeight;
        }
    }

    public void DoTrick()
    {
        trickIndex = Random.Range(0, 5);
        Debug.Log(trickIndex);
        //trick = true;
        DogJump();
        DogAnim(trickIndex);
    }

    void DogAnim(int dogAnim)
    {
        switch (dogAnim)
        {
            case 0:
                if(state != State.Idle)
                {
                    anim.SetTrigger("DoTrick");
                    dogArmature.animation.Play("Base");
                    state = State.Idle;
                }
                break;

            case 1:
                if(state != State.Aviao)
                {
                    dogArmature.animation.Play("Aviao(Arremessar)");
                    anim.SetTrigger("AviaoTrigger");
                    state = State.Aviao;
                }
                break;

            case 2:
                if (state != State.Carro)
                {
                    dogArmature.animation.Play("Rolema");
                    anim.SetTrigger("CarroTrigger");
                    state = State.Carro;
                }
                break;

            case 3:
                if (state != State.Moto)
                {
                    dogArmature.animation.Play("Moto");
                    anim.SetTrigger("MotoTrick");
                    state = State.Moto;
                }
                break;

            case 4:
                if (state != State.Pipa)
                {
                    dogArmature.animation.Play("Pipa");
                    anim.SetTrigger("PipaTrigger");
                    state = State.Pipa;
                }
                break;
        }
    }

    public void EndTrick()
    {
        dogArmature.animation.Play("Base");
        state = State.Idle;
    }
}
