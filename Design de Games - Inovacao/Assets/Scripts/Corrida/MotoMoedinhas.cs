using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoMoedinhas : MonoBehaviour
{

	public ParticleSystem brilhinho;
	private ParticleSystem.MainModule main;
	private ParticleSystem.EmissionModule emission;

	bool coletada = true;

	private void Start()
	{
		main = brilhinho.main;
		emission = brilhinho.emission;
	}

	public void Coleta()
	{
		main.loop = false;
		main.startSpeed = 5;
		emission.rateOverTime = 100;
		if (coletada)
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/ColetaMoeda");
			coletada = false;
		}

	}
}
