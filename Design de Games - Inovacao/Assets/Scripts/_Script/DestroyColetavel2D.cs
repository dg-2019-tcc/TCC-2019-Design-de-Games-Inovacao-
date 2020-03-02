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
    public AudioSource coletaSom;

    public FloatVariable CurrentLevelIndex;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CurrentLevelIndex.Value == 1 && index == 6)
            {
                LevelManager.Instance.GoVitoria();
            }

            else if (CurrentLevelIndex.Value == 2)
            {
                LevelManager.Instance.GoVitoria();
            }
            else
            {

                coletaSom.Play();
                playerView = other.GetComponent<PhotonView>();
                if (playerView.IsMine == true)
                {
                    PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                    jogador.PV.Owner.AddScore(1);
                    Destroy(gameObject);
                    index++;
                }
            }
        }

        
    }

	void Coleta()
	{
		Destroy(gameObject);
		index++;
	}

}
