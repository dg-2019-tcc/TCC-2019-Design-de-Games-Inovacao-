using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ColetavelGerador : MonoBehaviour
{
    public GameObject[] coletaveis;
    public GameObject coletavelCerto;
	private int index;

	private void Start()
	{
		RearrangeColetavel();
		SelectColetavel();
		coletaveis[(int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]].SetActive(true);
	}


	void Update()
    {
		Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]);

		RearrangeColetavel();
		

		if (!IsThereColetavel())
		{
			SelectColetavel();
			
		}
	}

	void SelectColetavel()
	{
		if (PhotonNetwork.LocalPlayer.IsMasterClient)
		{
			index = Random.Range(0, coletaveis.Length - 1);
			PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"] = index;
		}
		for (int i = 0; i < coletaveis.Length - 1; i++)
		{
			coletaveis[i].SetActive(false);
		}
		coletaveis[(int)PhotonNetwork.CurrentRoom.CustomProperties["IndexColetavel"]].SetActive(true);

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
				
	}


	private bool IsThereColetavel()
	{
		for (int i = 0; i < coletaveis.Length; i++)
		{
			if (coletaveis[i].activeSelf)
			{
				return true;
			}
		}
		return false;

	}

	
}
