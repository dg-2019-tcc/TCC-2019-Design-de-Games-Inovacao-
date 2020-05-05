using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Start()
	{
		coletavelGerador = FindObjectOfType<ColetavelGerador>();
	}


	void Update()
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
			winning.CustomProperties["Ganhador"] = 1;
			PhotonNetwork.LoadLevel("TelaVitoria");

		}
	}


	
}
