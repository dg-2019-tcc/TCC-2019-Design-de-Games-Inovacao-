using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationSetup : MonoBehaviour
{
	public ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    public PropsCustom legs;
    public PropsCustom shirt;
    public PropsCustom hair;

    void Start()
    {
		PhotonNetwork.SetPlayerCustomProperties(customProperties);

		/*
		PhotonNetwork.LocalPlayer.CustomProperties.Add("hairIndex", hair.propIndex);
        PhotonNetwork.LocalPlayer.CustomProperties.Add("hairColorIndex",hair.colorIndex);
        PhotonNetwork.LocalPlayer.CustomProperties.Add("shirtColorIndex", shirt.colorIndex);
		PhotonNetwork.LocalPlayer.CustomProperties.Add("shirtIndex", shirt.propIndex);
		PhotonNetwork.LocalPlayer.CustomProperties.Add("legsIndex", legs.propIndex);
        PhotonNetwork.LocalPlayer.CustomProperties.Add("legsColorIndex", legs.colorIndex);

        PhotonNetwork.LocalPlayer.CustomProperties.Add("Ganhador", 0);
		*/
	}    
}

