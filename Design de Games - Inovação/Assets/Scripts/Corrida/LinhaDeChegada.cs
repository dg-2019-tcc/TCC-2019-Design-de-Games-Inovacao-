using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaDeChegada : MonoBehaviour
{
    PhotonView playerView;

    public bool finished;

    public int totalPlayers;

    public static bool changeRoom;

    public void Update()
    {
       if (PhotonNetwork.PlayerList.Length >= totalPlayers)
        {
            changeRoom = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerView = other.GetComponent<PhotonView>();
            if (finished == false)
            {
                if (playerView.IsMine == true)
                {
                    PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                    jogador.ganhouCorrida = true;
                    Acabou();
                    totalPlayers++;
                }
            }
            else
            {
                if (playerView.IsMine == true)
                {
                    PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                    jogador.perdeuCorrida = true;
                    totalPlayers++;
                }
            }
        }
    }

    [PunRPC]
    public void Acabou()
    {
        finished = true;
    }
    }
