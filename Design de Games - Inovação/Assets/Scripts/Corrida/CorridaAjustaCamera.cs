using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using Cinemachine.Utility;


public class CorridaAjustaCamera : MonoBehaviour
{
	private GameObject cam;
	private Transform playerTransform;
	private Vector3 initialPos;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine && cam == null)
		{
			cam = collision.transform.GetChild(0).gameObject;
			initialPos = cam.transform.position;
			playerTransform = collision.transform;
		}
	}

	private void Update()
	{
		if (cam != null)
		{
				cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y, initialPos.z - Mathf.Clamp(playerTransform.position.y, 0, 50)), .1f);
		}
	}

}
