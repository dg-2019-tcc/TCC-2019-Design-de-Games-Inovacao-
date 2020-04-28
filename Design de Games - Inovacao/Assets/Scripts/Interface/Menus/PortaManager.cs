using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	private float joyGambiarra;

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
		
        if (controller.collisions.isDoor != true)
        {
			CloseDoor();
			/*
			Debug.Log("Abri");
            OpenDoor();
            Debug.Log(controller.collisions.isDoor);
			*/
        }
        else
        {
            
        }

		if (joyGambiarra < joy.Vertical)
		{
			if (joy.Vertical >= 0.5f && ButtonJogarCorrida != null && ButtonJogarCorrida.activeSelf == true)
			{
				lobbyController.DelayStart("Corrida");
				//ButtonJogarCorrida = false;
			}
			if (joy.Vertical >= 0.5f && ButtonJogarColeta != null && ButtonJogarColeta.activeSelf == true)
			{
				lobbyController.DelayStart("Coleta");
				//ButtonJogarColeta = false;
			}
			if (joy.Vertical >= 0.5f && ButtonJogarFutebol != null && ButtonJogarFutebol.activeSelf == true)
			{

				lobbyController.DelayStart("Futebol");
				//ButtonJogarCorrida = false;
			}
			if (joy.Vertical >= 0.5f && ButtonJogarMoto != null && ButtonJogarMoto.activeSelf == true)
			{
				lobbyController.DelayStart("Moto");
				//ButtonJogarColeta = false;
			}
			if (joy.Vertical >= 0.5f && ButtonJogarVolei != null && ButtonJogarVolei.activeSelf == true)
			{

				lobbyController.DelayStart("Volei");
				//ButtonJogarCorrida = false;
			}
			if (joy.Vertical >= 0.5f && ButtonRoupa != null && ButtonRoupa.activeSelf == true)
			{
                SceneManager.LoadScene("Customiza");
                //lobbyController.DelayStart("Customizar");
                //ButtonJogarColeta = false;
            }
		}
		joyGambiarra = joy.Vertical;

	}

    public void OpenDoor()
    {
		controller.collisions.isDoor = true;


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

    /*private void OnTriggerEnter2D(Collider2D collision)
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
	}*/
}
