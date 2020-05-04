using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColetavelGerador : MonoBehaviour
{
    public GameObject[] coletaveis;
    public GameObject coletavelCerto;
	private int index;

	private void Start()
	{
		for (int i = 0; i < coletaveis.Length; i++)
		{
			coletaveis[i].SetActive(false);
		}
		index = Random.Range(0, coletaveis.Length);
		coletaveis[index].SetActive(true);
	}


	void Update()
    {
		if (coletaveis[index] == null)
		{
			if (PhotonNetwork.LocalPlayer.IsMasterClient)
			{
				index = Random.Range(0, coletaveis.Length);
				PhotonNetwork.LocalPlayer.CustomProperties["IndexColetavel"] = index;
				
			}
			RearrangeColetavel();
		}
	}


	void RearrangeColetavel()
	{
		List<GameObject> tempColetaveis = new List<GameObject>();
		foreach (GameObject item in coletaveis)
		{
			if (item != null)
				tempColetaveis.Add(item);
		}
		coletaveis = tempColetaveis.ToArray();
		for (int i = 0; i < coletaveis.Length; i++)
		{
			coletaveis[i].SetActive(false);
		}
		
		coletaveis[(int)PhotonNetwork.MasterClient.CustomProperties["IndexColetavel"]].SetActive(true);
	}


	[PunRPC]
	void SincronizaColetaveis()
	{
		if (coletaveis[index] != null)
		{
			Destroy(coletaveis[index]);
		}

	}
}
