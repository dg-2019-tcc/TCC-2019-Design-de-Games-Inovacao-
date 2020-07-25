using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Complete;

public class Player2DAnimations : MonoBehaviour
{
	public GameObject frente;
	public GameObject lado;
    public TakeOffUnused takeOffFrente;
    public TakeOffUnused takeOffLado;

	public string idlePose = "0_Idle";
	public string walkAnimation = "0_Corrida_V2";
	public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
	public string descendoJumpAnimation = "1_NoAr(3_Descendo)";
	public string aterrisandoAnimation = "1_Aterrisando";
	public string chuteAnimation = "3_Bicuda(SemPreparacao)";
	public string arremessoAnimation = "6_Arremessar(3_Arremesso)";
	public string inativoAnimation = "1_Inatividade(2_IdlePose)";
	public string pipaAnimation = "7_Pipa";
	public string carroWalkAnim = "6_Rolima(Andando)";
	public string carroUpAnim = "6_Rolima(SubindoNoAr)";
	public string carroDownAnim = "6_Rolima(DescendoNoAr)";
	public string stunAnim = "3_Atordoado";
	public string vitoriaAnim = "2_Vencer";
	public string derrotaAnim = "2_Perder";

	public enum State {Idle, Walking, Rising, Falling, Aterrisando, Chutando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown, Stun, Ganhou, Perdeu}

	public State state = State.Idle;
    public State oldState;
    public State nextState;
    private bool shouldChangeArmature;

	private Controller2D controller;
	[SerializeField]
	private UnityArmatureComponent player;
	[SerializeField]
	private UnityArmatureComponent playerFrente;

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
	private float coolToNext;

	public BoolVariable pipaActive;
	public BoolVariable carroActive;

	public bool isCorrida;
	public float inativoTime;

	public TriggerCollisionsController triggerCollisions;

	public BoolVariable textoAtivo;

	private bool isOnline;

    public BoolVariable levouDogada;

    public bool isVictory;
    public bool isLoja;

    private bool desativaIdle;
    private CustomDisplay customDisplay;
    private DogController dogController;

	private void Start()
	{
        lado.SetActive(true);
        frente.SetActive(true);

        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

		photonView = gameObject.GetComponent<PhotonView>();
		controller = GetComponent<Controller2D>();
		triggerCollisions = GetComponent<TriggerCollisionsController>();
        customDisplay = GetComponent<CustomDisplay>();
        takeOffFrente = frente.GetComponent<TakeOffUnused>();
        takeOffLado = lado.GetComponent<TakeOffUnused>();
        dogController = GetComponent<DogController>();
       
		if (PhotonNetwork.InRoom)
		{
			isOnline = true;
		}
		else
		{
			isOnline = false;
		}

        GameManager.Instance.ChecaFase();
        customDisplay.AtivaRoupas();
        //Tirando a customização não utilizada
        if (!GameManager.Instance.fase.Equals(GameManager.Fase.Loja))
        {
            takeOffFrente.CheckAndExecute();
            takeOffLado.CheckAndExecute();
            takeOffFrente.letThemBeOn = true;
            takeOffLado.letThemBeOn = true;
            frente.SetActive(false);
        }
        else
        {
            takeOffFrente.letThemBeOn = true;
            takeOffLado.letThemBeOn = true;
            lado.SetActive(false);
        }

	}

    public void ChangeMoveAnim(Vector3 moveAmount, Vector2 oldPos, Vector2 input, bool stun, bool ganhou)
	{
        if (isLoja) return;
        Cooldown();
		if (!PhotonNetwork.InRoom || photonView.IsMine)
		{
            if(lado.activeInHierarchy == true && frente.activeInHierarchy == true)
            {
                lado.SetActive(false);
            }
            if (textoAtivo.Value == false)
            {

                moveX = Mathf.Abs(moveAmount.x);
                if (!isVictory)
                {
                    if (!dogController.state.Equals(DogController.State.Carro) && !dogController.state.Equals(DogController.State.Carro) && dogButtonAnim == false && !stun)
                    {
                        if (/*oldPos.y < moveAmount.y*/ moveAmount.y > 0 /*&& controller.collisions.below == false*/)
                        {
                            jaAterrisou = false;
                            nextState = State.Rising;
                        }

                        else if (moveAmount.y < 0 && controller.collisions.below == false /*&& jaAterrisou == false*/)
                        {
                            nextState = State.Falling;
                        }

                        else if(moveAmount.y < -2 && controller.collisions.below == true)
                        {
                            nextState = State.Aterrisando;
                        }

                        else if (input.x != 0 && controller.collisions.below)
                        {
                            nextState = State.Walking;
                        }

                        else
                        {
                            nextState = State.Idle;
                        }
                    }
                    else
                    {
                        if (stun)
                        {
                            nextState = State.Stun;
                        }

                        else
                        {
                            if (dogController.state.Equals(DogController.State.Carro))
                            {
                                nextState = State.CarroWalk;
                                Debug.Log(dogController.state.Equals(DogController.State.Carro));
                            }

                            if (pipaActive.Value == true)
                            {
                                nextState = State.Pipa;
                            }
                        }
                    }
                }


                else
                {
                    if (ganhou)
                    {
                        nextState = State.Ganhou;
                    }

                    else
                    {
                        nextState = State.Perdeu;
                    }
                }
            }

            else
            {
                nextState = State.Idle;
            }

            if(nextState != state && coolToNext>=0.2f)
            {
                PlayAnim(nextState);
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
                nextState = State.Arremessando;
                Debug.Log("DogButton");
			}

			else if (fase == 1)
			{
                nextState = State.Chutando;
			}
		}

		else
		{
            nextState = State.Idle;
		}
        PlayAnim(nextState);
	}

