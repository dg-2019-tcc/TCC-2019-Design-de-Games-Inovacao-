﻿using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolSelect : MonoBehaviour
{
    public PlayerThings jogador;
    [HideInInspector]
    public PhotonView photonView;
    public BoolVariable partidaComecou;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && partidaComecou.Value == true)
        {
            jogador = other.GetComponent<PlayerThings>();
            if (!PhotonNetwork.InRoom)
            {
                Debug.Log("DesabilitaCollider00");
                DesabilitaColider();
            }
            else
            {
                Debug.Log("DesabilitaCollider01");
                photonView.RPC("DesabilitaColider", RpcTarget.All);
            }
            DesabilitaColider();
        }
    }

    [PunRPC]
    public void DesabilitaColider()
    {
        Debug.Log("DesabilitaCollider02");
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
