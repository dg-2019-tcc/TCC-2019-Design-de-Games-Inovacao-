using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

	private bool isOn;
	private CamLookaheadAim CLA;

	[Header("Cinemachine")]
	public CinemachineConfiner CC;
	private CinemachineVirtualCamera VC;


	public void ActivateCamera(bool state)
	{
		isOn = state;

		if (isOn)
		{
			
			VC = GetComponent<CinemachineVirtualCamera>();
			VC.Priority = 40;
			CC = GetComponent<CinemachineConfiner>();
			CC.m_BoundingShape2D = GameObject.Find("CameraConfiner").GetComponent<PolygonCollider2D>();
			CC.InvalidatePathCache();
		}
		else
		{
			VC.Priority = -1;
		}
	}
}
