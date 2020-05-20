using Photon.Pun;
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

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogador = other.GetComponent<PlayerThings>();
            if (!PhotonNetwork.InRoom)
            {
                DesabilitaColider();
            }
            else
            {
                photonView.RPC("DesabilitaColider", RpcTarget.All);
            }
            DesabilitaColider();
        }
    }

    [PunRPC]
    public void DesabilitaColider()
    {
         this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
