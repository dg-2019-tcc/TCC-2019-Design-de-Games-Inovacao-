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
		PhotonNetwork.SetPlayerCustomProperties(customProperties); //quando a gente criou esse código a gente muito pensou que ia ficar gigante, mas no final é fácil assim...
	}    
}

