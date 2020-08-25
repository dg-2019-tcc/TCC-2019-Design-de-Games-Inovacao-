using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Complete;
using UnityCore.Scene;

public class PortaManager : MonoBehaviour
{
    [SerializeField]
	private string qualPorta;

    public TriggerCollisionsController controller;

	public GameObject placaCorrida;
	public GameObject placaColeta;
    public GameObject placaFutebol;
	public GameObject placaMoto;
    public GameObject placaVolei;
    public GameObject placaTenis;
    public GameObject placaBazar;
    public GameObject placaCabelo;
	
    /*public GameObject ButtonRoupa;

    public GameObject customButtons;*/

    public DelayStartLobbyController lobbyController;

    public Joystick joy;

	private float joyGambiarra;
    public bool abriPorta;
    public bool coletaDoor;
    public bool futebolDoor;
    public bool voleiDoor;
    public bool motoDoor;
    public bool corridaDoor;
    public bool hairDoor;
    public bool shirtDoor;
    public bool shortsDoor;
    public bool shoesDoor;

    public FloatVariable spawnHUBPoints;

    public BoolVariable buildPC;
    private Vector2 keyInput;

	private void Start()
	{
        spawnHUBPoints = Resources.Load<FloatVariable>("SpawnHUBPoints");
        buildPC = Resources.Load<BoolVariable>("BuildPC");

        controller = FindObjectOfType<TriggerCollisionsController>();
        joy = FindObjectOfType<Joystick>();
        //ButtonRoupa.SetActive(false);
	}

    private void Update()
    {
		if (joy == null && buildPC.Value == false)
		{
			joy = FindObjectOfType<Joystick>();
		}

        if(controller == null)
        {
            controller = FindObjectOfType<TriggerCollisionsController>();
        }

        if (controller.collisions.isDoor != true)
        {
			CloseDoor();

        }

        if (buildPC.Value == false)
        {
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
                if (joy.Vertical >= 0.8f && corridaDoor == true)
                {
                    spawnHUBPoints.Value = 5;
                    lobbyController.DelayStart("Corrida");
                    //ButtonJogarCorrida = false;
                }
                if (joy.Vertical >= 0.8f && coletaDoor == true)
                {
                    spawnHUBPoints.Value = 1;
                    lobbyController.DelayStart("Coleta");
                    //ButtonJogarColeta = false;
                }
                if (joy.Vertical >= 0.8f && futebolDoor == true)
                {
                    spawnHUBPoints.Value = 2;
                    lobbyController.DelayStart("Futebol");
                    //ButtonJogarCorrida = false;
                }
                if (joy.Vertical >= 0.8f && motoDoor == true)
                {
                    spawnHUBPoints.Value = 3;
                    lobbyController.DelayStart("Moto");
                    //ButtonJogarColeta = false;
                }
                if (joy.Vertical >= 0.8f && voleiDoor == true)
                {
                    spawnHUBPoints.Value = 4;
                    lobbyController.DelayStart("Volei");
                    //ButtonJogarCorrida = false;
                }
            }
        }

        else
        {
            keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (keyInput.y > 0 && hairDoor)
            {
                spawnHUBPoints.Value = 8;
                LoadingManager.instance.LoadGame(SceneType.Cabelo);
                //SceneManager.LoadScene("Cabelo");
            }

            if (keyInput.y > 0 && shirtDoor)
            {
                spawnHUBPoints.Value = 7;
                LoadingManager.instance.LoadGame(SceneType.Shirt);
                //SceneManager.LoadScene("Shirt");
            }


            if (keyInput.y > 0 && shoesDoor)
            {
                Debug.Log(shoesDoor);
                spawnHUBPoints.Value = 6;
                LoadingManager.instance.LoadGame(SceneType.Tenis);
                //SceneManager.LoadScene("Tenis");
            }

            if (keyInput.y > 0 && abriPorta)
            {
                LoadingManager.instance.LoadGame(SceneType.HUB);
                //SceneManager.LoadScene("HUB");
                Debug.Log("Colidiu");
                OpenDoorTutorial();
            }
            if (keyInput.y > 0 && corridaDoor == true)
            {
                spawnHUBPoints.Value = 5;
                lobbyController.DelayStart("Corrida");
                //ButtonJogarCorrida = false;
            }
            if (keyInput.y > 0 & coletaDoor == true)
            {
                spawnHUBPoints.Value = 1;
                lobbyController.DelayStart("Coleta");
                //ButtonJogarColeta = false;
            }
            if (keyInput.y > 0 && futebolDoor == true)
            {
                spawnHUBPoints.Value = 2;
                lobbyController.DelayStart("Futebol");
                //ButtonJogarCorrida = false;
            }
            if (keyInput.y > 0 && motoDoor == true)
            {
                spawnHUBPoints.Value = 3;
                lobbyController.DelayStart("Moto");
                //ButtonJogarColeta = false;
            }
            if (keyInput.y > 0 && voleiDoor == true)
            {
                spawnHUBPoints.Value = 4;
                lobbyController.DelayStart("Volei");
                //ButtonJogarCorrida = false;
            }
        }

        if (buildPC.Value == false)
        {
            joyGambiarra = joy.Vertical;
        }

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
                corridaDoor = true;
                placaCorrida.SetActive(true);
                break;

            case "Coleta":
                coletaDoor = true;
                placaColeta.SetActive(true);
                break;

            case "Futebol":
                futebolDoor = true;
                placaFutebol.SetActive(true);
                break;

            case "Moto":
                motoDoor = true;
                placaMoto.SetActive(true);
                break;

            case "Volei":
                voleiDoor = true;
                placaVolei.SetActive(true);
                break;

            case "Customizar":
                //ButtonRoupa.SetActive(true);
                break;

            case "Cabelo":
                placaCabelo.SetActive(true);
                hairDoor = true;
                break;

            case "Shirt":
                placaBazar.SetActive(true);
                shirtDoor = true;
                break;

            case "Tenis":
                placaTenis.SetActive(true);
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
                corridaDoor = false;
                placaCorrida.SetActive(false);
                break;

            case "Coleta":
                coletaDoor = false;
                placaColeta.SetActive(false);
                break;

            case "Futebol":
                futebolDoor = false;
                placaFutebol.SetActive(false);
                break;

            case "Moto":
                motoDoor = false;
                placaMoto.SetActive(false);
                break;

            case "Volei":
                voleiDoor = false;
                placaVolei.SetActive(false);
                break;

            case "Customizar":
                //ButtonRoupa.SetActive(true);
                break;

            case "Cabelo":
                placaCabelo.SetActive(false);
                hairDoor = false;
                break;

            case "Shirt":
                placaBazar.SetActive(false);
                shirtDoor = false;
                break;

            case "Tenis":
                placaTenis.SetActive(false);
                shoesDoor = false;
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
