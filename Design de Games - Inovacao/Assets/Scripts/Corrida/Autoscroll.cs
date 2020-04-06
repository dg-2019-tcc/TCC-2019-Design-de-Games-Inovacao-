using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoscroll : MonoBehaviour
{

	public BoolVariable partidaComecou;

	private Joystick joystick;
	

	public PlayerMovement playerMovement;

	private void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
		joystick = FindObjectOfType<Joystick>();
	}


	void Update()
	{
		if (partidaComecou.Value && joystick.Horizontal >= 0)
		{
			playerMovement.autoScroll = 2;
		}
	}
}
