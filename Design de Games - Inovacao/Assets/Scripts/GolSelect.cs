using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolSelect : MonoBehaviour
{
    public PlayerThings jogador;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogador = other.GetComponent<PlayerThings>();
            DesabilitaColider();
        }
    }

    [PunRPC]
    public void DesabilitaColider()
    {
         this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
