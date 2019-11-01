using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationSetup : MonoBehaviour
{
	public ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

	void Start()
    {
		PhotonNetwork.SetPlayerCustomProperties(customProperties);

		PhotonNetwork.LocalPlayer.CustomProperties.Add("hairIndex", PlayerPrefs.GetInt("hairIndex"));
        PhotonNetwork.LocalPlayer.CustomProperties.Add("hairColorIndex", PlayerPrefs.GetInt("hairColorIndex"));
        PhotonNetwork.LocalPlayer.CustomProperties.Add("shirtColorIndex", PlayerPrefs.GetInt("shirColorIndex"));
		PhotonNetwork.LocalPlayer.CustomProperties.Add("shirtIndex", PlayerPrefs.GetInt("shirtIndex"));
		PhotonNetwork.LocalPlayer.CustomProperties.Add("legsIndex", PlayerPrefs.GetInt("legsIndex"));
        PhotonNetwork.LocalPlayer.CustomProperties.Add("legsColorIndex", PlayerPrefs.GetInt("legsColorIndex"));

        PhotonNetwork.LocalPlayer.CustomProperties.Add("Ganhador", 0);
	}    
}

