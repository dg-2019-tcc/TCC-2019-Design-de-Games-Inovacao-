﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class OfflineButton : MonoBehaviour
{
    public Sprite[] conectionSprites;
    public Image buttonImage;
    public BoolVariableArray acabou01;
    public TextMeshProUGUI offlineText;

    public GameObject offlineMode;

    private void Start()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");

        if (GameManager.historiaMode == false)
        {
            offlineMode.SetActive(true);
            PhotonNetwork.OfflineMode = true;
        }
        else
        {
            offlineMode.SetActive(false);
            PhotonNetwork.OfflineMode = false;
        }

        OfflineMode.modoDoOffline = PhotonNetwork.OfflineMode;
    }

	public void Update()
	{
		if (PhotonNetwork.OfflineMode)
		{
			offlineText.text = "Modo: Offline";
			buttonImage.sprite = conectionSprites[1];
			offlineText.color = new Color32(0, 101, 255, 255);
			OfflineMode.modoDoOffline = PhotonNetwork.OfflineMode;
		}
		else
		{
			offlineText.text = "Modo: Online";
			buttonImage.sprite = conectionSprites[0];
			offlineText.color = new Color32(254, 155, 0, 255);
			OfflineMode.modoDoOffline = PhotonNetwork.OfflineMode;
		}
	}

	public void AtivaOffline()
    {
		PhotonNetwork.OfflineMode = !PhotonNetwork.OfflineMode;
    }
}
