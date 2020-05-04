using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			pickColetavel();
		}
	}


	void pickColetavel()
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
		index = Random.Range(0, coletaveis.Length);
		coletaveis[index].SetActive(true);
        coletavelCerto = coletaveis[index];
	}
}
