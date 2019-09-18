using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyColetavel : MonoBehaviourPunCallbacks
{
    PhotonView playerView;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))

			playerView = other.GetComponent<PhotonView>();
		{
            if(playerView.IsMine == true && playerView.IsMine != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
