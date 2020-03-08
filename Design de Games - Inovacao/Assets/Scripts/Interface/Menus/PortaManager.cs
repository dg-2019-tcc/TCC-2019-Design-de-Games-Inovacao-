using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaManager : MonoBehaviour
{
	public string qualPorta;

	public bool ButtonJogarCorrida;
	public bool ButtonJogarColeta;
	public GameObject ButtonRoupa;

    public DelayStartLobbyController lobbyController;

    public Joystick joy;

	private void Start()
	{
        joy = FindObjectOfType<Joystick>();
        ButtonRoupa.SetActive(false);
	}

    private void Update()
    {
        if (joy.Vertical >= 0.5f && ButtonJogarCorrida == true)
        {
            lobbyController.DelayStart("Corrida Blocada");
            ButtonJogarCorrida = false;
        }
        if (joy.Vertical >= 0.5f && ButtonJogarColeta == true)
        {
            lobbyController.DelayStart("Fase01Prototipo");
            ButtonJogarColeta = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")){
			switch (qualPorta)
			{
				default:
					break;

				case "Corrida":
                    ButtonJogarCorrida = true;
					break;

				case "Coleta":
                    ButtonJogarColeta = true;
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
					ButtonJogarCorrida = false;
					break;

				case "Coleta":
                    ButtonJogarColeta = false;
					break;

				case "Customizar":
                    ButtonRoupa.SetActive(false);
					break;
			}

		}
	}
}
