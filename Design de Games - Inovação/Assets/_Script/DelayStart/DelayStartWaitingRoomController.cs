using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;

    [SerializeField]
    private string multiplayerSceneIndex1;
    [SerializeField]
    private string multiplayerSceneIndex2;
    [SerializeField]
    private int menuSceneIndex;

    private int playerCount;
    private int roomSize;
    [SerializeField]
    private int minPlayerToStart;

    [SerializeField]
    private Text playerCountDisplay;
    [SerializeField]
    private Text timerToStartDisplay;

    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;

    private float timerToStartGame;
    private float notFullRoomTimer;
    private float fullRoomTimer;

    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameWaitTime;

    [HideInInspector]
    public bool modo;

    [SerializeField]
    private GameObject botaoModo1;
    [SerializeField]
    private GameObject botaoModo2;

    [SerializeField]
    private GameObject startGameNow;


    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullRoomTimer = maxFullGameWaitTime;
        notFullRoomTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;
        
        //botaoModo2.SetActive(false);


        if (PhotonNetwork.IsMasterClient)
        {
            //botaoModo1.SetActive(true);
            startGameNow.SetActive(true);
        }
        else
        {
            //botaoModo1.SetActive(false);
            startGameNow.SetActive(false);
        }
        
        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + " - " + roomSize;

        if(playerCount == roomSize)
        {
            readyToStart = true;
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
        if (PhotonNetwork.IsMasterClient == true)
        {
            if (modo == true)
            {
                botaoModo1.SetActive(true);
                botaoModo2.SetActive(false);
            }
            else
            {
                botaoModo1.SetActive(false);
                botaoModo2.SetActive(true);
            }
        }
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        if (playerCount <= 1)
        {
            ResetTimer();
        }
        if (readyToStart == true)
        {
            fullRoomTimer -= Time.deltaTime;
            timerToStartGame = fullRoomTimer;
        }
        else if (readyToCountDown)
        {
            notFullRoomTimer -= Time.deltaTime;
            timerToStartGame = notFullRoomTimer;
        }

        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
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

    public void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        /*if(modo != true)
        {
            PhotonNetwork.LoadLevel(multiplayerSceneIndex1);
        }*/
        //else
        //{
            PhotonNetwork.LoadLevel(multiplayerSceneIndex2);
        //}
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void SwichMode()
    {
        if (modo == true)
        {
            modo = false;
            botaoModo1.SetActive(true);
            botaoModo2.SetActive(false);
        }
        else
        {
            modo = true;
            botaoModo1.SetActive(false);
            botaoModo2.SetActive(true);
        }
    }
}
