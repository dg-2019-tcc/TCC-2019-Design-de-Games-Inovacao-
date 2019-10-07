using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationSetup : MonoBehaviour
{
	public ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
	// Start is called before the first frame update
	void Start()
    {
		PhotonNetwork.SetPlayerCustomProperties(customProperties);

		PhotonNetwork.LocalPlayer.CustomProperties.Add("hairIndex", PlayerPrefs.GetInt("hairIndex"));
		PhotonNetwork.LocalPlayer.CustomProperties.Add("chestIndex", PlayerPrefs.GetInt("chestIndex"));
		PhotonNetwork.LocalPlayer.CustomProperties.Add("legsIndex", PlayerPrefs.GetInt("legsIndex"));

	}

    
}

