using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class ProntoButton : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;
    [SerializeField]
    private string tutorial;
    [SerializeField]
    private string hub;

    public BoolVariable jaJogou;

    public void ComecaJogo()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        /*if (jaJogou.Value)
        {
            nomeDoMenu = hub;
        }
        else
        {
            nomeDoMenu = tutorial;
        }*/
        //jaJogou.Value = true;
        SceneManager.LoadScene(nomeDoMenu);
    }

    void ChamaTutorial()
    {
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
    }

    public void TutorialStart()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
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
