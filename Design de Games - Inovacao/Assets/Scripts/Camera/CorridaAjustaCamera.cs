using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using Cinemachine.Utility;


public class CorridaAjustaCamera : MonoBehaviour
{
    //[HideInInspector]
	public GameObject cam;
    public Transform playerTransform;
    public Vector3 initialPos;
    public FloatVariable motoSpeedChange;
    public float maxCam;
    public float minCam;

	

	
	private void Update()
	{
		if (cam != null)
		{
			cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y, initialPos.z - Mathf.Clamp(playerTransform.position.y, 0, 50)), .5f);
		}
		else
		{
            Debug.Log("Ajuste");
            foreach (PhotonView photonView in FindObjectsOfType<PhotonView>())
			{
				if (photonView.CompareTag("Player") && photonView.IsMine)
				{
					cam = photonView.gameObject.transform.GetChild(1).gameObject;
					initialPos = cam.transform.position;
					playerTransform = photonView.gameObject.transform;
				}
			}
		}

	}

}
