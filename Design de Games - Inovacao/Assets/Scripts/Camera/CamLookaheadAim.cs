using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookaheadAim : MonoBehaviour
{
	
	private Transform player;
	private PlayerMovement playerScript;

	private Vector2 aimPos;

	[Header ("Distância para visão")]
	public float distance;
	public float velocidade;

	private void Start()
	{
		player = transform.parent.transform;
		playerScript = player.GetComponent<PlayerMovement>();
		aimPos = new Vector2(distance, aimPos.y);
	}


	private void Update()
	{
		gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, player.position.x + aimPos.x, velocidade),
														gameObject.transform.position.y, gameObject.transform.position.z);


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
