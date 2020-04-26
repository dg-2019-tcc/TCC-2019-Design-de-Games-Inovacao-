using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaManager : MonoBehaviour
{
    [SerializeField]
	private string qualPorta;

    public Controller2D controller;

	public GameObject ButtonJogarCorrida;
	public GameObject ButtonJogarColeta;
    public GameObject ButtonJogarFutebol;
	public GameObject ButtonJogarMoto;
    public GameObject ButtonJogarVolei;

    public GameObject ButtonRoupa;

    public GameObject customButtons;

    public DelayStartLobbyController lobbyController;

    public Joystick joy;

	private void Start()
	{
        controller = FindObjectOfType<Controller2D>();
        joy = FindObjectOfType<Joystick>();
        ButtonRoupa.SetActive(false);
	}

    private void Update()
    {
		if (joy == null)
		{
			joy = FindObjectOfType<Joystick>();
		}

        if (controller.collisions.isDoor == true)
        {
            OpenDoor();
            Debug.Log(controller.collisions.isDoor);
        }

        else
        {
            CloseDoor();
        }
		/*if (joy.Vertical >= 0.5f && ButtonJogarCorrida == true)
		{

			lobbyController.DelayStart("Corrida");
			//ButtonJogarCorrida = false;
		}
		if (joy.Vertical >= 0.5f && ButtonJogarColeta == true)
		{
			lobbyController.DelayStart("Coleta");
			//ButtonJogarColeta = false;
		}
		if (joy.Vertical >= 0.5f && ButtonJogarFutebol == true)
		{

			lobbyController.DelayStart("Futebol");
			//ButtonJogarCorrida = false;
		}
		if (joy.Vertical >= 0.5f && ButtonJogarMoto == true)
		{
			lobbyController.DelayStart("Moto");
			//ButtonJogarColeta = false;
		}
		if (joy.Vertical >= 0.5f && ButtonJogarVolei == true)
		{

			lobbyController.DelayStart("Volei");
			//ButtonJogarCorrida = false;
		}
		if (joy.Vertical >= 0.5f && ButtonRoupa == true)
		{
			lobbyController.DelayStart("Customizar");
			//ButtonJogarColeta = false;
		}*/

	}

    public void OpenDoor()
    {

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

            case "Futebol":
                ButtonJogarFutebol.SetActive(true);
                break;

            case "Moto":
                ButtonJogarMoto.SetActive(true);
                break;

            case "Volei":
                ButtonJogarVolei.SetActive(true);
                break;

            case "Customizar":
                ButtonRoupa.SetActive(true);
                break;

        }
    }

    public void CloseDoor()
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

            case "Futebol":
                ButtonJogarFutebol.SetActive(false);
                break;

            case "Moto":
                ButtonJogarMoto.SetActive(false);
                break;

            case "Volei":
                ButtonJogarVolei.SetActive(false);
                break;

            case "Customizar":
                ButtonRoupa.SetActive(false);
                customButtons.SetActive(false);
                break;
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
                    ButtonJogarCorrida.SetActive(true);
                    break;

				case "Coleta":
                    ButtonJogarColeta.SetActive(true);
                    break;

                case "Futebol":
                    ButtonJogarFutebol.SetActive(true);
                    break;

				case "Moto":
					ButtonJogarMoto.SetActive(true);
					break;

                case "Volei":
                    ButtonJogarVolei.SetActive(true);
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

				case "Futebol":
					ButtonJogarFutebol.SetActive(false);
					break;

				case "Moto":
					ButtonJogarMoto.SetActive(false);
					break;

                case "Volei":
                    ButtonJogarVolei.SetActive(false);
                    break;

                case "Customizar":
                    ButtonRoupa.SetActive(false);
                    customButtons.SetActive(false);
					break;
			}

		}
	}
}
