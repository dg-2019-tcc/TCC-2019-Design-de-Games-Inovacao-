using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;
using Photon.Pun;
using Complete;

public class EmpinaMoto : MonoBehaviour
{
	[Header ("PhotonViews")]
	public PhotonView playerPV;
	public PhotonView motoPV;

	[Header ("ScriptableObjects")]
	public FloatVariable playerSpeed;
	//spublic BoolVariable canJump;
	public FloatVariable jumpForce;
	private float originalJumpForce;
	public Controller2D controller;
    public TriggerCollisionsController triggerController;

	[Header ("Booleans (tá visível pra checar se ta dando certo, mas tem script vendo tbm)")]
	public bool isEmpinando;
	public bool isManobrandoNoAr;

	public static bool carregado;

	
	[Header("Velocidades")]
	public float baseSpeed;
	public float boostSpeed;
	private float originalSpeed;
	public float jumpMoto;

	[Header("GayTrail")]
	public GameObject particula;
	public ParticleSystem trail;

    [Header("GiraMoto")]
    public float rotationSpeed = 5f;
    public GameObject player;
    public Joystick joy;
    public PlayerThings playerThings;

    #region Unity Function

    private void Start()
    {
        joy = FindObjectOfType<Joystick>();
        isEmpinando = false;
        isManobrandoNoAr = false;
        originalSpeed = playerSpeed.Value;
        playerSpeed.Value = baseSpeed;
        motoPV = GetComponent<PhotonView>();
        originalJumpForce = jumpForce.Value;
        jumpForce.Value = jumpMoto;
    }

    private void Update()
    {
        UpdateRotation();
        CheckTrick();

        if (controller.collisions.below) { isManobrandoNoAr = false;}

        baseSpeed = Mathf.Lerp(baseSpeed, boostSpeed, 0.01f * Time.deltaTime);
    }

    private void OnDestroy()
    {
        playerSpeed.Value = originalSpeed;
        jumpForce.Value = originalJumpForce;
    }

    #endregion

    #region Public Functions

    public void buttonEmpina()
    {
        if (isManobrandoNoAr || isEmpinando) return;

        if (triggerController.collisions.boostMoto && controller.collisions.below)
        {
            isEmpinando = true;

            if (!PhotonNetwork.InRoom)
            {
                daGrau(1);
            }
            else
            {
                motoPV.RPC("daGrau", RpcTarget.All, 1);
            }

        }


        if (!controller.collisions.below)
        {
            isManobrandoNoAr = true;

            if (!PhotonNetwork.InRoom)
            {
                daGrau(1);
            }
            else
            {
                motoPV.RPC("daGrau", RpcTarget.All, 2);
            }
        }
        Debug.Log("buttonEmpina");
    }

    public void stopManobra()
    {
        isManobrandoNoAr = false;
        if (!PhotonNetwork.InRoom){ daGrau(0); }
        else{ motoPV.RPC("daGrau", RpcTarget.All, 0);}
    }

    #endregion

    #region Private Functions

    void UpdateRotation()
    {
        if (controller.collisions.descendingSlope)
        {
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, -controller.collisions.slopeAngle), 1f);
        }
        else if (controller.collisions.climbingSlope)
        {
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, controller.collisions.slopeAngle), 1f);
        }
        else
        {
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.Euler(player.transform.localRotation.x, player.transform.localRotation.y, 0), 0.5f);
        }
    }

    void CheckTrick()
    {
        if (isManobrandoNoAr)
        {
            if (!trail.isPlaying) { trail.Play(); }
            if (controller.collisions.below)
            {
                playerThings.StartCoroutine("LevouDogada");
                if (trail.isPlaying) { trail.Stop();}
            }
        }
        else if (isEmpinando)
        {
            playerSpeed.Value = Mathf.Lerp(playerSpeed.Value, boostSpeed, 0.5f);
            if (!trail.isPlaying){ trail.Play();}
        }
        else
        {
            if (trail.isPlaying) {trail.Stop();}
        }
    }

    [PunRPC]
    private void daGrau(int modo)
    {
        switch (modo)
        {
            case 1:
                StartCoroutine("Empinando");
                isEmpinando = true;
                break;

            case 2:
                //StartCoroutine("Empinando");
                isManobrandoNoAr = true;
                break;

            default:
                isManobrandoNoAr = false;
                break;
        }
        Debug.Log("daGrau");
    }

    private IEnumerator Empinando()
    {
        yield return new WaitForSeconds(2);
        isEmpinando = false;
    }

    #endregion

}
