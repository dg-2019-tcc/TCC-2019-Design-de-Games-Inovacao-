using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class ProntoButton : MonoBehaviour
{
    void ChamaTutorial()
    {

    }

    public void TutorialStart()
    {
        DelayStartWaitingRoomController.minPlayerToStart = 1;
        DelayStartWaitingRoomController.tutorialMode = true;
        CreateTutorialRoom();
    }

    void CreateTutorialRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = false, IsOpen = false, MaxPlayers = (byte)1 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
    }
}
