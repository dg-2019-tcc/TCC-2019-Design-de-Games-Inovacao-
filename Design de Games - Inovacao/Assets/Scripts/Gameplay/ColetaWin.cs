using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

using Photon.Pun.UtilityScripts;
public class ColetaWin : MonoBehaviour
{
	private Player[] players;
	private int[] score;
	private int compareScore;
	private Player winning;

	private ColetavelGerador coletavelGerador;

	public int totalColetaveis;


	private bool ganhouJa;

    public BoolVariable acabou01;
    public FloatVariable flowIndex;

	private void Start()
	{
		coletavelGerador = FindObjectOfType<ColetavelGerador>();
		winning = PhotonNetwork.PlayerList[0];
		ganhouJa = false;
	}


	void Update()
    {
        if (acabou01.Value == true)
        {
            foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                //Debug.Log(p.ActorNumber);

                //players[p.ActorNumber] = p;
                //score[p.ActorNumber] = p.GetScore();
                if (compareScore - p.GetScore() < 0)
                {
                    compareScore = p.GetScore();
                    winning = p;
                }
            }


            if (coletavelGerador.coletaveis.Length <= 0)
            {
                if (ganhouJa) return;
                if (OfflineMode.modoDoOffline && compareScore < 4)
                {
                    winning.CustomProperties["Ganhador"] = 0;
                    PhotonNetwork.LoadLevel("TelaVitoria");

                }
                else
                {
                    winning.CustomProperties["Ganhador"] = 1;
                    PhotonNetwork.LoadLevel("TelaVitoria");
                }
                ganhouJa = true;

            }   
        }

        else
        {
            if (coletavelGerador.coletaveis.Length <= 0)
            {
                Debug.Log("Acabou");
                SceneManager.LoadScene("HistoriaColeta");
                flowIndex.Value = 3;
            }

        }
	}


	
}
