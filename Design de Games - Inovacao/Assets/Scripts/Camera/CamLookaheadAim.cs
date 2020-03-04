using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookaheadAim : MonoBehaviour
{
	
	private Transform player;
	private PlayerMovement playerScript;
	public GameObject cameraMidpoint;

	private Vector2 aimPos;
	protected Joystick joyStick;

	[Header ("Distância para visão")]
	public float distance;
	public float deltaAltura;
	public float velocidade;

	private void Start()
	{
		player = transform.parent.transform;
		playerScript = player.GetComponent<PlayerMovement>();
		joyStick = FindObjectOfType<Joystick>();
		aimPos = new Vector2(distance, aimPos.y);
		//cameraConfiner = playerScript.cameraManager.CC.gameObject;
		cameraMidpoint = GameObject.Find("CameraMidpoint");

	}


	private void Update()
	{
		gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, player.position.x + aimPos.x, velocidade),
														Mathf.Lerp(player.position.y, cameraMidpoint.transform.position.y + joyStick.Vertical*2, deltaAltura) ,
														gameObject.transform.position.z);


		if (playerScript.rightDir)
		{
			aimPos = new Vector2(distance, aimPos.y);
		}
		if (playerScript.leftDir)
		{
			aimPos = new Vector2(-distance, aimPos.y);
		}

	}

}
