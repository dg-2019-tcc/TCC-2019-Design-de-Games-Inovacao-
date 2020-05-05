using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoMoedinhas : MonoBehaviour
{

	public ParticleSystem brilhinho;

	public void Coleta()
	{
		brilhinho.loop = false;
		brilhinho.startSpeed = 5;
		brilhinho.emissionRate = 100;
		
	}
}
