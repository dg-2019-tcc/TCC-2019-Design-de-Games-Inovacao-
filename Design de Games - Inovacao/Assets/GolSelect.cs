using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolSelect : MonoBehaviour
{
    public PlayerMovement jogador;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogador = other.GetComponent<PlayerMovement>();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
