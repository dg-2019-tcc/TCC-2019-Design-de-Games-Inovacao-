﻿using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolManager : MonoBehaviourPunCallbacks
{
    PhotonView playerView;
    public static int index;
    [HideInInspector]
    public static float coletavel;
    public AudioSource coletaSom;



    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bola"))
        {
            jogador.PV.Owner.AddScore(1);
            index++;

            if (jogador.PV.Owner.GetScore()index >= LevelManager.Instance.coletaMax)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                LevelManager.Instance.GoPodium();
            }


            Destroy(gameObject);

        }


    }*/
}
