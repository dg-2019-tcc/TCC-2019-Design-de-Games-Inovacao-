using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
		PhotonNetwork.ConnectUsingSettings();
    }

    
    void Update()
    {
        
    }

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connectado ao servidor " + PhotonNetwork.CloudRegion);

	}
}