	private void PlayAnim(State anim)
	{
        coolToNext = 0;
        CheckArmature();

        if (isOnline)
		{
			photonView.RPC("AnimState", RpcTarget.All, anim);
		}
		else
		{
			AnimState(anim);
		}
	}

    void Cooldown()
    {
        coolToNext += Time.deltaTime;
    }

    void CheckArmature()
    {
        if(frente.activeInHierarchy == true)
        {
            if (nextState == State.Idle || nextState == State.Ganhou || nextState == State.Inativo || nextState == State.Perdeu || nextState == State.Stun)
            {
                shouldChangeArmature = false;
            }
            else
            {
                shouldChangeArmature = true;
                ChageArmature(false);
            }
        }

        if(lado.activeInHierarchy == true)
        {
            if (nextState == State.Idle || nextState == State.Ganhou || nextState == State.Inativo || nextState == State.Perdeu || nextState == State.Stun)
            {
                shouldChangeArmature = true;
                ChageArmature(true);
            }
            else
            {
                shouldChangeArmature = false;
            }
        }

    }

    void ChageArmature(bool shouldFrenteBeOn)
    {
        frente.SetActive(shouldFrenteBeOn);
        lado.SetActive(!shouldFrenteBeOn);
    }

    [PunRPC]
    public void AnimState(State anim)
    {
        switch (anim)
        {
            case State.Ganhou:
                if (state != State.Ganhou)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.Play(vitoriaAnim);
                    state = State.Ganhou;
                }
                break;

            case State.Perdeu:
                if (state != State.Perdeu)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.Play(derrotaAnim);
                    state = State.Perdeu;
                }
                break;

            case State.Chutando:
                if (state != State.Chutando)
                {
                    inativoTime = 0f;
                    player.animation.Play(chuteAnimation);
                    state = State.Chutando;
                }
                break;

            case State.Arremessando:
                if (state != State.Arremessando)
                {
                    inativoTime = 0f;
                    player.animation.Play(arremessoAnimation);
                    state = State.Arremessando;
                    Debug.Log(state);
                }
                break;

            case State.CarroWalk:
                if (state != State.CarroWalk)
                {
                    player.animation.Play(carroWalkAnim);
                    state = State.CarroWalk;
                }
                break;

            case State.Pipa:
                if (state != State.Pipa)
                {
                    player.animation.Play(pipaAnimation);
                    state = State.Pipa;
                }
                break;

            case State.Aterrisando:
                if (state != State.Aterrisando)
                {
                    player.animation.Play(aterrisandoAnimation);
                    state = State.Aterrisando;
                }
                break;

            case State.Idle:
                if (state != State.Idle)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.timeScale = 1;
                    playerFrente.animation.Play(idlePose);
                    state = State.Idle;
                }
                break;

            case State.Stun:
                if (state != State.Stun)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.Play(stunAnim);
                    state = State.Stun;
                }
                break;

            case State.Walking:
                if (state != State.Walking)
                {
                    player.animation.Play(walkAnimation);
                    state = State.Walking;
                }
                break;

            case State.Rising:
                if (state != State.Rising)
                {
                    inativoTime = 0f;
                    player.animation.Play(subindoJumpAnimation);
                    state = State.Rising;
                }
                break;

            case State.Falling:
                if (state != State.Falling)
                {
                    inativoTime = 0f;
                    player.animation.Play(descendoJumpAnimation);
                    state = State.Falling;
                }
                break;
        }
    }
}
