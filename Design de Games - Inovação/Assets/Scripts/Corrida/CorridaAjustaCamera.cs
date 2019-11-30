using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using Cinemachine.Utility;


public class CorridaAjustaCamera : MonoBehaviour
{
	private GameObject cam;
	private Vector3 currentPos;
	private Vector3 initialPos;
	private bool isHigh = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
		{
			if (cam == null)
			{
				cam = collision.transform.GetChild(0).gameObject;
				initialPos = cam.transform.position;
			}

			isHigh = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
		{
			currentPos = collision.transform.position;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
		{
			isHigh = false;
		}
	}


	private void Update()
	{
		if (cam != null)
		{
			if (isHigh)
			{
					cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y, initialPos.z - Mathf.Clamp(currentPos.y, 0, 50)), .1f);
				
			}
			else
			{
				cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y, initialPos.z), .1f);
			}
		}
	}

}
