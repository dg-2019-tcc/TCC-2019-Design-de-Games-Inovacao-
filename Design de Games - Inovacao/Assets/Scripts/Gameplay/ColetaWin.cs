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
	private int totalScore;

	public int totalColetaveis;


    void Update()
    {
		foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
		{
			players[p.ActorNumber] = p;
			score[p.ActorNumber] = p.GetScore();
			if (compareScore - score[p.ActorNumber] < 0)
			{
				compareScore = score[p.ActorNumber];
				winning = p;
			}
			totalScore += score[p.ActorNumber];
		}


		if (totalScore >= totalColetaveis)
		{
			winning.CustomProperties["Ganhador"] = 1;
			
		}
	}


	
}
