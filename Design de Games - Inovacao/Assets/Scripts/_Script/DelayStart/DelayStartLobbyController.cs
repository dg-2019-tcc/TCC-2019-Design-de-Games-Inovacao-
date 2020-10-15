using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityCore.Scene;

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

    public FloatVariable spawnHUBPoints;

    bool SomParaFase = true;

    #region Unity Function

    private void Start()
    {
        SomParaFase = true;

        spawnHUBPoints = Resources.Load<FloatVariable>("SpawnHUBPoints");

        if (SceneManager.GetActiveScene().name != "MenuCustomizacao") return;

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
    }


    #endregion

    #region Public Functions

    //public void DelayStart(string gameMode)
    public void DelayStart(SceneType gameMode)
    {

        if (SomParaFase)
        {
            StartCoroutine(EntrarNaFaseSom());
        }
        //delayStartButton.SetActive(false);
        //delayStartButton2.SetActive(false);
        delayCancelButton.SetActive(true);
        //loadingScene.SetActive(true);
        //	PlayerCanvas.SetActive(false);

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }

        StartCoroutine(StartGamemode(gameMode));
    }

    //public IEnumerator StartGamemode(string gameMode)
    public IEnumerator StartGamemode(SceneType gameMode)
    {
        currentRoomSize = RoomSize;
        switch (gameMode)
        {
            case SceneType.Corrida:

                CorridaFade.SetActive(true);

                spawnHUBPoints.Value = 5;

                yield return new WaitForSeconds(tempoPraFade);

                DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = "Corrida";
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }
                break;

            case SceneType.Coleta:

                ColetaFade.SetActive(true);

                spawnHUBPoints.Value = 1;

                yield return new WaitForSeconds(tempoPraFade);

                DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = "Coleta";
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

                break;

            case SceneType.Futebol:

                FutebolFade.SetActive(true);

                spawnHUBPoints.Value = 2;

                yield return new WaitForSeconds(tempoPraFade);

                currentRoomSize = 2;
                DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = "Futebol";
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

                break;

            case SceneType.Volei:

                VoleiFade.SetActive(true);

                spawnHUBPoints.Value = 4;

                yield return new WaitForSeconds(tempoPraFade);

                currentRoomSize = 2;
                DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = "Volei";
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

                break;

            case SceneType.Moto:

                MotoFade.SetActive(true);

                spawnHUBPoints.Value = 3;

                yield return new WaitForSeconds(tempoPraFade);

                DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = "Moto";
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

                break;


            default:                                                                                                        //Caso padrão vai pro Tutorial

                ColetaFade.SetActive(true);

                spawnHUBPoints.Value = 1;

                yield return new WaitForSeconds(tempoPraFade);

                DelayStartWaitingRoomController.minPlayerToStart = 2;
                DelayStartWaitingRoomController.tutorialMode = false;
                DelayStartWaitingRoomController.gameMode = gameMode;
                gameModeAtual = "Tutorial";
                if (PhotonNetwork.OfflineMode == false)
                {
                    OnJoinRoomButton(gameModeAtual);
                }
                else
                {
                    CreateRoomWithMode(gameModeAtual);
                }

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
        if (tutorialMode == false)
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

        PhotonNetwork.JoinRandomRoom(expectecProperties, 0);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

        if (tutorialMode == false)
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
        //  delayStartButton.SetActive(true);
        //loadingScene.SetActive(false);
        //	PlayerCanvas.SetActive(true);

        CorridaFade.SetActive(false);
        FutebolFade.SetActive(false);
        MotoFade.SetActive(false);
        VoleiFade.SetActive(false);
        ColetaFade.SetActive(false);
        StopAllCoroutines();

        if (PhotonNetwork.CurrentRoom != null)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    #endregion

    #region Private Functions

    IEnumerator EntrarNaFaseSom()
    {
        SomParaFase = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        yield return new WaitForSeconds(2);
        SomParaFase = true;
    }

    void CreateTutorialRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = false, IsOpen = false, MaxPlayers = (byte)1 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
    }

    #endregion
}

