using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject delayStartButton; //Botão utilizado para criar e entrar em um jogo
    [SerializeField]
    private GameObject delayCancelButton; //Botão utilizado para parar de procurar uma sala de jogo
	[SerializeField]
	private GameObject loadingScene; //Feedback pro jogador de que a cena está carregando, o "esperando"
    [SerializeField]
    private int RoomSize; //Utilizado para setar manualmente o numero de jogadores de uma sala
    
    [HideInInspector]
    public bool modo;

    public InputField playerNameInput;

    public AudioSource startSound;

    public PlayerAvatar newAvatar;

    public PropsCustom hair;

    public PropsCustom shirt;

    public PropsCustom legs;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartButton.SetActive(true);

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


    public void DelayStart()
    {
        SaveCustom();
        startSound.Play();
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
		loadingScene.SetActive(true);
        PhotonNetwork.JoinRandomRoom();


        //ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", modo } };
        

        
        //PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, (byte)RoomSize);
        
    }
    
    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating room now");
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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room... trying again");
        CreateRoom();
    }

    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    public void SaveCustom()
    {
        newAvatar.shirtIndex = shirt.propIndex;
        newAvatar.hairIndex = hair.propIndex;
        newAvatar.legsIndex = legs.propIndex;
        newAvatar.hairColorIndex = hair.colorIndex;
        newAvatar.shirtColorIndex = shirt.colorIndex;
        newAvatar.legsColorIndex = legs.colorIndex;
    }
}

