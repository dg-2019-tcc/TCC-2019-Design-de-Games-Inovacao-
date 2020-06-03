using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoMoedinhas : MonoBehaviour
{

	public ParticleSystem brilhinho;

	bool coletada = true;

	public void Coleta()
	{
		brilhinho.loop = false;
		brilhinho.startSpeed = 5;
		brilhinho.emissionRate = 100;
		if (coletada)
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/ColetaMoeda");
			coletada = false;
		}

	}
}
