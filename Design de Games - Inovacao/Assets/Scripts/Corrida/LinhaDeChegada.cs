using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaDeChegada : MonoBehaviour
{
    PhotonView playerView;

    public static bool finished;

    public int totalPlayers;

    public static bool changeRoom = false;

    public bool euAcabei = false;

	private WinnerManager winnerManager;

    public void Start()
    {
        euAcabei = false;
        finished = false;
		winnerManager = FindObjectOfType<WinnerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerView = other.GetComponent<PhotonView>();
            if (finished == false)
            {
                if (playerView.IsMine == true && euAcabei == false)
                {
                   // PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                    //playerView.RPC("Acabou", RpcTarget.All);
                    winnerManager.ganhouCorrida = true;
                    totalPlayers++;
                    euAcabei = true;
                    changeRoom = true;
                }
            }
            else
            {
                if (playerView.IsMine == true && euAcabei == false)
                {
                    PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                    totalPlayers++;
                    euAcabei = true;
                    

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
