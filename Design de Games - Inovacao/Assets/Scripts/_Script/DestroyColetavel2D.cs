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
	[HideInInspector]
	public static float coletavel;
	public AudioSource coletaSom;
    
    public AudioSource coletaSom;

    public FloatVariable CurrentLevelIndex;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CurrentLevelIndex.Value == 1 && index == 6)
            {
                PlayerMovement jogador = other.GetComponent<PlayerMovement>();
                jogador.PV.Owner.AddScore(1);
				index++;

				if (jogador.PV.Owner.GetScore() >= LevelManager.Instance.variavelquedizquantoscoletaveistemquepegar)
				{
					LevelManager.Instance.GoPodium();
				}
              
				Destroy(gameObject);
			}
        }

        
    }

	void Coleta()
	{
		Destroy(gameObject);
		index++;
	}

}
