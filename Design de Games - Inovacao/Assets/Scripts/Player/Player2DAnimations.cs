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


	public string idlePose = "0_Idle";
	public string walkAnimation = "0_Corrida_V2";
	public string startJumpAnimation = "1_Pulo";
	public string subindoJumpAnimation = "1_NoAr(1_Subindo)";
	public string transitionJumpAnimation = "1_NoAr(2_Transicao)";
	public string descendoJumpAnimation = "1_NoAr(3_Descendo)";
	public string aterrisandoAnimation = "1_Aterrisando";
	public string chuteAnimation = "3_Bicuda(SemPreparacao)";
	public string abaixarAnimation = "5_Abaixar(2_IdlePose)";
	public string arremessoAnimation = "6_Arremessar(3_Arremesso)";
	public string inativoAnimation = "1_Inatividade(2_IdlePose)";
	public string pipaAnimation = "7_Pipa";
	public string carroWalkAnim = "6_Rolima(Andando)";
	public string carroUpAnim = "6_Rolima(SubindoNoAr)";
	public string carroDownAnim = "6_Rolima(DescendoNoAr)";
	public string motoWalkAnim = "8_Moto(Andando)";
	public string motoDownAnim = "8_Moto(DescendoNoAr)";
	public string motoUpAnim = "8_Moto(SubindoNoAr)";
	public string motoGrauAnim = "8_Moto(Empinando)";
	public string motoCrashAnim = "8_Moto(Batendo)";
	public string motoLandAnim = "8_Moto(Aterrisando)";

	public enum State {Idle, Walking, Jumping, Rising, Falling, TransitionAir, Aterrisando, Chutando, Abaixando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown,MotoWalk, MotoUp, MotoDown,MotoGrau, MotoCrash, MotoLand}

	public State state = State.Idle;

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
	private float coolToIdle;

	public BoolVariable pipaActive;
	public BoolVariable carroActive;

	public bool isCorrida;
	public float inativoTime;

	public TriggerCollisionsController triggerCollisions;

	public BoolVariable textoAtivo;

	private bool isOnline;


	private void Start()
	{
		textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

		photonView = gameObject.GetComponent<PhotonView>();
		controller = GetComponent<Controller2D>();
		triggerCollisions = GetComponent<TriggerCollisionsController>();
		state = State.Abaixando;
		frente.SetActive(false);

		if (PhotonNetwork.InRoom)
		{
			isOnline = true;
		}
		else
		{
			isOnline = false;
		}

	}

	[PunRPC]
	void ChangeArmature()
	{
		lado.SetActive(true);
		frente.SetActive(false);
	}



	public void ChangeMoveAnim(Vector3 moveAmount, Vector2 oldPos, Vector2 input, bool Jump, bool stopJump)
	{

		if (!PhotonNetwork.InRoom || photonView.IsMine)
		{
            if (textoAtivo.Value == false)
            {

                moveX = Mathf.Abs(moveAmount.x);

                if (carroActive.Value == false && pipaActive.Value == false)
                {
                    if (state == State.Aterrisando || state == State.Walking || state == State.Abaixando)
                    {
                        if (state == State.Walking && input.x == 0)
                        {
                            coolToIdle = 0.66f;
                        }

                        else
                        {
                            coolToIdle += Time.deltaTime;
                        }
                    }

                    if (dogButtonAnim == false)
                    {
                        if (pipaActive.Value == false && carroActive.Value == false && dogButtonAnim == false)
                        {
                            if (moveAmount.y < -2 && jaAterrisou && state != State.Aterrisando)
                            {
                                jaAterrisou = false;
                            }

                            if (oldPos.y < moveAmount.y && controller.collisions.below == false)
                            {
                                jaAterrisou = false;
                                PlayAnim("NoArUp");
                            }

                            else if (/*moveAmount.y < 15 &&*/ moveAmount.y > 0 && controller.collisions.below == false)
                            {
                                PlayAnim("TransitionAir");
                            }

                            else if (moveAmount.y <= 0 && controller.collisions.below == false && jaAterrisou == false)
                            {
                                PlayAnim("Fall");
                            }

                            else if (moveAmount.y < -5f && input.x == 0 && input.y >= 0 && controller.collisions.below == true && jaAterrisou == false)
                            {
                                PlayAnim("Aterrisando");
                            }

                            else if (controller.collisions.below && input.x == 0 && input.y < 0)
                            {
                                PlayAnim("Abaixar");
                            }

                            else if (input.x != 0 && controller.collisions.below)
                            {
                                PlayAnim("Walking");
                            }
                            else
                            {
                                PlayAnim("Idle");
                                //}
                            }
                        }
                    }





                    else
                    {
                        PlayAnim("Idle");
                    }
                }

                else
                {
                    if(pipaActive.Value == true)
                    {
                        PlayAnim("Pipa");
                    }

                    else if(carroActive.Value == true)
                    {
                        if (oldPos.y < moveAmount.y && controller.collisions.below == false)
                        {
                            PlayAnim("CarroUp");
                        }

                        else if (moveAmount.y <= 0 && controller.collisions.below == false)
                        {
                            PlayAnim("CarroDown");
                        }

                        else
                        {
                            PlayAnim("CarroWalk");
                        }
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
				PlayAnim("Arremesso");
			}

			else if (fase == 1)
			{
				PlayAnim("Chute");
			}
		}

		else
		{
			PlayAnim("Idle");
		}
	}

	private void PlayAnim(string anim)
	{
		if (isOnline)
		{
			photonView.RPC("AnimState", RpcTarget.All, anim);
		}
		else
		{
			AnimState(anim);
		}

	}

	[PunRPC]
	public void AnimState(string anim)
	{
		if (state == State.Idle || state == State.Inativo)
		{
			if(anim == "Walking" || anim == "NoArUp" || anim == "Fall" || anim == "Aterrisando" || anim == "Chute" || anim == "Arremesso" || anim == "Abaixar" || anim == "TransitionAir" || anim == "Pipa")
			{
                playerFrente.animation.Play(idlePose);
                lado.SetActive(true);
				frente.SetActive(false);
			}
		}
		switch (anim)
		{
            case "CarroWalk":
                if(state != State.CarroWalk)
                {
                    player.animation.Play(carroWalkAnim);
                    state = State.CarroWalk;
                }
                break;

            case "CarroUp":
                if (state != State.CarroUp)
                {
                    player.animation.Play(carroUpAnim);
                    state = State.CarroUp;
                }
                break;

            case "CarroDown":
                if (state != State.CarroDown)
                {
                    player.animation.Play(carroDownAnim);
                    state = State.CarroDown;
                }
                break;

            case "Pipa":
                if(state != State.Pipa)
                {
                    player.animation.Play(pipaAnimation);
                    state = State.Pipa;
                }
                break;

			case "Idle":
				if (state != State.Idle)
				{
                    coolToIdle = 0;
					frente.SetActive(true);
					lado.SetActive(false);
					playerFrente.animation.timeScale = 1;
					playerFrente.animation.Play(idlePose);
					state = State.Idle;
				}
				break;

			case "Inatividade":
				if (state != State.Inativo)
				{
					playerFrente.animation.timeScale = 1;
					playerFrente.animation.Play(inativoAnimation);
					state = State.Inativo;
				}
				break;

			case "Walking":
				if (state != State.Walking)
				{
					player.animation.Play(walkAnimation);
					state = State.Walking;
				}
				break;

			case "StartPulo":
				if (state != State.Jumping)
				{
					inativoTime = 0f;
					player.animation.FadeIn(startJumpAnimation, 0f, 1);
					player.animation.timeScale = 1;
					state = State.Jumping;
				}
				break;
			case "NoArUp":
				if (state != State.Rising)
				{
                    inativoTime = 0f;
					//player.animation.timeScale = 1;
					player.animation.Play(subindoJumpAnimation);
					state = State.Rising;
				}
				break;
			case "Fall":
				if (state != State.Falling)
				{
                    inativoTime = 0f;
					//player.animation.timeScale = 1;
					player.animation.Play(descendoJumpAnimation);
					state = State.Falling;
				}
				break;
			case "Aterrisando":
				if (state != State.Aterrisando)
				{
					inativoTime = 0f;
					//player.animation.timeScale = 1.5f;
					player.animation.Play(aterrisandoAnimation);
					state = State.Aterrisando;
					jaAterrisou = true;
				}
				break;
			case "Chute":
				if (state != State.Chutando)
				{
					inativoTime = 0f;
					//player.animation.timeScale = 1f;
					player.animation.Play(chuteAnimation);
					state = State.Chutando;
				}
				break;
			case "Arremesso":
				if (state != State.Arremessando)
				{
					inativoTime = 0f;
					//player.animation.timeScale = 1;
					player.animation.Play(arremessoAnimation);
					state = State.Arremessando;
				}
				break;
			case "Abaixar":
				if (state != State.Abaixando)
				{
					inativoTime = 0f;
					//player.animation.timeScale = 1;
					player.animation.Play(abaixarAnimation);
					state = State.Abaixando;
				}
				break;
			case "TransitionAir":
				if (state != State.TransitionAir)
				{
                    inativoTime = 0f;
					// player.animation.timeScale = 1.5f;
					player.animation.Play(transitionJumpAnimation);
					state = State.TransitionAir;
				}
				break;
		}
	}
}
