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
	public string motoWalkAnim = "8_Moto(Andando)";
	public string motoDownAnim = "8_Moto(DescendoNoAr)";
	public string motoUpAnim = "8_Moto(SubindoNoAr)";
	public string motoGrauAnim = "8_Moto(Empinando)";
	public string motoCrashAnim = "8_Moto(Batendo)";
	public string motoLandAnim = "8_Moto(Aterrisando)";
	public string stunAnim = "3_Atordoado";
	public string vitoriaAnim = "2_Vencer";
	public string derrotaAnim = "2_Perder";

	public enum State {Idle, Walking, Rising, Falling, Aterrisando, Chutando, Arremessando, Inativo, Pipa, CarroWalk, CarroUp, CarroDown,MotoWalk, MotoUp, MotoDown,MotoGrau, MotoCrash, MotoLand, Stun, Ganhou, Perdeu}

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

    public bool isMoto;
    public EmpinaMoto empina;
    public BoolVariable levouDogada;

    public bool isVictory;

    private bool desativaIdle;


	private void Start()
	{
		textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

		photonView = gameObject.GetComponent<PhotonView>();
		controller = GetComponent<Controller2D>();
		triggerCollisions = GetComponent<TriggerCollisionsController>();
        if (isMoto == false)
        {
            frente.SetActive(true);
            lado.SetActive(false);
        }

        else
        {
            frente.SetActive(false);
            lado.SetActive(true);
        }

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



	public void ChangeMoveAnim(Vector3 moveAmount, Vector2 oldPos, Vector2 input, bool stun, bool ganhou)
	{

		if (!PhotonNetwork.InRoom || photonView.IsMine)
		{
            if (textoAtivo.Value == false)
            {

                moveX = Mathf.Abs(moveAmount.x);
                if (!isVictory)
                {
                    if (!isMoto && carroActive.Value == false && pipaActive.Value == false && dogButtonAnim == false && !stun)
                    {
                        if (/*oldPos.y < moveAmount.y*/ moveAmount.y > 0 /*&& controller.collisions.below == false*/)
                        {
                            jaAterrisou = false;
                            PlayAnim("NoArUp");
                        }

                        else if (moveAmount.y < 0 && controller.collisions.below == false /*&& jaAterrisou == false*/)
                        {
                            PlayAnim("Fall");
                        }

                        else if (input.x != 0 && controller.collisions.below)
                        {
                            PlayAnim("Walking");
                        }

                        else
                        {
                            PlayAnim("Idle");
                        }
                    }

                    else if (!isMoto && carroActive.Value == false && pipaActive.Value == false && dogButtonAnim == false && stun)
                    {
                        PlayAnim("Stun");
                    }

                    else if (pipaActive.Value == true)
                    {
                        PlayAnim("Pipa");
                    }

                    else if (carroActive.Value == true)
                    {
                        PlayAnim("CarroWalk");

                    }

                    else if (isMoto)
                    {
                        if (stun == false)
                        {

                            if (empina.isEmpinando || empina.isManobrandoNoAr)
                            {
                                PlayAnim("MotoGrau");
                            }


                            else if (oldPos.y < moveAmount.y && controller.collisions.below == false)
                            {
                                PlayAnim("MotoUp");
                            }

                            else if (moveAmount.y <= 0 && controller.collisions.below == false)
                            {
                                PlayAnim("MotoDown");
                            }

                            else
                            {
                                PlayAnim("MotoWalk");
                            }
                        }

                        else
                        {
                            PlayAnim("MotoCrash");
                        }
                    }


                }


                else
                {
                    if (ganhou)
                    {
                        PlayAnim("Ganhou");
                    }

                    else
                    {
                        PlayAnim("Perdeu");
                    }
                }
            }

            else
            {
                PlayAnim("Idle");
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
			if(anim == "Walking" || anim == "NoArUp" || anim == "Fall" || anim == "Aterrisando" || anim == "Chute" || anim == "Arremesso" || anim == "Abaixar" || anim == "TransitionAir" || anim == "Pipa" || anim == "CarroWalk" || anim == "MotoWalk"|| anim == "MotoUp" || anim == "MotoDown" || anim == "MotoCrash" || anim == "MotoGrau")
			{
                lado.SetActive(true);
                playerFrente.animation.Play(idlePose);
				frente.SetActive(false);
			}
		}
		switch (anim)
		{
            case "MotoWalk":
                if(state != State.MotoWalk)
                {
                    player.animation.Play(motoWalkAnim);
                    state = State.MotoWalk;
                }
                break;

            case "MotoCrash":
                if (state != State.MotoCrash)
                {
                    player.animation.Play(motoCrashAnim);
                    state = State.MotoCrash;
                }
                break;

            case "MotoUp":
                if (state != State.MotoUp)
                {
                    player.animation.Play(motoUpAnim);
                    state = State.MotoUp;
                }
                break;

            case "MotoDown":
                if (state != State.MotoDown)
                {
                    player.animation.Play(motoDownAnim);
                    state = State.MotoDown;
                }
                break;

            case "MotoGrau":
                if (state != State.MotoGrau)
                {
                    player.animation.Play(motoGrauAnim);
                    state = State.MotoGrau;
                }
                break;

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

            case "Stun":
                if (state != State.Stun)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.Play(stunAnim);
                    state = State.Stun;
                }
                break;

            case "Ganhou":
                if (state != State.Ganhou)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.Play(vitoriaAnim);
                    state = State.Ganhou;
                }
                break;

            case "Perdeu":
                if (state != State.Perdeu)
                {
                    frente.SetActive(true);
                    lado.SetActive(false);
                    playerFrente.animation.Play(derrotaAnim);
                    state = State.Perdeu;
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
		}
	}
}
