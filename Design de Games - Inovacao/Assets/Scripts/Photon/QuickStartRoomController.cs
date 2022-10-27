using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private int multiplayerSceneIndex;

	#region Photon Function
	public override void OnEnable()
	{
		PhotonNetwork.AddCallbackTarget(this);
	}

	public override void OnDisable()
	{
		PhotonNetwork.RemoveCallbackTarget(this);
	}

	public override void OnJoinedRoom()
	{
		StartGame();
	}
	#endregion

	#region Private Functions
	private void StartGame()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.LoadLevel(multiplayerSceneIndex);
		}
	}
	#endregion
}
