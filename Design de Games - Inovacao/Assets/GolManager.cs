using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolManager : MonoBehaviourPunCallbacks
{
    PhotonView playerView;

    public  int index;

    public GameObject bola;

    public Transform bolaSpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bola"))
        {
            bola = other.gameObject;

            GolSelect playerGol = GetComponentInParent<GolSelect>();

            playerGol.jogador.PV.Owner.AddScore(1);
            index++;
        }

    }

    IEnumerator ResataBola()
    {
        bola.transform.position = bolaSpawnPoint.position;

        yield return new WaitForSeconds(3f);

        StopAllCoroutines();
    }
}
