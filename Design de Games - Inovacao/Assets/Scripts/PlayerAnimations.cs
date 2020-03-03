using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimations : MonoBehaviour
{
	public Animator playerAC;
	public Animator carroAC;
	public Animator dogAC;

	[HideInInspector] public Rigidbody2D rb2d;
	private PlayerMovement playerMovement;

	//settando id de animação pra pesar menos no update
	public int animatorIsWalking;
	public int animatorJump;
	public int animatorUp;
	public int animatorFalling;
	public int animatorOnFloor;
	public int animatorDogada;
	public int animatorWon;
	public int animatorLost;

	// Start is called before the first frame update
	void Start()
    {
		playerMovement = GetComponent<PlayerMovement>();
		SetAnimations();

		if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] == 1)
		{
			var coroa = PhotonNetwork.Instantiate("Coroa", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
			coroa.transform.parent = transform;
            playerAC.SetTrigger(animatorWon);
        }

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (rb2d.velocity.y > 0)
		{
			playerMovement.canJump.Value = false;
			playerAC.SetBool(animatorUp, true);
			playerAC.SetBool(animatorFalling, false);
			playerAC.SetBool(animatorOnFloor, false);
		}

		else if (rb2d.velocity.y < 0)
		{
			playerAC.SetBool(animatorUp, false);
			playerAC.SetBool(animatorFalling, true);
			playerAC.SetBool(animatorOnFloor, false);
		}

		else if (rb2d.velocity.y == 0)
		{
			playerAC.SetBool(animatorOnFloor, true);
			playerAC.SetBool(animatorUp, false);
			playerAC.SetBool(animatorFalling, false);
		}
	}

	private void SetAnimations()
	{
		animatorIsWalking = Animator.StringToHash("isWalking");
		animatorJump = Animator.StringToHash("Jump");
		animatorUp = Animator.StringToHash("Up");
		animatorFalling = Animator.StringToHash("Falling");
		animatorOnFloor = Animator.StringToHash("onFloor");
		animatorDogada = Animator.StringToHash("Dogada");
		animatorWon = Animator.StringToHash("Won");
		animatorLost = Animator.StringToHash("Lost");
	}

	public void Walk(bool state)
	{
		playerAC.SetBool(animatorIsWalking, state);
		dogAC.SetBool(animatorIsWalking, state);
		carroAC.SetBool(animatorIsWalking, state);
	}

}
