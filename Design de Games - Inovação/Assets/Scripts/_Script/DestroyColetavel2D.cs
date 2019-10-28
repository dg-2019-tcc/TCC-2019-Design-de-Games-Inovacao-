using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyColetavel2D : MonoBehaviourPunCallbacks
{
    PhotonView playerView;

    public static int index;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerView = other.GetComponent<PhotonView>();
            if(playerView.IsMine == true && playerView.IsMine != null)
            {
                PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                jogador.PV.Owner.AddScore(1);
                Destroy(gameObject);
                index++;
            }
        }
    }

}
