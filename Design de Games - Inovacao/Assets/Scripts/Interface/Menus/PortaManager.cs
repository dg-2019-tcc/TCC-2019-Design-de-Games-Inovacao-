using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaManager : MonoBehaviour
{
	public string qualPorta;

	public GameObject ButtonJogarCorrida;
	public GameObject ButtonJogarColeta;
	public GameObject ButtonRoupa;

	private void Start()
	{
		ButtonJogarCorrida.SetActive(false);
		ButtonJogarColeta.SetActive(false);
		ButtonRoupa.SetActive(false);

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")){
			switch (qualPorta)
			{
				default:
					break;

				case "Corrida":
					ButtonJogarCorrida.SetActive(true);
					break;

				case "Coleta":
					ButtonJogarColeta.SetActive(true);
					break;

				case "Customizar":
					ButtonRoupa.SetActive(true);
					break;
			}

		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			switch (qualPorta)
			{
				default:
					break;

				case "Corrida":
					ButtonJogarCorrida.SetActive(false);
					break;

				case "Coleta":
					ButtonJogarColeta.SetActive(false);
					break;

				case "Customizar":
					ButtonRoupa.SetActive(false);
					break;
			}

		}
	}

}
