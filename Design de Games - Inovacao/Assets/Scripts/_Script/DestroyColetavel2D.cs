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

    public FloatVariable botScore;

    public void PegouColetavel(bool isLocal)
    {
		if (isLocal)
		{
			index++;
			if (/*jogador.PV.Owner.GetScore()*/index >= LevelManager.Instance.coletaMax)
			{
				if (botScore.Value >= 8)
				{
					PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
				}

				else
				{
					PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
				}

				LevelManager.Instance.GoPodium();
			}
		}


        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement jogador = other.GetComponent<PlayerMovement>();
            jogador.PV.Owner.AddScore(1);
            index++;

            if (/*jogador.PV.Owner.GetScore()*/index >= LevelManager.Instance.coletaMax)
            {
                if (botScore.Value >= 8)
                {
                    PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
                }

                else
                {
                    PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                }
                
                LevelManager.Instance.GoPodium();
            }


            Destroy(gameObject);

        }

        if (other.CompareTag("AI"))
        {
            botScore.Value++;
            Destroy(gameObject);
        }


    }
}
