using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaManager : MonoBehaviour
{
    [SerializeField]
	private string qualPorta;

    public TriggerCollisionsController controller;

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
    public bool abriPorta;
    public bool hairDoor;
    public bool shirtDoor;
    public bool shortsDoor;
    public bool shoesDoor;

    public FloatVariable spawnHUBPoints;

	private void Start()
	{
        spawnHUBPoints = Resources.Load<FloatVariable>("SpawnHUBPoints");

        controller = FindObjectOfType<TriggerCollisionsController>();
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
            if (joy.Vertical >= 0.8f && hairDoor)
            {
                spawnHUBPoints.Value = 8;
                SceneManager.LoadScene("Cabelo");
            }

            if (joy.Vertical >= 0.8f && shirtDoor)
            {
                spawnHUBPoints.Value = 7;
                SceneManager.LoadScene("Shirt");
            }


            if (joy.Vertical >= 0.8f && shoesDoor)
            {
                Debug.Log(shoesDoor);
                spawnHUBPoints.Value = 6;
                SceneManager.LoadScene("Tenis");
            }

            if (joy.Vertical >= 0.8f && abriPorta)
            {
                //SceneManager.LoadScene("HUB");
                Debug.Log("Colidiu");
                OpenDoorTutorial();
            }
			if (joy.Vertical >= 0.8f && ButtonJogarCorrida != null && ButtonJogarCorrida.activeSelf == true)
			{
                spawnHUBPoints.Value = 5;
                lobbyController.DelayStart("Corrida");
				//ButtonJogarCorrida = false;
			}
			if (joy.Vertical >= 0.8f && ButtonJogarColeta != null && ButtonJogarColeta.activeSelf == true)
			{
                spawnHUBPoints.Value = 1;
                lobbyController.DelayStart("Coleta");
				//ButtonJogarColeta = false;
			}
			if (joy.Vertical >= 0.8f && ButtonJogarFutebol != null && ButtonJogarFutebol.activeSelf == true)
			{
                spawnHUBPoints.Value = 2;
                lobbyController.DelayStart("Futebol");
				//ButtonJogarCorrida = false;
			}
			if (joy.Vertical >= 0.8f && ButtonJogarMoto != null && ButtonJogarMoto.activeSelf == true)
			{
                spawnHUBPoints.Value = 3;
                lobbyController.DelayStart("Moto");
				//ButtonJogarColeta = false;
			}
			if (joy.Vertical >= 0.8f && ButtonJogarVolei != null && ButtonJogarVolei.activeSelf == true)
			{
                spawnHUBPoints.Value = 4;
                lobbyController.DelayStart("Volei");
				//ButtonJogarCorrida = false;
			}
			if (joy.Vertical >= 0.8f && ButtonRoupa != null && ButtonRoupa.activeSelf == true)
			{
                SceneManager.LoadScene("Customiza");
                //lobbyController.DelayStart("Customizar");
                //ButtonJogarColeta = false;
            }
		}
		joyGambiarra = joy.Vertical;

	}

    public void OpenDoorTutorial()
    {

            SceneManager.LoadScene("HUB");

        FindObjectOfType<PauseManager>().VoltaMenu();
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

            case "Cabelo":
                hairDoor = true;
                break;

            case "Shirt":
                shirtDoor = true;
                break;

            case "Tenis":
                shoesDoor = true;
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

            case "Cabelo":
                hairDoor = false;
                break;

            case "Shirt":
                shirtDoor = false;
                break;

            case "Tenis":
                shortsDoor = false;
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
