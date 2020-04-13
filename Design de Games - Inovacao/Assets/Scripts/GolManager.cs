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
            index++;
            playerGol.jogador.PV.Owner.AddScore(1);

            Debug.Log(index);

            Recomeca();

            if(playerGol.jogador.PV.Owner.GetScore() >= 5)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                LevelManager.Instance.GoPodium();
            }
            
        }

    }

    [PunRPC]
    public void Recomeca()
    {
        StartCoroutine("ResetaBola");
    }

    IEnumerator ResetaBola()
    {
        bola.SetActive(false);

        bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

        bola.GetComponent<Rigidbody2D>().isKinematic = true;

        bola.transform.position = bolaSpawnPoint.position;

        index++;

        yield return new WaitForSeconds(3f);

        bola.SetActive(true);

        bola.GetComponent<Rigidbody2D>().isKinematic = false;

    }
}
