﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityCore.Scene;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{


    [Header("Photon")]
    
    private PhotonView myPhotonView;



    [Header("Configurações de sala")]

    public static bool tutorialMode;
    public static SceneType gameMode;
	//public static string gameMode;
    private int playerCount;
    private int roomSize;



    [Header("Necessário para começar")]

    [SerializeField]
    private float maxWaitTime;//Tempo para iniciar o jogo
    [SerializeField]
    private float maxFullGameWaitTime;//Tempo caso a sala encha
    //Jogadores mínimos para começar
    public static int minPlayerToStart;
    //Bools
    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;
    //Timers
    private float timerToStartGame;
    private float notFullRoomTimer;
    private float fullRoomTimer;
    //Mostrar textos
    [SerializeField]
    private Text playerCountDisplay;
    [SerializeField]
    private Image timerToStartDisplay;



    [Header("Transições de cena")]

   // [SerializeField]
   // private string multiplayerSceneIndex;
    [SerializeField]
    private string tutorialSceneIndex;
    [SerializeField]
    private string menuSceneIndex;



    [Header("Botões")]

    [SerializeField]
    private GameObject startGameNow;


    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullRoomTimer = maxFullGameWaitTime;
        notFullRoomTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;
        


        if (PhotonNetwork.IsMasterClient)
        {
            startGameNow.SetActive(true);
        }
        else
        {
            startGameNow.SetActive(false);
        }
        
        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + " - " + roomSize;

		if (roomSize == 2 && !OfflineMode.modoDoOffline)
		{
			startGameNow.SetActive(false);
		}

#if UNITY_EDITOR
		startGameNow.SetActive(true);

#endif

		if(playerCount == roomSize)
        {
            readyToStart = true;
			startGameNow.SetActive(true);
        }
        else if(playerCount >= minPlayerToStart)
        {
            readyToCountDown = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }

		if (OfflineMode.modoDoOffline)
		{
			startGameNow.SetActive(true);
			readyToCountDown = true;
			readyToStart = true;
			playerCountDisplay.text = " ";
		}
    }
    

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        PlayerCountUpdate();

        if (PhotonNetwork.IsMasterClient)
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullRoomTimer = timeIn;

        if (timeIn < fullRoomTimer)
        {
            fullRoomTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PlayerCountUpdate();        
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        if (playerCount <= 1 && minPlayerToStart != 1 && !OfflineMode.modoDoOffline)
        {
            ResetTimer();
        }
        if (readyToStart == true)
        {
            fullRoomTimer -= Time.deltaTime;
            timerToStartGame = fullRoomTimer;
        }
        else if (readyToCountDown && minPlayerToStart != 1 && timerToStartGame > 0)
        {
            notFullRoomTimer -= Time.deltaTime;
            timerToStartGame = notFullRoomTimer;
        }

        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.fillAmount = 1 - (timerToStartGame/maxWaitTime)*2;
        if (timerToStartGame <= 0f)
        {
            if (startingGame)
                return;
            StartGame();
        }
    }

    void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullRoomTimer = maxWaitTime;
        fullRoomTimer = maxFullGameWaitTime;
    }

	[PunRPC]
	public void StartGame()
	{
		startingGame = true;
		if (tutorialMode == false)
		{
            //PhotonNetwork.LoadLevel(gameMode);
            LoadingManager.instance.LoadNewScene(gameMode, SceneType.SalaDeEspera, true);
        }
		else
		{
            //PhotonNetwork.LoadLevel(tutorialSceneIndex);
            LoadingManager.instance.LoadNewScene(SceneType.Tutorial2, SceneType.Customiza, false);
        }
	}

	public void OnStartGameButton()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
		startGameNow.SetActive(false);
		if (!PhotonNetwork.IsMasterClient)
			return;
		PhotonNetwork.CurrentRoom.IsOpen = false;
		myPhotonView.RPC("StartGame", RpcTarget.All);
	}

	public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        if (PhotonNetwork.IsMasterClient)
        {
            startGameNow.SetActive(true);
        }
    }
}
