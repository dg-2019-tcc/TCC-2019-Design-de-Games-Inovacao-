using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HistDisconnect : MonoBehaviour
{
	private void OnDestroy()
	{
		FailMessageManager.manualShutdown = true;
		PhotonNetwork.Disconnect();
	}
}
