﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private GameObject quickStartButton;
	[SerializeField]
	private GameObject quickCancelButton;
	[SerializeField]
	private int RoomSize;

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		quickStartButton.SetActive(true);
	}


	public void QuickStart()
	{
		quickStartButton.SetActive(false);
		quickCancelButton.SetActive(true);
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		CreateRoom();
	}

	void CreateRoom()
	{
		int randomRoomNumber = Random.Range(0, 10000);
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
		PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		CreateRoom();

	}

	public void QuickCancel()
	{
		quickCancelButton.SetActive(false);
		quickStartButton.SetActive(true);
		PhotonNetwork.LeaveRoom();

	}
}
