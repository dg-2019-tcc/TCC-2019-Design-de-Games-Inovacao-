using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookaheadAim : MonoBehaviour
{
	
	private Transform player;
	private PlayerThings playerScript;
	public GameObject cameraMidpoint;

	private Vector2 aimPos;
	protected Joystick joyStick;

	[Header ("Distância para visão")]
	public float distance;
	public float deltaAltura;
	public float velocidade;

    private float verticalCamPos;

	private void Start()
	{
		player = transform.parent.transform;
		playerScript = player.GetComponent<PlayerThings>();
		joyStick = FindObjectOfType<Joystick>();
		aimPos = new Vector2(distance, aimPos.y);
		//cameraConfiner = playerScript.cameraManager.CC.gameObject;
		cameraMidpoint = GameObject.Find("CameraMidpoint");

	}


	private void Update()
	{
		if (cameraMidpoint == null) return;
		if (joyStick.Vertical >= 0.9 || joyStick.Vertical <= -0.9)
        {
            verticalCamPos = Mathf.Lerp(verticalCamPos, joyStick.Vertical * 3, velocidade*2);
        }

        else
        {
            verticalCamPos = Mathf.Lerp(verticalCamPos, 0, velocidade*2);
		}

		
		gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, player.position.x + aimPos.x, velocidade),
														Mathf.Lerp(player.position.y, cameraMidpoint.transform.position.y + verticalCamPos, deltaAltura) ,
														gameObject.transform.position.z);


		if (PlayerThings.rightDir)
		{
			aimPos = new Vector2(distance, aimPos.y);
		}
		if (PlayerThings.leftDir)
		{
			aimPos = new Vector2(-distance, aimPos.y);
		}

	}

}
