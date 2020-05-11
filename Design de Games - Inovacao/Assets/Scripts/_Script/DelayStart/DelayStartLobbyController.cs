using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [Header("Botões")]

	[SerializeField]
	private GameObject delayStartButton; //Botão utilizado para criar e entrar em um jogo
	[SerializeField]
	private GameObject delayStartButton2;
    [SerializeField]
    private GameObject delayStartButton3;
    [SerializeField]
    private GameObject delayCancelButton; //Botão utilizado para parar de procurar uma sala de jogo
	//[SerializeField]
	//private GameObject loadingScene; //Feedback pro jogador de que a cena está carregando, o "esperando"
    [SerializeField]
    private GameObject tutorialButton;
    public InputField playerNameInput;
	//public GameObject PlayerCanvas;



	[Header("Fade específico de modo de jogo")]

	[SerializeField]
	private GameObject ColetaFade;
	[SerializeField]
	private GameObject CorridaFade;
    [SerializeField]
    private GameObject FutebolFade;
    [SerializeField]
    private GameObject VoleiFade;
    [SerializeField]
    private GameObject MotoFade;
    [SerializeField]
	private GameObject TutorialFade;

	[SerializeField]
	private float tempoPraFade;



	[Header("Configurações de sala")]
    
    [SerializeField]
    private int RoomSize; //Utilizado para setar manualmente o numero de jogadores de uma sala
    [SerializeField]
    private bool tutorialMode;
    [HideInInspector]
    public bool modo;
    [HideInInspector]
    public string gameModeAtual;

	private int currentRoomSize;
	private bool isDuel;



    [Header("Som")]

    public AudioSource startSound;


	private void Start()
	{
		if (SceneManager.GetActiveScene().name != "MenuCustomizacao") return;
		tutorialButton.SetActive(true);

		if (PlayerPrefs.HasKey("NickName"))
		{
			if (PlayerPrefs.GetString("NickName") == "")
			{
				PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
			}
			else
			{
				PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
			}
		}
		else
		{
			PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
		}
		playerNameInput.text = PhotonNetwork.NickName;

		CorridaFade.SetActive(false);
		ColetaFade.SetActive(false);
		TutorialFade.SetActive(false);
	}



    public void DelayStart(string gameMode)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        //delayStartButton.SetActive(false);
        //delayStartButton2.SetActive(false);
        delayCancelButton.SetActive(true);
		tutorialButton.SetActive(false);
		//loadingScene.SetActive(true);
	//	PlayerCanvas.SetActive(false);

		StartCoroutine(StartGamemode(gameMode));     
    }



   



    public IEnumerator StartGamemode(string gameMode)
	{
		currentRoomSize = RoomSize;
        switch (gameMode)
		{
			case "Corrida":

                CorridaFade.SetActive(true);

				yield return new WaitForSeconds(tempoPraFade);

				DelayStartWaitingRoomController.minPlayerToStart = 2;
				DelayStartWaitingRoomController.tutorialMode = false;
				DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = gameMode;
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }
				break;

			case "Coleta":

                ColetaFade.SetActive(true);

				yield return new WaitForSeconds(tempoPraFade);

				DelayStartWaitingRoomController.minPlayerToStart = 2;
				DelayStartWaitingRoomController.tutorialMode = false;
				DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = gameMode;
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

                break;

			case "Futebol":

                FutebolFade.SetActive(true);

				yield return new WaitForSeconds(tempoPraFade);

				currentRoomSize = 2;
				DelayStartWaitingRoomController.minPlayerToStart = 2;
				DelayStartWaitingRoomController.tutorialMode = false;
				DelayStartWaitingRoomController.gameMode = gameMode;
				gameModeAtual = gameMode;
				if (PhotonNetwork.OfflineMode == false)
				{
					OnJoinRoomButton(gameModeAtual);
				}
				else
				{
					CreateRoomWithMode(gameModeAtual);
				}

				break;

            case "Volei":

                VoleiFade.SetActive(true);
				
				yield return new WaitForSeconds(tempoPraFade);

				currentRoomSize = 2;
				DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = gameMode;
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

                break;

            case "Moto":

                MotoFade.SetActive(true);

				yield return new WaitForSeconds(tempoPraFade);

				DelayStartWaitingRoomController.minPlayerToStart = 2;
				DelayStartWaitingRoomController.tutorialMode = false;
				DelayStartWaitingRoomController.gameMode = gameMode;
				gameModeAtual = gameMode;
				if (PhotonNetwork.OfflineMode == false)
				{
					OnJoinRoomButton(gameModeAtual);
				}
				else
				{
					CreateRoomWithMode(gameModeAtual);
				}

				break;

			case "Tutorial":

                TutorialFade.SetActive(true);

				yield return new WaitForSeconds(tempoPraFade);

				DelayStartWaitingRoomController.minPlayerToStart = 1;
				DelayStartWaitingRoomController.tutorialMode = true;
				CreateTutorialRoom();
				break;

			default:																										//Caso padrão vai pro Tutorial
				
				TutorialFade.SetActive(true);

				yield return new WaitForSeconds(tempoPraFade);

				DelayStartWaitingRoomController.minPlayerToStart = 1;
				DelayStartWaitingRoomController.tutorialMode = true;
				CreateTutorialRoom();
				break;
		}
	}

 
    
    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //Debug para saber que falhou em conectar numa sala
        //Debug.Log("Failed to join a room");
        if(tutorialMode == false)
        {
            CreateRoomWithMode(gameModeAtual);
        }
        else
        {
            CreateTutorialRoom();
        }
    }

    public void CreateRoom()
    {
        //Debug para saber que está criando uma sala
        //Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)currentRoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
    }

    public void CreateRoomWithMode(string gameMode)
    {

        int randomRoomNumber = Random.Range(0, 10000);


        RoomOptions newRoomOptions = new RoomOptions();
        newRoomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        newRoomOptions.MaxPlayers = (byte)currentRoomSize;
        newRoomOptions.CustomRoomProperties.Add(gameMode, 1); //diminuir o tamanho de "Modo" para "MD"
        newRoomOptions.CustomRoomPropertiesForLobby = new string[] { gameMode };

        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, newRoomOptions, null, null);
    }



    public void OnJoinRoomButton(string gameMode)
    {
        ExitGames.Client.Photon.Hashtable expectecProperties = new ExitGames.Client.Photon.Hashtable();
        expectecProperties.Add(gameMode, 1);

        PhotonNetwork.JoinRandomRoom(expectecProperties, 4);
    }


    void CreateTutorialRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = false, IsOpen = false, MaxPlayers = (byte)1 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);        
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
        if(tutorialMode == false)
        {
            CreateRoomWithMode(gameModeAtual);
        }
        else
        {
            CreateTutorialRoom();
        }
    }


    public void DelayCancel()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        delayCancelButton.SetActive(false);
        tutorialButton.SetActive(true);
    //  delayStartButton.SetActive(true);
	//	tutorialButton.SetActive(true);
		//loadingScene.SetActive(false);
	//	PlayerCanvas.SetActive(true);

		CorridaFade.SetActive(false);
		FutebolFade.SetActive(false);
		MotoFade.SetActive(false);
		VoleiFade.SetActive(false);
		ColetaFade.SetActive(false);
		TutorialFade.SetActive(false);
		StopAllCoroutines();

        if(PhotonNetwork.CurrentRoom != null)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}

