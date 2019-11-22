using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [Header("Botões")]

	[SerializeField]
	private GameObject delayStartButton; //Botão utilizado para criar e entrar em um jogo
	[SerializeField]
	private GameObject delayStartButton2;
	[SerializeField]
    private GameObject delayCancelButton; //Botão utilizado para parar de procurar uma sala de jogo
	[SerializeField]
	private GameObject loadingScene; //Feedback pro jogador de que a cena está carregando, o "esperando"
    [SerializeField]
    private GameObject tutorialButton;
    public InputField playerNameInput;



    [Header("Configurações de sala")]
    
    [SerializeField]
    private int RoomSize; //Utilizado para setar manualmente o numero de jogadores de uma sala
    [SerializeField]
    private bool tutorialMode;
    [HideInInspector]
    public bool modo;



    [Header("Som")]

    public AudioSource startSound;


	private void Start()
	{
		if (SceneManager.GetActiveScene().name != "MenuCustomizacao") return;
	//	delayStartButton.SetActive(true);
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
	}


	public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }


    public void DelayStart(string gameMode)
    {
        startSound.Play();
        delayStartButton.SetActive(false);
		delayStartButton2.SetActive(false);
		delayCancelButton.SetActive(true);
        tutorialButton.SetActive(false);
        loadingScene.SetActive(true);
        DelayStartWaitingRoomController.minPlayerToStart = 2;
        DelayStartWaitingRoomController.tutorialMode = false;
		DelayStartWaitingRoomController.gameMode = gameMode;
        PhotonNetwork.JoinRandomRoom();
        
        //ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", modo } };  
        //PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, (byte)RoomSize);        
    }

    public void TutorialStart()
    {
        startSound.Play();
        delayStartButton.SetActive(false);
		delayStartButton2.SetActive(false);
		delayCancelButton.SetActive(true);
        tutorialButton.SetActive(false);
        loadingScene.SetActive(true);
        DelayStartWaitingRoomController.minPlayerToStart = 1;
        DelayStartWaitingRoomController.tutorialMode = true;
        CreateTutorialRoom();
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
            CreateRoom();
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
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        /*
        RoomOptions roomOps = new RoomOptions();
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

        roomOps.CustomRoomProperties.Add("map", modo);

        roomOps.CustomRoomProperties["map"] = 1;
        Debug.Log(roomOps.CustomRoomProperties["map"]);
        //roomOps.CustomRoomProperties.SetValue("map", 3);
        

        roomOps.MaxPlayers = (byte)RoomSize;

        */


        //RoomOptions roomOptions = new RoomOptions();


        //roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "map", modo } };

        //roomOptions.CustomRoomPropertiesForLobby = { "map", 1} ;


        //roomOptions.CustomRoomProperties["map"] = modo;

        //Debug.Log(roomOptions.CustomRoomProperties[2]);
        //roomOptions.MaxPlayers = (byte)RoomSize;
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        //Debug.Log(randomRoomNumber + " / " + modo);
    }

    void CreateTutorialRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = false, IsOpen = false, MaxPlayers = (byte)1 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Debug.Log("Failed to create a room... trying again");
        if(tutorialMode == false)
        {
            CreateRoom();
        }
        else
        {
            CreateTutorialRoom();
        }
    }


    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
    //  delayStartButton.SetActive(true);
	//	tutorialButton.SetActive(true);
		loadingScene.SetActive(false);
		PhotonNetwork.LeaveRoom();
    }
}

